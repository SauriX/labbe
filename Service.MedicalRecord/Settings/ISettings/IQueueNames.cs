namespace Service.MedicalRecord.Settings.ISettings
{
    public interface IQueueNames
    {
        public string Branch { get; init; }
        public string Company { get; init; }
        public string Medic { get; init; }
        public string Email { get; init; }
        public string Whatsapp { get; init; }
    }
}
