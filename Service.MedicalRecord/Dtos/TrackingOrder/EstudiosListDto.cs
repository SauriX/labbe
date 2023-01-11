using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.TrackingOrder
{
    public class EstudiosListDto
    {
        public int solicitud { get; set; }
        public Guid solicitudId { get; set; }
        public StudiesRequestRouteDto Estudio { get; set; }
    }
}
