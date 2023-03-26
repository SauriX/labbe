using Service.MedicalRecord.Dtos.General;
using Service.MedicalRecord.Dtos.MassSearch;
using Service.MedicalRecord.Dtos.RequestedStudy;
using Service.MedicalRecord.Dtos.ResultValidation;
using Service.MedicalRecord.Dtos.Sampling;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IValidationApplication
    {
        Task<int> UpdateStatus(List<RequestedStudyUpdateDto> requestDto);
        Task<List<ValidationListDto>> GetAll(GeneralFilterDto search);
        Task<(byte[] file, string fileName)> ExportList(GeneralFilterDto search);
        Task<byte[] > SendResultFile(DeliverResultsStudiesDto estudios);
    }
}
