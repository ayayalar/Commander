using System.Threading.Tasks;
using DDDCore.Domain;

namespace DDDCore
{
    public abstract class Command<T> : CommandCommon<T>, ICommand<T> where T : IAggregateRoot
    {
        public virtual void Handle()
        {
        }

        public Task HandleAsync() => Task.CompletedTask;
    }
}