using Service.MedicalRecord.Dtos.InvoiceCatalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IInvoiceCatalogApplication
    {
        Task<List<InvoiceCatalogList>> getAll(InvoiceCatalogSearch search);
        Task<(byte[] file, string fileName)> ExportList(InvoiceCatalogSearch search);
    }
}
