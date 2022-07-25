using Service.MedicalRecord.Domain.PriceQuote;
using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Domain.Appointments
{
    public class AppointmentLab
    {
        public Guid Id { get; set; }
        public Guid? ExpedienteId { get; set; }
        public MedicalRecord.MedicalRecord? Expediente { get; set; }
        public string NombrePaciente { get; set; }
        public int Edad { get; set; }
        public string Direccion { get; set; }
        public string Procedencia { get; set; }
        public Guid CompaniaID { get; set; }
        public Guid MedicoID { get; set; }
      //  public string AfilacionID
        public string Genero { get; set; }
        public int Status { get; set; }
        public string Cita { get; set; }
        //Estatus_CitaId
        public string Email { get; set; }
        public string WhatsApp { get; set; }
        public Guid SucursalID { get; set; }
        public DateTime FechaCita { get; set; }
        public DateTime HoraCita { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid? UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
        public IEnumerable<CotizacionStudy>? Estudios { get; set; }
    }
}
