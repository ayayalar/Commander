using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDCore.Domain;

namespace DDDCore
{
    public class CommandRunner<T> : ICommandHandler<T> where T : IAggregateRoot
    {
        private readonly T _aggregateRoot;
        private IEvent _currentEvent;
        private IList<Command<T>> _commands;

        public CommandRunner(T aggregateRoot)
        {
            _aggregateRoot = aggregateRoot;
        }

        public void Handle(params Command<T>[] commands)
        {
            _commands = commands;
            Handle(_commands.Where(command => !command.EventConfiguration.IsEventHandler).ToList());
        }

        private void Handle(IEnumerable<Command<T>> commands)
        {
            foreach (var command in commands)
            {
                command.AggregateRoot = _aggregateRoot;

                if (command.EventConfiguration.HandlesEvent == _currentEvent?.GetType())
                {
                    command.EventConfiguration.Event = _currentEvent;
                }

                if (command.NestedCommandOrder == NestedCommandOrder.First)
                {
                    Handle(command.CommandList.OfType<Command<T>>());
                    command.Handle();
                }
                else
                {
                    command.Handle();
                    Handle(command.CommandList.OfType<Command<T>>());
                }

                foreach (var @event in command.EventConfiguration.GetEvents())
                {
                    _currentEvent = @event;
                    var eventHandlers = GetEventHandlers(@event);
                    Handle(eventHandlers);
                }
            }
        }        

        public async Task HandleAsync(params CommandAsync<T>[] commandList)
        {
            foreach (var appLogic in commandList)
            {
                appLogic.AggregateRoot = _aggregateRoot;
                if (appLogic.NestedCommandExist)
                {
                    await HandleAsync(appLogic.CommandList.OfType<CommandAsync<T>>().ToArray());
                }

                await appLogic.HandleAsync();
            }
        }

        private IEnumerable<Command<T>> GetEventHandlers(IEvent @event)
        {
            return _commands.Where(app => app.EventConfiguration.IsEventHandler &&
                                          app.EventConfiguration.HandlesEvent == @event.GetType()).ToList();
        }
    }
}