using Service.Sender.Settings.Interfaces;

namespace Service.Sender.Settings
{
    public class UrlLocalSettings : IUrlLocalSettings
    {
        public string Layout { get; init; }
        public string Images { get; init; }
    }
}
