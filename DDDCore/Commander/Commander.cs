using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commander.Domain;

namespace Commander
{
    public class Commander<TRequest, TModel> : ICommandHandler<TRequest, TModel>
        where TModel : IAggregateRoot
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
            _asyncCommands = commands;
            await InitializeCommandsAsync(_asyncCommands);
            return await ExecuteAsync(_asyncCommands.Where(cmd => !cmd.EventConfiguration.IsEventHandler));
        }

        private async Task<TModel> ExecuteAsync(IEnumerable<CommandAsync<TRequest, TModel>> commands)
        {
            foreach (var command in commands)
            {
                if (!command.Guard()) continue;

                command.Model = _model;
                SetCurrentEvent(command);

                _model = await command.HandleAsync();

                foreach (var @event in command.EventConfiguration.GetEvents())
                {
                    @event.IsExecuted = true;
                    _currentEvent = @event.Event;

                    var eventHandlers = GetEventHandlers(command, @event);
                    await ExecuteAsync(eventHandlers);
                }
            }

            return _model;
        }

        private TModel Execute(IEnumerable<Command<TRequest, TModel>> commands)
        {
            foreach (var command in commands)
            {
                if (!command.Guard()) continue;

                command.Model = _model;
                SetCurrentEvent(command);

                _model = command.Handle();

                foreach (var @event in command.EventConfiguration.GetEvents())
                {
                    @event.IsExecuted = true;
                    _currentEvent = @event.Event;

                    Execute(GetEventHandlers(command, @event));
                }
            }

            return _model;
        }

        private void InitializeCommands(IEnumerable<Command<TRequest, TModel>> commands)
        {
            foreach (var command in commands) 
            {
                command.Request = _request;
                command.Init();
            }
        }

        private async Task InitializeCommandsAsync(IEnumerable<CommandAsync<TRequest, TModel>> commands)
        {
            foreach (var command in commands)
            {
                command.Request = _request;
                await command.InitAsync();
            }
        }

        private IEnumerable<TCommand> GetEventHandlers<TCommand>(TCommand command, EventInfo @event)
        {
            return _commands.Where(app => app.EventConfiguration.IsEventHandler &&
                                          app.EventConfiguration.EventType == @event.GetType())
                .OfType<TCommand>()
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