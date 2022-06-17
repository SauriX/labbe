namespace Service.Catalog.Dtos.Configuration
{
    public class ConfigurationEmailDto
    {
        public string Correo { get; set; }
        public string Remitente { get; set; }
        public string Smtp { get; set; }
        public bool RequiereContraseña { get; set; }
        public string Contraseña { get; set; }
    }
}
