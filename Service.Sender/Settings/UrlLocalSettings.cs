using Service.Sender.Settings.Interfaces;

namespace Service.Sender.Settings
{
    public class UrlLocalSettings : IUrlLocalSettings
    {
        public string Layout { get; set; }
        public string SensitivesImages { get; set; }
        public string UsersImages { get; set; }
    }
}
