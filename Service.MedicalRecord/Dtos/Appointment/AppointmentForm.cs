using Service.MedicalRecord.Dtos.Quotation;
using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.Appointment
{
    public class AppointmentForm
    {
        public string Id { get; set; }
        public string expediente { get; set; }
        public string expedienteid { get; set; }
        public string nomprePaciente { get; set; }
        public int edad { get; set; }
        public int cargo { get; set; }
        public int typo { get; set; }
        public DateTime fechaNacimiento { get; set; }
        //public List<QuotetPrice> estudy { get; set; }
        public AppointmentGeneral? generales { get; set; }
        public string Genero { get; set; }
        public Guid UserId { get; set; }
        public Guid SucursalId { get; set; }
        public string tipo { get; set; } 
        public GeneralDomicilio? generalesDom { get; set; }
        public int status { get; set; }
        public DateTime fecha { get; set; }
    }
}
