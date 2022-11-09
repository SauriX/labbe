namespace Service.MedicalRecord.Dtos.WeeClinic
{
    public class WeeTokenValidationDto
    {
        public string Dato { get; set; }
        public string Mensaje { get; set; }

        public bool Enviado => Dato != null && Dato.ToLower() == "ok";
        public bool Validado => Dato != null && Dato.ToLower() == "si";
    }
}
