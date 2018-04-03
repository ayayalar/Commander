using System.Threading.Tasks;
using Commander.Domain;

namespace Commander
{
    internal interface ICommandAsync<out TRequest, TModel> where TModel : IAggregateRoot
    {
        TRequest Request { get; }
        Task InitAsync();
        Task<TModel> HandleAsync();
    }
}