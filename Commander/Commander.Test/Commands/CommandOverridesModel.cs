using Commander.Test.Data;

namespace Commander.Test.Commands
{
    internal class CommandOverridesModel : Command<Request, Model>
    {
        public static CommandOverridesModel Instance() => new CommandOverridesModel();

        public override Model Handle()
        {
            return new Model(id: 2, "test");
        }
    }
}