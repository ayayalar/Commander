using System.Collections.Generic;
using System.Threading.Tasks;
using DDDCore.Domain;

namespace DDDCore
{
    public interface IAppLogicHandler<T> where T: IAggregateRoot
    {
        void Handle(params Command<T>[] commandList);
        Task HandleAsync(params CommandAsync<T>[] commandList);
    }
}