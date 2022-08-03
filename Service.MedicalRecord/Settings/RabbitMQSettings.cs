using Service.MedicalRecord.Settings.ISettings;

namespace Service.MedicalRecord.Settings
{
    public class RabbitMQSettings : IRabbitMQSettings
    {
        public string Host { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
    }
}
