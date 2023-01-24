using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.RouteTracking
{
    public class RouteTrackingDeliverListDto
    {
        public Guid Id { get; set; }
        public string Seguimiento { get; set; }
        public string Solicitud { get; set; }
        public string Clave { get; set; }
        public string Sucursal { get; set; }
        public List<RouteTrackingStudyListDto> Estudios { get; set; }
        public string Fecha { get; set; }
        public string Status { get; set; }
        public Guid rutaId { get; set; }
    }
}
