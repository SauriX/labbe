namespace Service.Sender.Domain.EmailConfiguration
{
    public class EmailConfiguration
    {
        public byte Id { get; set; }
        public string Remitente { get; set; }
        public string Correo { get; set; }
        public string Smtp { get; set; }
        public bool RequiereContraseña { get; set; }
        public string Contraseña { get; set; }
    }
}
