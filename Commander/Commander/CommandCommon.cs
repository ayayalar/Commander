using System;
using Commander.Domain;


namespace Commander
{
    public abstract class CommandCommon<TRequest, TModel>
    {
        internal readonly EventConfiguration EventConfiguration = new EventConfiguration();

        public TRequest Request { get; internal set; }

        public TModel Model { get; internal set; }

        public Func<bool> Guard { get; protected set; } = () => true;

        public T GetEvent<T>() where T : class, IEvent, new()
        {
            if (EventConfiguration.EventType == null)
            {
                throw new ApplicationException("Event is not handled. Must call HandleEvent<T> in Init method first.");
            }
            return EventConfiguration.Event as T;
        }

        public void HandleEvent<T>() where T : class, IEvent, new() => EventConfiguration.EventType = typeof(T);

        public void RaiseEvent<T>(T @event) where T : class, IEvent, new()
        {
            EventConfiguration.Events.Add(new EventInfo(@event));
        }
    }
}