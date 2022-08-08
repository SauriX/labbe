namespace Service.Sender.Settings
{
    public class RabbitMQSettings
    {
        public string Host { get; init; }
        public string VirtualHost { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
    }
}
