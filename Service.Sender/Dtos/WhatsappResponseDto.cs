namespace Service.Sender.Dtos
{
    public class WhatsappResponseDto
    {
        public string Id { get; set; }
        public bool Sent { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
    }
}
