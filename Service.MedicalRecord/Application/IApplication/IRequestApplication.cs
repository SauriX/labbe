using Integration.WeeClinic.Dtos;
using Service.MedicalRecord.Dtos.Request;
using Service.MedicalRecord.Dtos.WeeClinic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IRequestApplication
    {
        Task<IEnumerable<RequestInfoDto>> GetByFilter(RequestFilterDto filter);
        Task<RequestDto> GetById(Guid recordId, Guid requestId);
        Task<RequestGeneralDto> GetGeneral(Guid recordId, Guid requestId);
        Task<RequestStudyUpdateDto> GetStudies(Guid recordId, Guid requestId);
        Task<IEnumerable<RequestPaymentDto>> GetPayments(Guid recordId, Guid requestId);
        Task<IEnumerable<string>> GetImages(Guid recordId, Guid requestId);
        Task SendTestEmail(RequestSendDto requestDto);
        Task SendTestWhatsapp(RequestSendDto requestDto);
        Task<string> Create(RequestDto requestDto);
        Task<string> CreateWeeClinic(RequestDto requestDto);
        Task<string> Convert(RequestConvertDto requestDto);
        Task<RequestPaymentDto> CreatePayment(RequestPaymentDto requestDto);
        Task UpdateGeneral(RequestGeneralDto requestDto);
        Task UpdateTotals(RequestTotalDto requestDto);
        Task UpdateStudies(RequestStudyUpdateDto requestDto);
        Task CancelRequest(Guid recordId, Guid requestId, Guid userId);
        Task CancelStudies(RequestStudyUpdateDto requestDto);
        Task<List<RequestPaymentDto>> CancelPayment(Guid recordId, Guid requestId, List<RequestPaymentDto> requestDto);
        Task<int> SendStudiesToSampling(RequestStudyUpdateDto requestDto);
        Task<int> SendStudiesToRequest(RequestStudyUpdateDto requestDto);
        Task AddPartiality(RequestPartialityDto requestDto);
        Task<byte[]> PrintTicket(Guid recordId, Guid requestId);
        Task<byte[]> PrintOrder(Guid recordId, Guid requestId);
        Task<byte[]> PrintTags(Guid recordId, Guid requestId, List<RequestTagDto> tags);
        Task<string> SaveImage(RequestImageDto requestDto);
        Task DeleteImage(Guid recordId, Guid requestId, string code);
        Task<WeeTokenValidationDto> SendCompareToken(RequestTokenDto requestDto, string actionCode);
        Task<WeeTokenVerificationDto> VerifyWeeToken(RequestTokenDto requestDto);
        Task<List<WeeServiceAssignmentDto>> AssignServices(Guid recordId, Guid requestId, Guid userId);
    }
}
