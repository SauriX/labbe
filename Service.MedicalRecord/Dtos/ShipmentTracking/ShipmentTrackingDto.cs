using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.ShipmentTracking
{
    public class ShipmentTrackingDto
    {
        public Guid Id { get; set; }
        public string SucursalOrigen { get; set; }
        public string SucursalDestino { get; set; }
        public string ResponsableOrigen { get; set; }
        public string ResponsableDestino { get; set; }
        public string Medioentrega { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public DateTime? HoraEnvio { get; set; }
        public DateTime? FechaEnestimada { get; set; }
        public DateTime? HoraEnestimada { get; set; }
        public DateTime? FechaEnreal { get; set; }
        public DateTime? HoraEnreal { get; set; }
        public List<ShipmentStudydto> Estudios { get; set; }
        public string Seguimiento { get; set; }
        public string Ruta { get; set; }
        public string Nombre { get; set; }
    }
}
