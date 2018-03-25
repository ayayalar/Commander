using DDDCore.Domain;

namespace DDDCore
{
    public abstract class CommandCommon<TContext, TAggregateRoot> where TContext : BoundedContext<TAggregateRoot>, new()
        where TAggregateRoot : IAggregateRoot
    {
        public TContext Context { get; internal set; } = new TContext();
        public EventConfiguration EventConfiguration { get; } = new EventConfiguration();
        public CommandOrder ChildCommandOrder { get; protected set; }
    }
}