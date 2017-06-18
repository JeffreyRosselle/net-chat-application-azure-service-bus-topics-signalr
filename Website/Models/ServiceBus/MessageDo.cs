namespace Website.Models.ServiceBus
{
    public class MessageDo
    {
        public MessageDo(string message, string name)
        {
            Message = message;
            Name = name;
        }

        public string Message { get; set; }
        public string Name { get; set; }
    }
}