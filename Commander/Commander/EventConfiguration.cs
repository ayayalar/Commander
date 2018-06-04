using System;
using System.Collections.Generic;
using IEvent = Commander.Common.IEvent;

namespace Commander
{
    public class EventConfiguration
    {
        internal readonly List<EventInfo> Events = new List<EventInfo>();
        internal IEnumerable<EventInfo> GetEvents() => Events;
        internal bool IsEventHandler => Event != null;
        protected internal IEvent Event { get; set; }
        internal Type EventType { get; set; }
    }
}