namespace Commander.Common
{
    public interface IEvent<out T> : IEvent
    {
        T Data { get; }
    }

    public interface IEvent
    {
    }
}