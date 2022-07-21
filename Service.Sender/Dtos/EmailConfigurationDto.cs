namespace Service.Sender.Dtos
{
    public class EmailConfigurationDto
    {
        public EmailConfigurationDto()
        {

        }

        public EmailConfigurationDto(string remitente, string correo, string smtp, bool requiereContraseña, string contraseña)
        {
            Remitente = remitente;
            Correo = correo;
            Smtp = smtp;
            RequiereContraseña = requiereContraseña;
            Contraseña = contraseña;
        }

        public string Remitente { get; set; }
        public string Correo { get; set; }
        public string Smtp { get; set; }
        public bool RequiereContraseña { get; set; }
        public string Contraseña { get; set; }
    }
}
