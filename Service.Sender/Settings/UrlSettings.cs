using Service.Sender.Settings.Interfaces;

namespace Service.Sender.Settings
{
    public class UrlSettings : IUrlSettings
    {
        public string Home { get; set; }
        public string Web { get; set; }
        public string Images { get; set; }
    }
}
