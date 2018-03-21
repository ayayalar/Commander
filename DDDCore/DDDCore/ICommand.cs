using System.Threading.Tasks;
using DDDCore.Domain;

namespace DDDCore
{
    internal interface ICommand<out T> where T : IAggregateRoot
    {
        T Model { get; }
        void Handle();
        Task HandleAsync();
    }
}