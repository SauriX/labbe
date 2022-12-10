using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.TrackingOrder
{
    public class EstudiosListDto
    {
        public int solicitud { get; set; }
        public Guid solicitudId { get; set; }
        public List<StudiesRequestRouteDto> Estudios { get; set; }
    }
}
