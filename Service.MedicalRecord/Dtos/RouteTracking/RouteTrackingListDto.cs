using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.RouteTracking
{
    public class RouteTrackingListDto
    {
        public Guid Id { get; set; }
        public string Seguimiento { get; set; }
        public string ClaveEtiqueta { get; set; }
        public string Recipiente { get; set; }
        public decimal Cantidad { get; set; }
        public string Estudios { get; set; }
        public string Solicitud { get; set; }
        public byte Estatus { get; set; }
        public string Ruta { get; set; }
        public string Entrega { get; set; }
        public Guid RutaId { get; set; }
    }
}
