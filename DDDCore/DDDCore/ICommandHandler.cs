using System.Threading.Tasks;
using DDDCore.Domain;

namespace DDDCore
{
    public interface ICommandHandler<T> where T: IAggregateRoot
    {
        void Handle(params Command<T>[] commands);
        Task HandleAsync(params CommandAsync<T>[] commands);
    }
}