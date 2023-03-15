﻿using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.TrackingOrder
{
    public class EstudiosListDto
    {
        public int solicitud { get; set; }
        public Guid solicitudId { get; set; }
        public bool IsInRute { get; set; }
        public Guid orderId { get; set; }
        public bool IsExtra { get; set; }
        public StudyRouteDto Estudio { get; set; }
    }
}
