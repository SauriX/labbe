namespace Service.MedicalRecord.Settings.ISettings
{
    public interface IQueueNames
    {
        public string Email { get; init; }
        public string Whatsapp { get; init; }
    }
}
