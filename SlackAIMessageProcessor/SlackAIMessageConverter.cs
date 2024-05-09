namespace SlackAIMessageProcessor
{
    public class SlackAIMessageConverter
    {
        public SlackAIMessageConverter()
        {
            SlackAIMessages = new List<AIMessage>().AsReadOnly();
        }

        public IReadOnlyList<AIMessage> SlackAIMessages { get; protected set; }

        public virtual void ConvertSlackMessages(IEnumerable<SlackMessage> messages)
        {
            var slackAIMessages = new List<AIMessage>();

            foreach (var message in messages)
            {
                slackAIMessages.Add(new UserAIMessage(message.UserProfile.RealName ?? "user", message.Text ?? string.Empty));
            }

            SlackAIMessages = slackAIMessages.AsReadOnly();
        }
    }
}
