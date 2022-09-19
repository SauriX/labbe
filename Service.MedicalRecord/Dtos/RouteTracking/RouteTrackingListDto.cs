using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.RouteTracking
{
    public class RouteTrackingListDto
    {
        public Guid Id { get; set; }
        public string Seguimiento { get; set; }
        public Guid Solicitud { get; set; }
        public string Clave { get; set; }
        public string Sucursal { get; set; }
        public DateTime Fecha { get; set; }
        public string Status { get; set; }
        public List<RouteTrackingStudyListDto> Estudios { get; set; }
    }
}
