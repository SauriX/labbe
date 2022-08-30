using Service.MedicalRecord.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Dtos.RequestedStudy
{
    public class RequestedStudyUpdateDto
    {
        public Guid SolicitudId { get; set; }
        public List<int> EstudioId { get; set; }
    }
}
