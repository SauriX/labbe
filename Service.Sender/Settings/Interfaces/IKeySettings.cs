namespace Service.Sender.Settings.Interfaces
{
    public interface IKeySettings
    {
        public string MailKey { get; init; }
        public string Token { get; init; }
    }
}
