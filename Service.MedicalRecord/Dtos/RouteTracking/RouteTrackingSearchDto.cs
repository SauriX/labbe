﻿using System;

namespace Service.MedicalRecord.Dtos.RouteTracking
{
    public class RouteTrackingSearchDto
    {
        public DateTime[] Fechas { get; set; }
        public string Origen { get; set; }

        public string Destino { get; set; }
        public string Buscar { get; set; }
    }
}
