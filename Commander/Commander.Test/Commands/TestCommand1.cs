using Commander.Test.Data;

namespace Commander.Test.Commands
{
    internal class TestCommand1 : Command<Request, Model>
    {
        private readonly Repository _repo;

        public TestCommand1(Repository repo)
        {
            _repo = repo;
        }
        public override Model Handle()
        {
            var model = _repo.GetModel();
            model.For = Request.For;
            model.CreatedAt = Request.CreatedAt;

            return model;
        }
    }
}