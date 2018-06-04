namespace Commander.Common
{
    public interface IRepository<out T> where T : AggregateRoot
    {
        T Domain { get; }
    }
}
