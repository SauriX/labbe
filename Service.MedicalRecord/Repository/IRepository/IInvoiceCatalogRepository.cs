using Service.MedicalRecord.Dtos.InvoiceCatalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface IInvoiceCatalogRepository
    {
        Task<List<Domain.Request.Request>> GetNotas(InvoiceCatalogSearch search);
        Task<List<Domain.Request.Request>> GetSolicitudbyclave(List<string> clave);
    }
}
