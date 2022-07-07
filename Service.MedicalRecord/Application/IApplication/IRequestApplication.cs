using Service.MedicalRecord.Dtos.Request;
using System;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IRequestApplication
    {
        Task<byte[]> GetTicket();
        Task<string> Create(RequestDto request);
    }
}
