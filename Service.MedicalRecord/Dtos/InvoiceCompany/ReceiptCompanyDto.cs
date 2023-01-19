using Service.MedicalRecord.Dtos.Request;
using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.InvoiceCompany
{
    public class ReceiptCompanyDto : RequestTicketDto
    {
        public List<Guid> SolicitudesId { get; set; }

    }
}
