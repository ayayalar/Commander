using System;
using System.Collections.Generic;
using DDDCore.Domain;

namespace DDDCore
{
    public class EventConfiguration
    {
        private readonly List<IEvent> _events = new List<IEvent>();

        public bool IsEventHandler { get; set; }
        public Type HandlesEvent { get; set; }
        public IEvent Event { internal get; set; }

        public void RaiseEvent<T>(T @event) where T : class, IEvent, new() => _events.Add(@event);
        public T GetEvent<T>() where T : class, IEvent, new() => Event as T;
        internal IEnumerable<IEvent> GetEvents() => _events;
    }
}