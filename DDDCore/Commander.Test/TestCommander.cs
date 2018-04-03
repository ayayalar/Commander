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
            var request = new Request() {For = nameof(TestCommand1)};
            var command = new TestCommand1();
            var commander = new Commander<Request, Model>(request);
            var model = commander.Execute(command);

            model.Should().NotBeNull();
            model.For.Should().Be(request.For);
        }

    }
}
