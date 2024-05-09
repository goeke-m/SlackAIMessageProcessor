using Newtonsoft.Json;

namespace SlackAIMessageProcessor.Messages
{
    public class AIMessageGroup
    {
        public AIMessageGroup(SystemAIMessage systemMessage, UserAIMessage userMessage, AssistantAIMessage assistantMessage)
        {
            var aIMessages = new List<AIMessage>();
            aIMessages.Add(systemMessage);
            aIMessages.Add(userMessage);
            aIMessages.Add(assistantMessage);
            Messages = aIMessages.AsReadOnly();
        }
        [JsonProperty(PropertyName = "messages")]
        public IReadOnlyList<AIMessage> Messages { get; set; }
    }
}
