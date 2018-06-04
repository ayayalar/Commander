using System.Threading.Tasks;
using AggregateRoot = Commander.Common.AggregateRoot;

namespace Commander
{
    internal interface ICommandAsync<out TRequest, TModel> where TModel : AggregateRoot
    {
        TRequest Request { get; }
        Task InitAsync();
        Task<TModel> HandleAsync();
    }
}