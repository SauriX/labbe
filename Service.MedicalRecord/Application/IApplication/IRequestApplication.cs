using Service.MedicalRecord.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IRequestApplication
    {
        Task AddPartiality(Guid requestId, bool apply, Guid userId);
        Task AddStudies(Guid requestId, List<RequestStudyDto> studiesDto);
        Task CancelStudies(Guid requestId, List<int> studiesIds);
        Task<RequestDto> Create(RequestDto requestDto);
        Task<byte[]> PrintOrder(Guid requestId);
        Task<byte[]> PrintTicket(Guid requestId);
        Task SaveImage(RequestImageDto requestImageDto);
        Task<int> SendStudiesToRequest(Guid requestId, List<int> studiesIds, Guid userId);
        Task<int> SendStudiesToSampling(Guid requestId, List<int> studiesIds, Guid userId);
        Task SendTestEmail(Guid requestId, string email);
        Task SendTestWhatsapp(Guid requestId, string phone);
        Task UpdateGeneral(RequestGeneralDto requestGeneralDto);
        Task UpdateTotals(RequestTotalDto requestTotalDto);
    }
}
