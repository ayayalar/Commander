using Commander.Domain;

namespace Commander
{
    internal interface ICommand<out TRequest, out TModel> where TModel: IAggregateRoot
    {
        TRequest Request { get; }
        void Init();
        TModel Handle();
    }
}