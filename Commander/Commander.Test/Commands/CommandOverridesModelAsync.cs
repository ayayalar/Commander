using System.Threading.Tasks;
using Commander.Test.Data;

namespace Commander.Test.Commands
{
    internal class CommandOverridesModelAsync : CommandAsync<Request, Model>
    {
        public static CommandOverridesModelAsync Instance() => new CommandOverridesModelAsync();

        public override Task<Model> HandleAsync()
        {
            return Task.FromResult(new Model("test"));
        }
    }
}