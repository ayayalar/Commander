using System.Threading.Tasks;
using Commander.Domain;

namespace Commander
{
    public interface ICommandHandler<TRequest, TModel> where TModel : IAggregateRoot
    {
        TModel Execute(Command<TRequest, TModel>[] commands);
        Task<TModel> ExecuteAsync(params CommandAsync<TRequest, TModel>[] commands);
    }
}