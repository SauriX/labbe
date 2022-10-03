using Service.MedicalRecord.Dtos;
using Service.MedicalRecord.Dtos.ClinicResults;
using Service.MedicalRecord.Dtos.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Client.IClient
{
    public interface IPdfClient
    {
        Task<byte[]> GenerateTicket(RequestOrderDto order);
        Task<byte[]> GenerateQuotation();
        Task<byte[]> GenerateOrder(RequestOrderDto order);
        Task<byte[]> GenerateTags(List<RequestTagDto> tags);
        Task<byte[]> GenerateLabResults(ClinicResultsPdfDto order);
    }
}
