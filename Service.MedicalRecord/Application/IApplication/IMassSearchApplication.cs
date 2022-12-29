using Service.MedicalRecord.Dtos.MassSearch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IMassSearchApplication
    {
        Task<MassSearchInfoDto> GetByFilter(MassSearchFilterDto filter);
        Task<byte[]> DownloadResultsPdf(MassSearchFilterDto search);
        Task<List<RequestsInfoDto>> GetAllCaptureResults(DeliverResultsFilterDto search);
        Task<(byte[] file, string fileName)> ExportList(DeliverResultsFilterDto search);
    }
}
