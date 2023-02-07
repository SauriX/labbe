using Service.MedicalRecord.Dtos.Invoice;
using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.InvoiceCompany
{
    public class InvoiceCompanyDto : InvoiceDto
    {

        public Guid? CompanyId { get; set; }
        public List<Guid> SolicitudesId { get; set; }
        public string TipoFactura { get; set; }
        public List<InvoiceCompanyStudiesInfoDto> Estudios { get; set; }

    }
}
