using Commander.Test.Commands;
using Commander.Test.Data;
using FluentAssertions;
using Xunit;

namespace Commander.Test
{
    public class TestCommander
    {
        [Fact]
        public void ShouldExecuteCommand()
        {
            var request = new Request();
            var command = new TestCommand1(new Repository());
            var commander = new Commander<Request, Model>(request);

            var model = commander.Execute(command, new TestCommand2());

            model.Should().NotBeNull();
            model.For.Should().Be(request.For);
            model.Name.Should().Be("Arif");
        }
    }
}