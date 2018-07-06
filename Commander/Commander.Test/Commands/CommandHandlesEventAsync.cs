using System.Threading.Tasks;
using Commander.Test.Data;

namespace Commander.Test.Commands
{
    internal class CommandHandlesEventAsync : CommandAsync<Request, Model>
    {
        public static CommandHandlesEventAsync Instance() => new CommandHandlesEventAsync();

        public override Task InitAsync()
        {
            HandleEvent<Event>();
            return Task.CompletedTask;
        }

        public override Task<Model> HandleAsync()
        {
            var @event = GetEvent<Event>();

            if (@event.Data.ModelUpdated) Model.Update();

            return Task.FromResult(Model);
        }
    }
}