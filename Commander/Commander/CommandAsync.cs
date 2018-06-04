using System.Threading.Tasks;
using AggregateRoot = Commander.Common.AggregateRoot;

namespace Commander
{
    public abstract class CommandAsync<TRequest, TModel> : CommandCommon<TRequest, TModel>,
        ICommandAsync<TRequest, TModel> where TModel : AggregateRoot
    {
        public Task InitAsync() => Task.CompletedTask;

        public abstract Task<TModel> HandleAsync();
    }
}