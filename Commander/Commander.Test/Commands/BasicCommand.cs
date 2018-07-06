using Commander.Test.Data;

namespace Commander.Test.Commands
{
    internal class BasicCommand : Command<Request, Model>
    {
        private readonly Repo _repo;
        public static BasicCommand Instance(Repo repo) => new BasicCommand(repo);

        public BasicCommand(Repo repo)
        {
            _repo = repo;
        }
            
        public override Model Handle()
        {
            return _repo.GetModel(Request.Name);
        }
    }
}
