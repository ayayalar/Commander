using System;
using DDDCore.Domain;

namespace DDDCore.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var model = new AggregateRoot();

            try
            {
                var commandRunner = new CommandRunner<Context, AggregateRoot>(model);
                commandRunner.Handle(new UpdateNameEvent(),
                    new ErenEventHandler(),
                    new AylinEventHandler()
                );
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine(model.Name);
            }
            
            Console.WriteLine(model.Name);
        }
    }

    internal class UpdateNameEvent: Command<Context, AggregateRoot>
    {
        public override void Handle()
        {
            Context.AggregateRoot.SetName("Arif");
            EventConfiguration.RaiseEvent(new ErenEventArgs("Eren"));
        }
    }

    internal class ChildCommandOfAylin : Command<Context, AggregateRoot>
    {
        public override void Handle()
        {
            Context.AggregateRoot.SetName("Mhelissa");
        }
    }

    internal class ErenEventHandler: Command<Context, AggregateRoot>
    {
        public override void Init()
        {
            EventConfiguration.HandleEvent<ErenEventArgs>();            
        }

        public override void Handle()
        {
            var @event = EventConfiguration.GetEvent<ErenEventArgs>();
            Context.AggregateRoot.SetName(@event.Name);

            EventConfiguration.RaiseEvent(new AylinEventArgs("Aylin"));
        }
    }

    internal class AylinEventHandler : Command<Context, AggregateRoot>
    {
        public override void Init()
        {
            ChildCommandOrder = CommandOrder.Last;
            EventConfiguration.HandleEvent<AylinEventArgs>();
            AddChildCommand(new ChildCommandOfAylin());
        }

        public override void Handle() 
        {
            var @event = EventConfiguration.GetEvent<AylinEventArgs>();
            Context.AggregateRoot.SetName(@event.Name);
        }
    }

    internal class AggregateRoot : IAggregateRoot
    {
        public void SetName(string name)
        {
            Name = name;
        }
        public string Name { get; private set; }
    }

    internal class ErenEventArgs : IEvent
    {
        public ErenEventArgs()
        {
            
        }

        public ErenEventArgs(string name)
        {
            Name = name;
        }
        
        public string Name { get; }
    }

    internal class AylinEventArgs : IEvent
    {
        public AylinEventArgs()
        {

        }

        public AylinEventArgs(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }

    internal class Context : BoundedContext<AggregateRoot>
    {
        
    }
}