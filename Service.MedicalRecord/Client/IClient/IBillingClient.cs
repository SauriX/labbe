using Service.MedicalRecord.Dtos.Invoice;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Client.IClient
{
    public interface IBillingClient
    {
        Task<InvoiceDto> CheckInPayment(InvoiceDto invoiceDto);
        Task<InvoiceDto> CheckInPaymentCompany(InvoiceDto invoiceDto);
    }
}
