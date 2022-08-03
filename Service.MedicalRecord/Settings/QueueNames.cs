using Service.MedicalRecord.Settings.ISettings;

namespace Service.MedicalRecord.Settings
{
    public class QueueNames : IQueueNames
    {
        public string Email { get; init; }
        public string Whatsapp { get; init; }
    }
}
