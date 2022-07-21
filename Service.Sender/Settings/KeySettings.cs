using Service.Sender.Settings.Interfaces;

namespace Service.Sender.Settings
{
    public class KeySettings : IKeySettings
    {
        public string MailPassword { get; set; }
        public string Token { get; set; }
    }
}
