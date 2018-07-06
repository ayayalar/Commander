using Commander.Common;

namespace Commander.Test.Events
{
    internal class EventModelUpdated : IEvent<Foo>
    {
        public EventModelUpdated(Foo foo)
        {
            Data = foo;
        }
        public Foo Data { get; }
    }

    internal class Foo
    {
        public Foo(string name)
        {
            Name = name;
        }
        public string Name { get; }
    }
}
