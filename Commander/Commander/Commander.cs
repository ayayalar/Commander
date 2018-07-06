using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commander.Common;

namespace Commander
{
    public class Commander<TRequest, TModel> : ICommandHandler<TRequest, TModel>
        where TModel : AggregateRoot
    {
        private readonly TRequest _request;
        private TModel _model;
        private IEvent _currentEvent;
        private IList<Command<TRequest, TModel>> _commands;
        private IList<CommandAsync<TRequest, TModel>> _asyncCommands;

        public Commander(TRequest request)
        {
            _request = request;
        }

        public TModel Execute(params Command<TRequest, TModel>[] commands)
        {
            _commands = commands.ToList();

            InitializeCommands(_commands);
            return Execute(_commands.Where(command => !command.EventConfiguration.IsEventHandler));
        }

        public async Task<TModel> ExecuteAsync(params CommandAsync<TRequest, TModel>[] commands)
        {
            _asyncCommands = commands.ToList();

            await InitializeCommandsAsync(_asyncCommands);
            return await ExecuteAsync(_asyncCommands.Where(cmd => !cmd.EventConfiguration.IsEventHandler));
        }

        private async Task<TModel> ExecuteAsync(IEnumerable<CommandAsync<TRequest, TModel>> commands)
        {
            commands = commands.ToList();           

            foreach (var command in commands)
            {
                if (!command.Guard()) continue;

                command.SetModelInstance(_model);

                SetCurrentEvent(command);

                var tempModel = await command.HandleAsync();

                if (_model == default(TModel) || _model == tempModel)
                {
                    _model = tempModel;
                }
                else
                {
                    throw new ApplicationException(ErrorMessages.OverrideWithNewInstance);
                }

                foreach (var @event in command.EventConfiguration.GetEvents())
                {
                    @event.IsExecuted = true;
                    _currentEvent = @event.Event;

                    await ExecuteAsync(GetEventHandlers(commands, command, @event));
                }
            }

            return _model;
        }

        private TModel Execute(IEnumerable<Command<TRequest, TModel>> commands)
        {
            commands = commands.ToList();

            foreach (var command in commands)
            {
                if (!command.Guard()) continue;

                command.SetModelInstance(_model);

                SetCurrentEvent(command);

                var tempModel = command.Handle();

                if (_model == default(TModel) || _model == tempModel)
                {
                    _model = tempModel;
                }
                else
                {
                    throw new ApplicationException(ErrorMessages.OverrideWithNewInstance);
                }

                foreach (var @event in command.EventConfiguration.GetEvents())
                {
                    @event.IsExecuted = true;
                    _currentEvent = @event.Event;

                    Execute(GetEventHandlers(commands, command, @event));
                }
            }

            return _model;
        }

        private void InitializeCommands(IEnumerable<Command<TRequest, TModel>> commands)
        {
            foreach (var command in commands)
            {
                command.SetRequest(_request);
                command.Init();
            }
        }

        private async Task InitializeCommandsAsync(IEnumerable<CommandAsync<TRequest, TModel>> commands)
        {
            foreach (var command in commands)
            {
                command.SetRequest(_request);
                await command.InitAsync();
            }
        }

        private static IEnumerable<TCommand> GetEventHandlers<TCommands, TCommand>(TCommands commands, TCommand command, EventInfo @event)
            where TCommands : IEnumerable<TCommand>
            where TCommand : CommandCommon<TRequest, TModel>
        {
            return commands.Where(app => app.EventConfiguration.IsEventHandler &&
                                          app.EventConfiguration.EventType == @event.GetType())
                .Where(cmd => cmd.GetType() != command.GetType() && !@event.IsExecuted)
                .ToList();
        }

        private void SetCurrentEvent<TCommand>(TCommand command)
            where TCommand : CommandCommon<TRequest, TModel>
        {
            if (command.EventConfiguration.EventType == _currentEvent?.GetType())
            {
                command.EventConfiguration.Event = _currentEvent;
            }
        }
    }
}