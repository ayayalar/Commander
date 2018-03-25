using System.Threading.Tasks;
using DDDCore.Domain;

namespace DDDCore
{
    internal interface ICommandAsync<out TContext, out TAggregateRoot> where TContext : IBoundedContext<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot
    {
        TContext Context { get; }
        Task InitAsync();
        Task HandleAsync();
    }
}