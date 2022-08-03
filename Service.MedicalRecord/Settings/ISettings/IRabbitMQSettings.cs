namespace Service.MedicalRecord.Settings.ISettings
{
    public interface IRabbitMQSettings
    {
        public string Host { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
    }
}
