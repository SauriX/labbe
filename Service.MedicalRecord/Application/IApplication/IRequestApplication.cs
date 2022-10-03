using Service.MedicalRecord.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IRequestApplication
    {
        Task<IEnumerable<ClinicResultsRequestDto>> GetByFilter(RequestFilterDto filter);
        Task<RequestDto> GetById(Guid recordId, Guid requestId);
        Task<RequestGeneralDto> GetGeneral(Guid recordId, Guid requestId);
        Task<RequestStudyUpdateDto> GetStudies(Guid recordId, Guid requestId);
        Task<IEnumerable<string>> GetImages(Guid recordId, Guid requestId);
        Task SendTestEmail(RequestSendDto requestDto);
        Task SendTestWhatsapp(RequestSendDto requestDto);
        Task<string> Create(RequestDto requestDto);
        Task<string> Convert(RequestConvertDto requestDto);
        Task UpdateGeneral(RequestGeneralDto requestDto);
        Task UpdateTotals(RequestTotalDto requestDto);
        Task UpdateStudies(RequestStudyUpdateDto requestDto);
        Task CancelRequest(Guid recordId, Guid requestId, Guid userId);
        Task CancelStudies(RequestStudyUpdateDto requestDto);
        Task<int> SendStudiesToSampling(RequestStudyUpdateDto requestDto);
        Task<int> SendStudiesToRequest(RequestStudyUpdateDto requestDto);
        Task AddPartiality(RequestPartialityDto requestDto);
        Task<byte[]> PrintTicket(Guid recordId, Guid requestId);
        Task<byte[]> PrintOrder(Guid recordId, Guid requestId);
        Task<byte[]> PrintTags(Guid recordId, Guid requestId, List<RequestTagDto> tags);
        Task<string> SaveImage(RequestImageDto requestDto);
        Task DeleteImage(Guid recordId, Guid requestId, string code);
    }
}
