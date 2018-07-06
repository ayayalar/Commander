namespace Commander
{
    public static class ErrorMessages
    {
        public const string OverrideWithNewInstance = "Model cannot be replaced with another instance.";
        public const string HandleEventIsNotRegistered = "Event is not handled. Must call HandleEvent<T> in Init method.";
    }
}
