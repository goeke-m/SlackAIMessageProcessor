namespace SlackAIMessageProcessor.Messages
{
    public class UserAIMessage : AIMessage
    {
        public UserAIMessage() : base("user") { }
        public UserAIMessage(string content) : base("user", content) { }
    }
}
