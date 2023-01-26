using Service.MedicalRecord.Dtos.Invoice;
using Service.MedicalRecord.Dtos.InvoiceCompany;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Client.IClient
{
    public interface IBillingClient
    {
        Task<List<SeriesDto>> GetBranchSeries(Guid branchId, byte type);
        Task<InvoiceDto> CheckInPayment(InvoiceDto invoiceDto);
        Task<InvoiceDto> CheckInPaymentCompany(InvoiceDto invoiceDto);
        Task<byte[]> DownloadPDF(string invoiceId);
        Task<string> CancelInvoice(InvoiceCancelation invoiceDto);
        Task<List<InvoiceDto>> getAllInvoice();
    }
}
