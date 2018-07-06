using System;
using System.Collections.Generic;
using System.Text;
using Commander.Common;

namespace Commander.Test.Data
{
    internal class Event : IEvent<EventData>
    {
        public Event(EventData data)
        {
            Data = data;
        }

        public EventData Data { get; }
    }
}
