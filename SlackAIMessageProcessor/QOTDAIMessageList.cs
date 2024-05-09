namespace SlackAIMessageProcessor
{
    public class QOTDAIMessageList
    {
        public QOTDAIMessageList()
        {
            
        }
        public QOTDAIMessageList(SystemAIMessage message)
        {
            QOTDMessage = message;
        }
        public SystemAIMessage QOTDMessage { get; set; } = new SystemAIMessage();
        public List<UserAIMessage> QOTDResponses { get; set; } = new List<UserAIMessage>();
    }
}
