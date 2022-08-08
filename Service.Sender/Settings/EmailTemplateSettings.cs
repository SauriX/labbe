using Service.Sender.Settings.Interfaces;

namespace Service.Sender.Settings
{
    public class EmailTemplateSettings : IEmailTemplateSettings
    {
        public string PrimaryColor { get; init; }
        public string BackgroundColor { get; init; }
    }
}
