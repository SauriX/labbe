using Service.MedicalRecord.Dtos.Sampling;
using System.Collections.Generic;
using System;

namespace Service.MedicalRecord.Dtos.RequestedStudy
{
    public class RequestedStudyUpdateDto
    {
        public List<int> EstudioId { get; set; }
        public Guid SolicitudId { get; set; }
        public string Usuario { get; set; }
        public Guid RuteOrder { get; set; }
    }
}
