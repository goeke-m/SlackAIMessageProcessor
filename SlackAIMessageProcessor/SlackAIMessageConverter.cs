using Newtonsoft.Json;
using System.Text;

namespace SlackAIMessageProcessor
{
    public class SlackAIMessageConverter
    {
        private const string TrainingFileName = "TrainingFile.jsonl";
        private const string ValidationFileName = "ValidationFile.jsonl";

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

        public void CreateTrianingFiles()
        {
            var trainingData = new StringBuilder();
            var validationData = new StringBuilder();

            var trainingSetCount = (int)(SlackAIMessages.Count * 0.8);
            var validationSetCount = SlackAIMessages.Count - trainingSetCount;

            var trainingSet = SlackAIMessages.Take(trainingSetCount).ToList();
            var validationSet = SlackAIMessages.Skip(trainingSetCount).Take(validationSetCount).ToList();

            foreach (var message in trainingSet)
            {
                trainingData.AppendLine(JsonConvert.SerializeObject(message));
            }

            foreach (var message in validationSet)
            {
                validationData.AppendLine(JsonConvert.SerializeObject(message));
            }

            File.Delete(TrainingFileName);
            File.Delete(ValidationFileName);

            File.WriteAllText(TrainingFileName, trainingData.ToString());
            File.WriteAllText(ValidationFileName, validationData.ToString());
        }
    }
}
