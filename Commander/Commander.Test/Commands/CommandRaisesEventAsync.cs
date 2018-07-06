using System.Threading.Tasks;
using Commander.Test.Data;

namespace Commander.Test.Commands
{
    internal class CommandRaisesEventAsync : CommandAsync<Request, Model>
    {
        private readonly Repo _repo;

        public static CommandRaisesEvent Instance(Repo repo) => new CommandRaisesEvent(repo);

        public CommandRaisesEventAsync(Repo repo)
        {
            _repo = repo;
        }

        public override Task<Model> HandleAsync()
        {
            var model = _repo.GetModel(Request.Name);
            model.Update();
            RaiseEvent(new Event(new EventData(true)));

            return Task.FromResult(model);
        }
    }
}