using System;
using IEvent = Commander.Common.IEvent;

namespace Commander
{
    public abstract class CommandCommon<TRequest, TModel>
    {
        internal readonly EventConfiguration EventConfiguration = new EventConfiguration();

        public TRequest Request { get; private set; }

        public TModel Model { get; private set; }

        public Func<bool> Guard { get; protected set; } = () => true;

        public T GetEvent<T>() where T : class, IEvent
        {
            if (EventConfiguration.EventType == null)
            {
                throw new ApplicationException(ErrorMessages.HandleEventIsNotRegistered);
            }
            return EventConfiguration.Event as T;
        }

        public void HandleEvent<T>() where T : class, IEvent => EventConfiguration.EventType = typeof(T);

        public void RaiseEvent<TEvent>(TEvent @event) where TEvent : class, IEvent
        {
            EventConfiguration.Events.Add(new EventInfo(@event));
        }

        internal void SetModelInstance(TModel model) => Model = model;
        internal void SetRequest(TRequest request) => Request = request;
    }
}