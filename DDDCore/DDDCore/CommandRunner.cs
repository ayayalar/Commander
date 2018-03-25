using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDCore.Domain;

namespace DDDCore
{
    public class CommandRunner<TContext, TAggregateRoot> : ICommandHandler<TContext, TAggregateRoot>
        where TContext : BoundedContext<TAggregateRoot>, new() where TAggregateRoot : IAggregateRoot
    {
        private readonly TAggregateRoot _aggregateRoot;
        private IEvent _currentEvent;
        private IList<Command<TContext, TAggregateRoot>> _commands;
        private IList<CommandAsync<TContext, TAggregateRoot>> _asyncCommands;

        public CommandRunner(TAggregateRoot aggregateRoot)
        {
            _aggregateRoot = aggregateRoot;
        }

        public void Handle(params Command<TContext, TAggregateRoot>[] commands)
        {
            _commands = commands;
            InitializeCommands(_commands);
            Handle(_commands.Where(command => !command.EventConfiguration.IsEventHandler));
        }

        public async Task HandleAsync(params CommandAsync<TContext, TAggregateRoot>[] commands)
        {
            _asyncCommands = commands;
            await InitializeCommandsAsync(_asyncCommands);
            await HandleAsync(_asyncCommands.Where(cmd => !cmd.EventConfiguration.IsEventHandler));
        }

        private async Task HandleAsync(IEnumerable<CommandAsync<TContext, TAggregateRoot>> commands)
        {
            foreach (var command in commands)
            {
                command.Context.AggregateRoot = _aggregateRoot;
                SetCurrentEvent(command);

                if (command.ChildCommandOrder == CommandOrder.First)
                {
                    await HandleAsync(command.ChildCommands.OfType<CommandAsync<TContext, TAggregateRoot>>());
                    await command.HandleAsync();
                }
                else
                {
                    await command.HandleAsync();
                    await HandleAsync(command.ChildCommands.OfType<CommandAsync<TContext, TAggregateRoot>>());
                }

                foreach (var @event in command.EventConfiguration.GetEvents())
                {
                    _currentEvent = @event;
                    var eventHandlers = GetEventHandlers<CommandAsync<TContext, TAggregateRoot>>(@event);
                    await HandleAsync(eventHandlers);
                }
            }
        }

        private void Handle(IEnumerable<Command<TContext, TAggregateRoot>> commands)
        {
            foreach (var command in commands)
            {
                command.Context.AggregateRoot = _aggregateRoot;
                SetCurrentEvent(command);

                if (command.ChildCommandOrder == CommandOrder.First)
                {
                    Handle(command.ChildCommands.OfType<Command<TContext, TAggregateRoot>>());
                    command.Handle();
                }
                else
                {                    
                    command.Handle();
                    Handle(command.ChildCommands.OfType<Command<TContext, TAggregateRoot>>());
                }

                foreach (var @event in command.EventConfiguration.GetEvents())
                {
                    _currentEvent = @event;
                    var eventHandlers = GetEventHandlers<Command<TContext, TAggregateRoot>>(@event);
                    Handle(eventHandlers);
                }
            }
        }

        private void InitializeCommands(IEnumerable<Command<TContext, TAggregateRoot>> commands)
        {
            foreach (var command in commands)
            {
                if (command.ChildCommandOrder == CommandOrder.First)
                {
                    InitializeCommands(command.ChildCommands.OfType<Command<TContext, TAggregateRoot>>());
                    command.Init();
                }
                else
                {
                    command.Init();
                    InitializeCommands(command.ChildCommands.OfType<Command<TContext, TAggregateRoot>>());
                }
                
            }
        }

        private async Task InitializeCommandsAsync(IEnumerable<CommandAsync<TContext, TAggregateRoot>> commands)
        {
            foreach (var command in commands)
            {                
                if (command.ChildCommandOrder == CommandOrder.First)
                {
                    await InitializeCommandsAsync(
                        command.ChildCommands.OfType<CommandAsync<TContext, TAggregateRoot>>());
                    await command.InitAsync();
                    
                }
                else
                {
                    await command.InitAsync();
                    await InitializeCommandsAsync(
                        command.ChildCommands.OfType<CommandAsync<TContext, TAggregateRoot>>());
                }
            }
        }

        private IEnumerable<TCommand> GetEventHandlers<TCommand>(IEvent @event)
        {
            return _commands.Where(app => app.EventConfiguration.IsEventHandler &&
                                          app.EventConfiguration.EventType == @event.GetType())
                .OfType<TCommand>()
                .ToList();
        }

        private void SetCurrentEvent<TCommand>(TCommand command)
            where TCommand : CommandCommon<TContext, TAggregateRoot>
        {
            if (command.EventConfiguration.EventType == _currentEvent?.GetType())
            {
                command.EventConfiguration.Event = _currentEvent;
            }
        }
    }
}