// See https://aka.ms/new-console-template for more information
using SlackAIMessageProcessor;
using SlackAIMessageProcessor.SlackMessageConverters;

var slackMessageLoader = new SlackMessageLoader("C:\\Users\\User\\Downloads\\qotd");
var slackMessageProcessor = new SlackQOTDAIMessageConverter();

slackMessageLoader.Load();
slackMessageProcessor.ConvertSlackMessages(slackMessageLoader.SlackMessages);
slackMessageProcessor.CreateTrianingFiles();

