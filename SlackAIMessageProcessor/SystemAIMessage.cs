namespace SlackAIMessageProcessor
{
    public class SystemAIMessage : AIMessage
    {
        public SystemAIMessage() : base("system") { }
        public SystemAIMessage(string content) : base("system", content) { }
    }
}
