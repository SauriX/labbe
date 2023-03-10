using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Dtos.RouteTracking
{
    public class TagRouteDto
    {
        public Guid Id { get; set; }
        public string ClaveEtiqueta { get; set; }
        public string Recipiente { get; set; }
        public decimal Cantidad { get; set; }
        public string Estudios { get; set; }
        public string Solicitud { get; set; }
        public string ClaveRuta { get; set; }
        public byte Estatus { get; set; }
        public bool Escaneo { get; set; }
    }
}
