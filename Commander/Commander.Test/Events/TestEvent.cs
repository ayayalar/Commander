using Commander.Common;

namespace Commander.Test.Events
{
    internal class TestEvent : IEvent<Foo>
    {
        public TestEvent(Foo foo)
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
