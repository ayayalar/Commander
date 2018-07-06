using System.Threading.Tasks;
using Commander.Test.Data;

namespace Commander.Test.Commands
{
    internal class BasicCommandAsync : CommandAsync<Request, Model>
    {
        private readonly Repo _repo;
        public static BasicCommandAsync Instance(Repo repo) => new BasicCommandAsync(repo);

        public BasicCommandAsync(Repo repo)
        {
            _repo = repo;
        }

        public override Task<Model> HandleAsync()
        {
            return Task.FromResult(_repo.GetModel(Request.Name));
        }
    }
}