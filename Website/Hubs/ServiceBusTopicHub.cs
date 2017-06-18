using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using Website.Models.ServiceBus;
using System;
using Website.Services;

namespace Website.Hubs
{
    [HubName("ServiceBusTopicHub")]
    public class ServiceBusTopicHub : Hub
    {
        private readonly TopicService _topicService;

        public ServiceBusTopicHub()
        {
            _topicService = new TopicService();
        }

        public void Start()
        {
            try
            {
                _topicService.GetClient().OnMessageAsync(async brokerMessage =>
                {
                    try
                    {
                        var message = brokerMessage.GetBody<string>();
                        if (!string.IsNullOrEmpty(message))
                        {
                            var context = GlobalHost.ConnectionManager.GetHubContext<ServiceBusTopicHub>();
                            var obj = JsonConvert.DeserializeObject<MessageDo>(message);
                            await context.Clients.All.SendMessage(obj.Message, obj.Name);
                        }
                        brokerMessage.Complete();
                    }
                    catch (Exception e)
                    {
                        brokerMessage.Abandon();
                    }
                });
            }
            catch
            {
                Start();
            }
        }

        public void Send(string message, string name)
        {
            if (string.IsNullOrEmpty(message)) return;
            if (string.IsNullOrEmpty(name))
                name = "Anonymous";
            if ((message.Contains("<") && message.Contains(">")) || (name.Contains("<") && name.Contains(">"))) return;
            _topicService.Send(JsonConvert.SerializeObject(new MessageDo(message, name)));
        }
    }
}