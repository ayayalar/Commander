namespace Commander.Test.Data
{
    internal class EventData
    {
        public EventData(bool modelUpdated)
        {
            ModelUpdated = modelUpdated;
        }

        public bool ModelUpdated { get; set; }
    }
}
