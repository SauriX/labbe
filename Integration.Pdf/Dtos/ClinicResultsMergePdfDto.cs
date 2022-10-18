using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integration.Pdf.Dtos
{
    public class ClinicResultsMergePdfDto
    {
        public ClinicResultsPdfDto LabResults { get; set; }
        public PathologicalResultsDto PathologicalResults { get; set; }
    }
}