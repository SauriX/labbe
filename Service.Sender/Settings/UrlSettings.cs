using Service.Sender.Settings.Interfaces;

namespace Service.Sender.Settings
{
    public class UrlSettings : IUrlSettings
    {
        public string Home { get; init; }
        public string Web { get; init; }
        public string Images { get; init; }
    }
}
