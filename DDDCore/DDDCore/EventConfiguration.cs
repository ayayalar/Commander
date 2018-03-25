using System;
using System.Collections.Generic;
using System.Linq;
using DDDCore.Domain;

namespace DDDCore
{
    public class EventConfiguration
    {
        private readonly List<IEvent> _events = new List<IEvent>();

        internal IEnumerable<IEvent> GetEvents() => _events;
        internal bool IsEventHandler => Event != null;
        internal IEvent Event { get; set; }
        internal Type EventType { get; set; }

        public void HandleEvent<T>() where T : class, IEvent, new() => EventType = typeof(T);
        public T GetEvent<T>() where T : class, IEvent, new() => Event as T;

        public void RaiseEvent<T>(T @event) where T : class, IEvent, new()
        {
            if (@event.GetType() == EventType || _events.Any(e => e.GetType() == EventType))
            {
                throw new ApplicationException("Cannot handle and raise the same event from same command.");
            }

            _events.Add(@event);
        } 
    }
}