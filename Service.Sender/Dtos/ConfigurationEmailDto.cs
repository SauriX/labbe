namespace Service.Sender.Dtos
{
    public class ConfigurationEmailDTO
    {
        public string Usuario { get; set; }
        public string Dominio { get; set; }
        public string Smtp { get; set; }
        public bool RequiereContraseña { get; set; }
        public string Contraseña { get; set; }
        public string Remitente { get; set; }
    }
}
