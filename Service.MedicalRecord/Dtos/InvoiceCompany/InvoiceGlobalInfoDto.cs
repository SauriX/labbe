using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.InvoiceCompany
{
    public class InvoiceGlobalInfoDto
    {
        public Guid SucursalId { get; set; }
        public List<Guid> SolicitudesId { get; set; }
    }
}
