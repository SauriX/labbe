using Service.MedicalRecord.Domain.Request;
using System;

namespace Service.MedicalRecord.Domain.TrackingOrder
{
    public class TrackingOrderDetail
    {
        public Guid Id { get; set; }
        public Guid SeguimientoId { get; set; }
        public virtual TrackingOrder Seguimiento { get; set; }
        public int EtiquetaId { get; set; }
        public virtual RequestTag Etiqueta{ get; set; }
        public decimal Cantidad { get; set; }
        public Guid SolicitudId { get; set; }
        public virtual Request.Request Solicitud{ get; set; }
        public bool Escaneado { get; set; }
        public bool Extra { get; set; }
        public Guid ExpedienteId { get; set; }
        public string NombrePaciente { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid? UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }
}
