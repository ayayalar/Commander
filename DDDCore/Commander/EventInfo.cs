using Commander.Domain;

namespace Commander
{
    internal class EventInfo
    {
        public EventInfo(IEvent @event)
        {
            Event = @event;
        }

        public IEvent Event { get; set; }
        public bool IsExecuted { get; set; }
    }
}