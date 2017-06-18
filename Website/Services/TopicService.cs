using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Configuration;

namespace Website.Services
{
    public class TopicService
    {
        private const string TopicName = "chat";
        private readonly TopicClient _topicClient;
        public TopicService()
        {
            var mngr = NamespaceManager.CreateFromConnectionString(ConfigurationManager.AppSettings.Get("ServiceBusConnectionString"));
            if (!mngr.TopicExists(TopicName))
            {
                var td = new TopicDescription(TopicName);
                td.MaxSizeInMegabytes = 5120;
                td.DefaultMessageTimeToLive = new TimeSpan(0, 0, 10);
                mngr.CreateTopic(TopicName);
            }

            if (!mngr.SubscriptionExists(TopicName, "all"))
            {
                mngr.CreateSubscription(TopicName, "all");
            }

            _topicClient = TopicClient.CreateFromConnectionString(ConfigurationManager.AppSettings.Get("ServiceBusConnectionString"), TopicName);
        }

        public void Send(string message)
        {
            _topicClient.Send(new BrokeredMessage(message));
        }

        public SubscriptionClient GetClient()
        {
            return SubscriptionClient.CreateFromConnectionString(ConfigurationManager.AppSettings.Get("ServiceBusConnectionString"), TopicName, "all");
        }
    }
}