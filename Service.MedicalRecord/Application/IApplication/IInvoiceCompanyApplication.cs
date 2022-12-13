using Service.MedicalRecord.Dtos.InvoiceCompany;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IInvoiceCompanyApplication
    {
        Task<IEnumerable<InvoiceCompanyInfoDto>> GetByFilter(InvoiceCompanyFilterDto filter);
    }
}
