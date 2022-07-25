namespace Service.MedicalRecord.Dtos.Appointment
{
    public class AppointmentGeneral
    {
        public string procedencia {get; set;}
        public string compañia { get; set; }
        public string medico { get; set; }
        public string nomprePaciente { get; set; }
        public string observaciones { get; set; }
        public string tipoEnvio { get; set; }
        public string email { get; set; }
        public string whatssap { get; set; }
        public bool activo { get; set; }
    }
}
