using Commander.Test.Data;
using Commander.Test.Events;

namespace Commander.Test.Commands
{
    internal class TestCommand2 : Command<Request, Model>
    {
        public override void Init()
        {
            HandleEvent<TestEvent>();
        }

        public override Model Handle()
        {
            var testEvent = GetEvent<TestEvent>();
            Model.Name = testEvent.Data.Name;

            return Model;
        }
    }
}