using Newtonsoft.Json;

namespace SlackAIMessageProcessor.Messages
{
    public class AIMessage
    {
        public AIMessage() : this(string.Empty) { }
        public AIMessage(string role) : this(role, string.Empty) { }
        public AIMessage(string role, string content)
        {
            Role = role;
            Content = content;
        }
        [JsonProperty(PropertyName = "role")]
        public string Role { get; set; }
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }
    }
}
