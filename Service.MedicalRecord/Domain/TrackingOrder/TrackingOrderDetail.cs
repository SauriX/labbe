using System;

namespace Service.MedicalRecord.Domain.TrackingOrder
{
    public class TrackingOrderDetail
    {
        public Guid Id { get; set; }
        public Guid SeguimientoId { get; set; }
        public virtual TrackingOrder Seguimiento { get; set; }
        public int EstudioId { get; set; }
        public string Estudio { get; set; }
        public Guid SolicitudId { get; set; }
        public virtual Domain.Request.Request Solicitud{ get; set; }
        public Guid ExpedienteId { get; set; }
        public string NombrePaciente { get; set; }
        public decimal Temperatura { get; set; }
        public bool Escaneado { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }
        public int SolicitudEstudioId { get; set; }
        public virtual Domain.Request.RequestStudy SolicitudEstudio { get; set; }
    }
}
