using DDDCore.Domain;

namespace DDDCore
{
    public abstract class BoundedContext<TAggregateRoot> : IBoundedContext<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot
    {
        public TAggregateRoot AggregateRoot { get; internal set; }
    }
}