using System.Threading.Tasks;
using Commander.Domain;

namespace Commander
{
    public abstract class CommandAsync<TRequest, TModel> : CommandCommon<TRequest, TModel>,
        ICommandAsync<TRequest, TModel> where TModel : IAggregateRoot
    {
        public Task InitAsync() => Task.CompletedTask;

        public abstract Task<TModel> HandleAsync();
    }
}