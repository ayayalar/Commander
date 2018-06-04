using System.Threading.Tasks;
using AggregateRoot = Commander.Common.AggregateRoot;

namespace Commander
{
    public interface ICommandHandler<TRequest, TModel> where TModel : AggregateRoot
    {
        TModel Execute(Command<TRequest, TModel>[] commands);
        Task<TModel> ExecuteAsync(params CommandAsync<TRequest, TModel>[] commands);
    }
}