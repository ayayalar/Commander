namespace DDDCore.Domain
{
    public interface IBoundedContext<out T> where T : IAggregateRoot
    {
        T AggregateRoot { get; }
    }
}
