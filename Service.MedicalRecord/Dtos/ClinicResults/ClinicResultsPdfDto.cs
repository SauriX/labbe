using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Dtos.ClinicResults
{
    public class PathologicalResultsDto
    {
        public List<ClinicResultsCaptureDto> CapturaResultados { get; set; }
        public ClinicResultsRequestDto SolicitudInfo { get; set; }
    }
}
