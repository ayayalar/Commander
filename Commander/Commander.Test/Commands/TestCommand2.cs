using Commander.Test.Data;

namespace Commander.Test.Commands
{
    internal class TestCommand2 : Command<Request, Model>
    {

        public override Model Handle()
        {
            Model.For = nameof(TestCommand2) ;
            Model.CreatedAt = Model.CreatedAt;

            return Model;
        }
    }
}