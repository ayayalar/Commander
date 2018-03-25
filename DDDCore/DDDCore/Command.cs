using System.Collections.Generic;
using System.Linq;
using DDDCore.Domain;

namespace DDDCore
{
    public abstract class Command<TContext, TAggregateRoot> : CommandCommon<TContext, TAggregateRoot>,
        ICommand<TContext, TAggregateRoot>
        where TContext : BoundedContext<TAggregateRoot>, new() where TAggregateRoot : IAggregateRoot
    {
        private readonly List<ICommand<TContext, TAggregateRoot>> _childCommands =
            new List<ICommand<TContext, TAggregateRoot>>();

        protected void AddChildCommand(Command<TContext, TAggregateRoot> command) => _childCommands.Add(command);

        internal IEnumerable<ICommand<TContext, TAggregateRoot>> ChildCommands => _childCommands;
        internal bool HasChildCommands => _childCommands.Any();

        public virtual void Init()
        {
        }

        public virtual void Handle()
        {
        }
    }
}