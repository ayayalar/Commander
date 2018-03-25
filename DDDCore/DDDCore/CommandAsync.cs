using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDCore.Domain;

namespace DDDCore
{
    public abstract class CommandAsync<TContext, TAggregateRoot> : CommandCommon<TContext, TAggregateRoot>,
        ICommandAsync<TContext, TAggregateRoot>
        where TContext : BoundedContext<TAggregateRoot>, new() where TAggregateRoot : IAggregateRoot
    {
        private readonly List<ICommandAsync<TContext, TAggregateRoot>> _childCommands =
            new List<ICommandAsync<TContext, TAggregateRoot>>();

        protected void AddChildCommand(CommandAsync<TContext, TAggregateRoot> command) => _childCommands.Add(command);

        internal IEnumerable<ICommandAsync<TContext, TAggregateRoot>> ChildCommands => _childCommands;
        internal bool HasChildCommands => _childCommands.Any();

        public Task InitAsync() => Task.CompletedTask;

        public abstract Task HandleAsync();
    }
}