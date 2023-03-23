using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.InvoiceCompany
{
    public class InvoiceGlobalResponseDto
    {
        public List<string> SolicitudesGeneradas { get; set; }
        public List<string> SolicitudesFallidas { get; set; }
    }
}
