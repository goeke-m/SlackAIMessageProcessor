namespace SlackAIMessageProcessor
{
    public class GroupedSlackMessages
    {
        public GroupedSlackMessages()
        {
            Responses = new List<SlackMessage>();
        }

        public SlackMessage QOTDMessage { get; set; }
        public List<SlackMessage> Responses { get; set; }
    }
}
