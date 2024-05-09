using SlackAIMessageProcessor.Messages;

namespace SlackAIMessageProcessor.SlackMessageConverters
{
    public interface ISlackAIMessageConverter
    {
        IReadOnlyList<AIMessageGroup> SlackAIMessages { get; }

        void ConvertSlackMessages(IEnumerable<SlackMessage> messages);
        void CreateTrianingFiles();
    }
}