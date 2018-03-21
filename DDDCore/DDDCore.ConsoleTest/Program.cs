using System;
using System.Runtime.InteropServices.ComTypes;
using DDDCore.Domain;

namespace DDDCore.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var model = new Test();
            CommandRunner<Test> commandRunner = new CommandRunner<Test>(model);
            commandRunner.Handle(new UpdateName(), new MyEventHandler());
            
            Console.WriteLine(model.Name);
        }
    }

    class Test: IAggregateRoot
    {
        public string Name { get; set; }
    }

    class UpdateName: Command<Test>
    {
        public UpdateName()
        {
            NestedCommandOrder = NestedCommandOrder.Last;
        }
        public override void Handle()
        {
            Model.Name = "Arif";
            AddEvent(new MyEvent("Eren"));
            
        }
    }
    
    class MyEventHandler: Command<Test>
    {
        public MyEventHandler()
        {
            IsEventHandler = true;
            HandlesEvent = typeof(MyEvent);
        }
        public override void Handle()
        {

            Model.Name = (Event as MyEvent)?.Name;
        }
    }
    

    class MyEvent : IEvent
    {
        public MyEvent(string name)
        {
            Name = name;
        }
        public string Name { get; }
    }
}