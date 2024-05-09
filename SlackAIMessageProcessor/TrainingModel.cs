using Newtonsoft.Json;

namespace SlackAIMessageProcessor
{
    public class TrainingModel
    {
        public TrainingModel()
        {
            Messages = new();
        }

        [JsonProperty(PropertyName = "messages")]
        public List<AIMessage> Messages { get; set; }
    }
}
