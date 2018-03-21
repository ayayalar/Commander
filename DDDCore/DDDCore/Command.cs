using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDCore.Domain;

namespace DDDCore
{
    public abstract class Command<T> : CommandCommon<T>, ICommand<T> where T : IAggregateRoot
    {
        public virtual void Handle()
        {
        }

        public Task HandleAsync() => Task.CompletedTask;
    }
    
    public abstract class CommandAsync<T> : CommandCommon<T>, ICommand<T> where T : IAggregateRoot
    {
        public void Handle()
        {
        }

        public abstract Task HandleAsync();
    }
    
    public abstract class CommandCommon<T> where T : IAggregateRoot
    {
        private readonly List<ICommand<T>> _nestedCommandList = new List<ICommand<T>>();
        private readonly List<IEvent> _events = new List<IEvent>();
        
        protected void AddEvent(IEvent @event) => _events.Add(@event);
        protected void AddCommand(Command<T> command) => _nestedCommandList.Add(command);      

        internal IEnumerable<ICommand<T>> CommandList => _nestedCommandList;
        internal bool NestedCommandExist => _nestedCommandList.Any();
        internal IEnumerable<IEvent> GetEvents() => _events;
        
        public T Model { get; internal set; }
        public bool IsEventHandler { get; protected set; }
        public Type HandlesEvent { get; protected set; }
        public IEvent Event { protected get; set; }
        public NestedCommandOrder NestedCommandOrder { get; protected set; }
    }

    public enum NestedCommandOrder
    {
        First,
        Last
    }
}