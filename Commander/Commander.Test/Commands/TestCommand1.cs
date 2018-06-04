using Commander.Test.Data;
using Commander.Test.Events;

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

            RaiseEvent(new TestEvent(new Foo("Arif")));

            return model;
        }
    }
}