using Service.MedicalRecord.Dtos.Request;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Client.IClient
{
    public interface IPdfClient
    {
        Task<byte[]> GenerateTicket(RequestOrderDto order);
        Task<byte[]> GenerateQuotation();
        Task<byte[]> GenerateOrder(RequestOrderDto order);
    }
}
