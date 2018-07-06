using Commander.Test.Data;

namespace Commander.Test.Commands
{
    internal class CommandRaisesEvent : Command<Request, Model>
    {
        private readonly Repo _repo;

        public static CommandRaisesEvent Instance(Repo repo) => new CommandRaisesEvent(repo);

        public CommandRaisesEvent(Repo repo)
        {
            _repo = repo;
        }

        public override Model Handle()
        {            
            var model = _repo.GetModel(Request.Name);
            model.Update();
            RaiseEvent(new Event(new EventData(true)));

            return model;
        }
    }
}