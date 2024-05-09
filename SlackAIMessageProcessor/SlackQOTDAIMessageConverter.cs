using Newtonsoft.Json;
using System.Text;

namespace SlackAIMessageProcessor
{
    public class SlackQOTDAIMessageConverter
    {
        private const string QOTDPrompt = "<!here> Standard QOTD: ";
        private const string QOTDExhaustPrompt = "<!here> Standard QOTD: We have exhausted the selection of unanswered questions. Please submit new questions.";
        private const string TrainingFileName = "TrainingFile.jsonl";
        private const string ValidationFileName = "ValidationFile.jsonl";

        public IReadOnlyList<QOTDAIMessageList> SlackAIMessages { get; set; } = new List<QOTDAIMessageList>();

        public void ConvertSlackMessages(IEnumerable<SlackMessage> messages)
        {
            var slackMessages = new List<QOTDAIMessageList>();
            QOTDAIMessageList qotdAIMessageList = new();

            foreach (var message in messages.Where(x => !string.IsNullOrWhiteSpace(x.Text) && !x.Text.Contains(QOTDExhaustPrompt)))
            {
                if (message.Text.StartsWith(QOTDPrompt))
                {
                    var messageWithoutPrompt = message.Text.Replace(QOTDPrompt, string.Empty);
                    var qotdReformatted = messageWithoutPrompt.Substring(0, messageWithoutPrompt.IndexOf("?") + 1).Trim();
                    qotdAIMessageList = new(new SystemAIMessage(qotdReformatted));
                    slackMessages.Add(qotdAIMessageList);
                }
                else if (message.UserProfile != null)
                {
                    qotdAIMessageList?.QOTDResponses.Add(new UserAIMessage(message.UserProfile.RealName.Replace(".", " ") ?? "user", message.Text ?? string.Empty));
                }
            }

            SlackAIMessages = slackMessages.AsReadOnly();
        }

        public void CreateTrianingFiles()
        {
            var trainingData = new StringBuilder();
            var validationData = new StringBuilder();

            var messages = SlackAIMessages.Where(x => x.QOTDResponses.Count > 0).ToList();

            var trainingSetCount = (int)(messages.Count * 0.8);
            var validationSetCount = messages.Count - trainingSetCount;

            var trainingSet = messages.Take(trainingSetCount).ToList();
            var validationSet = messages.Skip(trainingSetCount).Take(validationSetCount).ToList();

            foreach (var message in trainingSet)
            {
                var trainingModel = new TrainingModel();
                trainingModel.Messages.Add(message.QOTDMessage);
                trainingModel.Messages.AddRange(message.QOTDResponses);
                trainingData.AppendLine(JsonConvert.SerializeObject(trainingModel));
            }

            foreach (var message in validationSet)
            {
                var trainingModel = new TrainingModel();
                trainingModel.Messages.Add(message.QOTDMessage);
                trainingModel.Messages.AddRange(message.QOTDResponses);
                validationData.AppendLine(JsonConvert.SerializeObject(trainingModel));
            }

            File.Delete(TrainingFileName);
            File.Delete(ValidationFileName);

            File.WriteAllText(TrainingFileName, trainingData.ToString());
            File.WriteAllText(ValidationFileName, validationData.ToString());
        }
    }
}
