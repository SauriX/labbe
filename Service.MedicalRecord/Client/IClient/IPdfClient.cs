using Service.MedicalRecord.Dtos.ClinicResults;
using Service.MedicalRecord.Dtos.DeliverOrder;
using Service.MedicalRecord.Dtos.PendingRecive;
using Service.MedicalRecord.Dtos.Quotation;
using Service.MedicalRecord.Dtos.Request;
using Service.MedicalRecord.Dtos.WorkList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Client.IClient
{
    public interface IPdfClient
    {
        Task<byte[]> GenerateTicket(RequestTicketDto ticket);
        Task<byte[]> GenerateInvoiceCompanyTicket(RequestTicketDto ticket);
        Task<byte[]> GenerateQuotation();
        Task<byte[]> GenerateOrder(RequestOrderDto order);
        Task<byte[]> GenerateTags(List<RequestTagDto> tags);
        Task<byte[]> GenerateLabResults(ClinicResultsPdfDto order);
        Task<byte[]> GeneratePathologicalResults(ClinicResultPathologicalPdfDto order);
        Task<byte[]> PendigForm(List<PendingReciveDto> order);
        Task<byte[]> GenerateWorkList(WorkListDto workList);
        Task<byte[]> MergeResults(ClinicResultPathologicalPdfDto order, ClinicResultsPdfDto labOrder);
        Task<byte[]> DeliverForm(DeliverOrderdDto order);
        Task<byte[]> PriceQuoteReport(PriceQuoteDto priceQuote);
    }
}
