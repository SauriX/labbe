using Service.MedicalRecord.Dtos.General;
using Service.MedicalRecord.Dtos.MassSearch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IMassSearchApplication
    {
        Task<MassSearchInfoDto> GetByFilter(GeneralFilterDto filter);
        Task<byte[]> DownloadResultsPdf(GeneralFilterDto search);
        Task<List<RequestsInfoDto>> GetAllCaptureResults(GeneralFilterDto search);
        Task<(byte[] file, string fileName)> ExportList(GeneralFilterDto search);
    }
}
