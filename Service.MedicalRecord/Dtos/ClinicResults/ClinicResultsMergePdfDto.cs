using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Dtos.ClinicResults
{
    public class ClinicResultsMergePdfDto
    {
        public ClinicResultsPdfDto LabResults { get; set; }
        public ClinicResultPathologicalPdfDto PathologicalResults { get; set; }
    }
}
