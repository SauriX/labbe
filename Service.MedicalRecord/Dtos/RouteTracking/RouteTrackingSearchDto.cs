using System;

namespace Service.MedicalRecord.Dtos.RouteTracking
{
    public class RouteTrackingSearchDto
    {
        public DateTime[] Fechas { get; set; }

        public string Sucursal { get; set; }
        public string Buscar { get; set; }
    }
}
