using Service.MedicalRecord.Domain.PriceQuote;
using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Domain.Appointments
{
    public class AppointmentDom
    {
        public Guid Id { get; set; }
        public Guid? ExpedienteId { get; set; }
        public MedicalRecord.MedicalRecord? Expediente  { get; set; }
        public string NombrePaciente    { get; set; }
        //EquipoId
        //UsuarioID
        //RecolectorID
        public string Estatus_Cita { get; set; }
        public string Direccion { get; set; }
        public string Celular { get; set; }
        public string Genero { get; set; }
        public string Email { get; set; }
        public string WhatsApp { get; set; }
        public string Indicaciones { get; set; }
        //SolicitudId
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
