using System.Threading.Tasks;
using DDDCore.Domain;

namespace DDDCore
{
    public interface ICommandHandler<TContext, TAggregateRoot> where TContext : BoundedContext<TAggregateRoot>, new()
        where TAggregateRoot : IAggregateRoot
    {
        void Handle(params Command<TContext, TAggregateRoot>[] commands);
        Task HandleAsync(params CommandAsync<TContext, TAggregateRoot>[] commands);
    }
}