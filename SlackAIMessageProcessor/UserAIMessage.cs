namespace SlackAIMessageProcessor
{
    public class UserAIMessage : AIMessage
    {
        public UserAIMessage(string userName) : base(userName) { }
        public UserAIMessage(string userName, string content) : base(userName, content) { }
    }
}
