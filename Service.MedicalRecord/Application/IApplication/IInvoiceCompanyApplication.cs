﻿using Service.MedicalRecord.Dtos.Invoice;
using Service.MedicalRecord.Dtos.InvoiceCompany;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IInvoiceCompanyApplication
    {
        Task<InvoiceCompanyInfoDto> GetByFilter(InvoiceCompanyFilterDto filter);
        Task<string> GetNextPaymentNumber(string serie);
        Task<InvoiceDto> CheckInPayment(InvoiceCompanyDto invoice);
    }
}
