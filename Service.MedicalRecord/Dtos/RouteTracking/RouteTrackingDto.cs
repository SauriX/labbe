using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Dtos.RouteTracking
{
    public class RouteTrackingDto
    {
        public Guid Id { get; set; }
        public string DestinoId { get; set; }
        public string OrigenId { get; set; }
        public string Clave { get; set; }
        public DateTime Recoleccion { get; set; }
        public Guid RutaId { get; set; }
        public Guid MuestraId { get; set; }
        public bool Escaneo { get; set; }
        public decimal Temperatura { get; set; }
        public bool Activo { get; set; }
    }
}
