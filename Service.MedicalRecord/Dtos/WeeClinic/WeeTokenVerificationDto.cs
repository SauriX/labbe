namespace Service.MedicalRecord.Dtos.WeeClinic
{
    public class WeeTokenVerificationDto
    {
        public string Mensaje { get; set; }
        public int OK { get; set; }
        public bool Verificado => OK == 1 && Mensaje != null && Mensaje.ToLower() == "ok";
    }
}
