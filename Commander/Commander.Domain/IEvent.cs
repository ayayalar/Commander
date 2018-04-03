namespace Commander.Domain
{
    public interface IEvent<out T> : IEvent
    {
        T Data { get; }
    }

    public interface IEvent
    {
    }
}