using System.Threading.Tasks;
using DDDCore.Domain;

namespace DDDCore
{
    public abstract class CommandAsync<T> : CommandCommon<T>, ICommand<T> where T : IAggregateRoot
    {
        public void Handle()
        {
        }

        public abstract Task HandleAsync();
    }
}