using System;

namespace Service.MedicalRecord.Dtos.TrackingOrder
{
    public class TrackingOrderFormDto
    {
        public Guid Id { get; set; }
        public string SucursalDestinoId { get; set; }
        public string SucursalOrigenId { get; set; }
        public string MuestraId { get; set; }
        public string SolicitudId { get; set; }
        public bool EscaneoCodigoBarras { get; set; }
        public double Temperatura { get; set; }
        public string ClaveEstudio { get; set; }
        public string Estudio { get; set; }
        public string PacienteId { get; set; }
        public bool Escaneado { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
