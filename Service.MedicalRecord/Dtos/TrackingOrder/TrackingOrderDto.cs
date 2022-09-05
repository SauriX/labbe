using System;

namespace Service.MedicalRecord.Dtos.TrackingOrder
{
    public class TrackingOrderDto
    {
        public Guid Id { get; set; }
        public string SucursalDestinoId { get; set; }
        public string SucursalOrigenId { get; set; }
        public int MaquiladorId { get; set; }
        public string Clave { get; set; }
        public DateTime DiaRecoleccion { get; set; }
        public string RutaId { get; set; }
        public string MuestraId { get; set; }
        public bool EscaneoCodigoBarras { get; set; }
        public double Temperatura { get; set; }
        public bool Activo { get; set; }
    }
}
