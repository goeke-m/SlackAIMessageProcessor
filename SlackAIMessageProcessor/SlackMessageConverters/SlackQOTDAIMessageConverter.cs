using Newtonsoft.Json;
using SlackAIMessageProcessor.Messages;
using System.Text;

namespace SlackAIMessageProcessor.SlackMessageConverters
{
    public class SlackQOTDAIMessageConverter : ISlackAIMessageConverter
    {
        private const string QOTDPrompt = "<!here> Standard QOTD: ";
        private const string QOTDExhaustPrompt = "<!here> Standard QOTD: We have exhausted the selection of unanswered questions. Please submit new questions.";
        private const string TrainingFileName = "TrainingFile.jsonl";
        private const string ValidationFileName = "ValidationFile.jsonl";

        public SlackQOTDAIMessageConverter()
        {
            SlackAIMessages = new List<AIMessageGroup>();
        }

        public IReadOnlyList<AIMessageGroup> SlackAIMessages { get; protected set; }

        public void ConvertSlackMessages(IEnumerable<SlackMessage> messages)
        {
            var groupedSlackMessages = new List<GroupedSlackMessages>();
            GroupedSlackMessages groupedSlackMessage = new();

            foreach (var message in messages.Where(x => !string.IsNullOrWhiteSpace(x.Text) && !x.Text.Contains(QOTDExhaustPrompt)))
            {
                if (message.Text.StartsWith(QOTDPrompt))
                {
                    groupedSlackMessage = new() { QOTDMessage = message };
                    groupedSlackMessages.Add(groupedSlackMessage);
                }
                else if (message.UserProfile != null)
                {
                    groupedSlackMessage.Responses.Add(message);
                }
            }

            var slackAIMessages = new List<AIMessageGroup>();

            foreach (var messageGroup in groupedSlackMessages.Where(x => x.Responses.Count > 0))
            {
                var messageWithoutPrompt = messageGroup.QOTDMessage.Text.Replace(QOTDPrompt, string.Empty);
                var qotdReformatted = messageWithoutPrompt.Substring(0, messageWithoutPrompt.IndexOf("?") + 1).Trim();
                var userAIMessage = new UserAIMessage(qotdReformatted);

                foreach (var message in messageGroup.Responses)
                {
                    var systemPrompt = $"{message.UserProfile.RealName.Replace('.', ' ').ToLower()} is a software developer responding to a question of the day slack post.";
                    slackAIMessages.Add(new AIMessageGroup(new SystemAIMessage(systemPrompt), userAIMessage, new AssistantAIMessage(message.Text ?? string.Empty)));
                }
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
