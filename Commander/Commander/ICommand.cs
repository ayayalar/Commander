using AggregateRoot = Commander.Common.AggregateRoot;

namespace Commander
{
    internal interface ICommand<out TRequest, out TModel> where TModel: AggregateRoot
    {
        TRequest Request { get; }
        void Init();
        TModel Handle();
    }
}