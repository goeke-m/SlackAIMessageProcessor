using Newtonsoft.Json;

namespace SlackAIMessageProcessor
{
    public class SlackMessageLoader
    {
        public SlackMessageLoader(string directoryPath) => DirectoryInformation = new DirectoryInfo(directoryPath);

        public DirectoryInfo DirectoryInformation { get; }

        public IReadOnlyList<SlackMessage> SlackMessages { get; private set; }

        public void Load() 
        {
            var slackMessages = new List<SlackMessage>();

            foreach (var file in DirectoryInformation.EnumerateFiles("*", SearchOption.AllDirectories))
            {
                var fileContents = File.ReadAllText(file.FullName);

                if (string.IsNullOrWhiteSpace(fileContents))
                    continue;

                var json = JsonConvert.DeserializeObject<List<SlackMessage>>(fileContents);

                if (json == null)
                    continue;

                slackMessages.AddRange(json.ToList());                
            }

            SlackMessages = slackMessages.AsReadOnly();
        }
    }
}
