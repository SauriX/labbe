using Service.Billing.Dtos.Facturapi;
using System.Threading.Tasks;

namespace Service.Billing.Client.IClient
{
    public interface IInvoiceClient
    {
        Task<FacturapiDto> GetInvoiceById(string facturapiId);
        Task<FacturapiDto> CreateInvoice(FacturapiDto invoice);
        Task<byte[]> GetInvoiceXML(string facturapiId);
    }
}
