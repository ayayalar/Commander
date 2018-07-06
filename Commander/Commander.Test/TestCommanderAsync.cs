using System;
using System.Threading.Tasks;
using Commander.Test.Commands;
using Commander.Test.Data;
using FluentAssertions;
using Xunit;

namespace Commander.Test
{
    public class TestCommanderAsync
    {
        [Fact]
        public async Task ShouldExecuteCommand()
        {
            var request = new Request("Test");
            var commander = new Commander<Request, Model>(request);

            var model = await commander.ExecuteAsync(BasicCommandAsync.Instance(Repo.Instance()));

            model.Name.Should().Be("Test");
            model.CreatedAt.Should().Be(DateTime.Today);
        }
    }
}