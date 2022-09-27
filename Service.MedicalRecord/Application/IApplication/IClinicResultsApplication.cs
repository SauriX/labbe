using Service.MedicalRecord.Dtos;
using Service.MedicalRecord.Dtos.ClinicResults;
using Service.MedicalRecord.Dtos.Request;
using Service.MedicalRecord.Dtos.RequestedStudy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IClinicResultsApplication
    {
        Task<List<ClinicResultsDto>> GetAll(RequestedStudySearchDto search);
        Task<(byte[] file, string fileName)> ExportList(RequestedStudySearchDto search);
        Task<IEnumerable<string>> GetImages(Guid recordId, Guid requestId);
        Task<int> UpdateStatus(List<ClinicResultsUpdateDto> requestDto);
        /*Task<byte[]> PrintOrder(Guid recordId, Guid requestId);*/
        Task<string> SaveImage(RequestImageDto requestDto);
        Task DeleteImage(Guid recordId, Guid requestId, string code);
        Task SendTestEmail(RequestSendDto requestDto);
        Task SendTestWhatsapp(RequestSendDto requestDto);
    }
}
