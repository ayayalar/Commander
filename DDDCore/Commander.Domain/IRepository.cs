namespace Commander.Domain
{
    public interface IRepository<out T> where T : IAggregateRoot
    {
        T Domain { get; }
    }
}
