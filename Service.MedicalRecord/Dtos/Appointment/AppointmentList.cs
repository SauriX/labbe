using System;

namespace Service.MedicalRecord.Dtos.Appointment
{
    public class AppointmentList
    {
        public string Id { get; set; }
        public string NoSolicitud { get; set; }
        public DateTime Fecha { get; set; }
        public string Expediente { get; set; }
        public string Direccion { get; set; }
        public string Nombre { get; set; }
        public string Edad { get; set; }
        public string Sexo { get; set; }
        public int Tipo { get; set; }
    }
}
