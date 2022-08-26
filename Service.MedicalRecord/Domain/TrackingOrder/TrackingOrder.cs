using System;

namespace Service.MedicalRecord.Domain.TrackingOrder
{
    public class TrackingOrder
    {
        public Guid Id { get; set; }
        public string SucursalDestinoId { get; set; }
        public string SucursalOrigenId { get; set; }
        public string MuestraId { get; set; }
        public string SolicitudId { get; set; }
        public bool EscaneoCodigoBarras { get; set; }
        public double Temperatura { get; set; }
        public string ClaveEstudio { get; set; } // verificar
        public string Estudio { get; set; }
        public string PacienteId { get; set; }
        public bool Escaneado { get; set; }
        public bool Activo { get; set; }
        public Guid? UsuarioCreoId { get; set; }
        public DateTime? FechaCreo { get; set; }
        public Guid? UsuarioModificoId { get; set; }
        public DateTime? FechaModifico { get; set; }
    }
}
