namespace SlackAIMessageProcessor.Messages
{
    public class AssistantAIMessage : AIMessage
    {
        public AssistantAIMessage() : base("assistant") { }
        public AssistantAIMessage(string content) : base("assistant", content) { }
    }
}
