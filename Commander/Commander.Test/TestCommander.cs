using System;
using Commander.Test.Commands;
using Commander.Test.Data;
using FluentAssertions;
using Xunit;

namespace Commander.Test
{
    public class TestCommander
    {
        [Fact]
        public void ShouldExecuteBasicCommand()
        {
            var request = new Request("Test");
            var commander = new Commander<Request, Model>(request);

            var model = commander.Execute(BasicCommand.Instance(Repo.Instance()));

            model.Name.Should().Be("Test");
            model.CreatedAt.Should().Be(DateTime.Today);
        }

        [Fact]
        public void ShouldRaiseAndHandleEvents()
        {
            var request = new Request("Test");
            var commander = new Commander<Request, Model>(request);

            var model = commander.Execute(CommandRaisesEvent.Instance(Repo.Instance()), CommandHandlesEvent.Instance());

            model.IsUpdated.Should().BeTrue();
            model.UpdatedAt.Should().Be(DateTime.Today);
        }

        [Fact]
        public void ShouldFailToExecuteOverridingModelWithNewInstance()
        {
            var request = new Request("Test");
            var commander = new Commander<Request, Model>(request);

            Action action = () => commander.Execute(BasicCommand.Instance(Repo.Instance()), CommandOverridesModel.Instance());

            action.Should().Throw<ApplicationException>().WithMessage("Model cannot be replaced with another instance.");
        }
    }
}