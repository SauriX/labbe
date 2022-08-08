using Service.Sender.Settings.Interfaces;

namespace Service.Sender.Settings
{
    public class KeySettings : IKeySettings
    {
        public string MailKey { get; init; }
        public string Token { get; init; }
    }
}
