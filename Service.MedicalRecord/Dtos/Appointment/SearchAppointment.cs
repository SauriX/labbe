using System;

namespace Service.MedicalRecord.Dtos.Appointment
{
    public class SearchAppointment
    {
        public DateTime[] fecha { get; set; }
        public string nombre { get; set; }

        public string tipo { get; set; }
    }
}
