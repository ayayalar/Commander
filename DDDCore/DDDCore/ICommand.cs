using DDDCore.Domain;

namespace DDDCore
{
    internal interface ICommand<out TContext, out TAggregateRoot> where TContext : IBoundedContext<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot
    {
        TContext Context { get; }
        void Init();
        void Handle();
    }
}