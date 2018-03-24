using System.Collections.Generic;
using System.Linq;
using DDDCore.Domain;

namespace DDDCore
{
    public abstract class CommandCommon<T> where T : IAggregateRoot
    {
        private readonly List<ICommand<T>> _nestedCommandList = new List<ICommand<T>>();

        protected void AddCommand(Command<T> command) => _nestedCommandList.Add(command);

        internal IEnumerable<ICommand<T>> CommandList => _nestedCommandList;
        internal bool NestedCommandExist => _nestedCommandList.Any();

        public T AggregateRoot { get; internal set; }
        public EventConfiguration EventConfiguration { get; } = new EventConfiguration();
        public NestedCommandOrder NestedCommandOrder { get; protected set; }
    }
}