using System.Linq;
using System.Threading.Tasks;
using DDDCore.Domain;

namespace DDDCore
{
    public class CommandRunner<T> : IAppLogicHandler<T> where T : IAggregateRoot
    {
        private readonly T _model;
        private bool _runEventHandlers = false;
        private IEvent _currentEvent;

        public CommandRunner(T model)
        {
            _model = model;
        }
        
        public void Handle(params Command<T>[] commandList)
        {
            foreach (var appLogic in commandList.Where(app => app.IsEventHandler == _runEventHandlers))
            {
                appLogic.Model = _model;

                if (_runEventHandlers)
                {
                    if (appLogic.HandlesEvent == _currentEvent.GetType())
                    {
                        appLogic.Event = _currentEvent;
                    }
                }

                if (appLogic.NestedCommandOrder == NestedCommandOrder.First)
                {
                    Handle(appLogic.CommandList.OfType<Command<T>>().ToArray());
                    appLogic.Handle();
                    
                }
                else
                {
                    appLogic.Handle();
                    Handle(appLogic.CommandList.OfType<Command<T>>().ToArray());
                }
                foreach (var @event in appLogic.GetEvents())
                {
                    _currentEvent = @event;
                    _runEventHandlers = true;
                    var eventHandlers = commandList
                        .Where(app => app.IsEventHandler && app.HandlesEvent == @event.GetType())
                        .ToArray();
                    Handle(eventHandlers);
                }
            }
        }

        public async Task HandleAsync(params CommandAsync<T>[] commandList)
        {
            foreach (var appLogic in commandList)
            {
                appLogic.Model = _model;
                if (appLogic.NestedCommandExist)
                {
                    await HandleAsync(appLogic.CommandList.OfType<CommandAsync<T>>().ToArray());
                }
                await appLogic.HandleAsync();
            }
        }
    }
}