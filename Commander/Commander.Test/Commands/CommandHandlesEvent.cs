using Commander.Test.Data;

namespace Commander.Test.Commands
{
    internal class CommandHandlesEvent : Command<Request, Model>
    {
        public static CommandHandlesEvent Instance() => new CommandHandlesEvent();
        public override void Init() => HandleEvent<Event>();

        public override Model Handle()
        {
            var @event = GetEvent<Event>();

            if (@event.Data.ModelUpdated) Model.Update();

            return Model;
        }
    }
}