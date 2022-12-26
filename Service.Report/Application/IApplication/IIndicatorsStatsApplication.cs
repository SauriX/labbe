using Service.Report.Dtos;
using Service.Report.Dtos.Indicators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Report.Application.IApplication
{
    public interface IIndicatorsStatsApplication
    {
        Task<List<Dictionary<string, object>>> GetByFilter(ReportFilterDto search);
        Task<List<ServicesCostDto>> GetServicesCosts(ReportFilterDto search);
        Task<(byte[] file, string fileName)> ExportSamplingsCost(ReportFilterDto search);
        Task<(byte[] file, string fileName)> ExportServicesCost(ReportFilterDto search);
        Task<(byte[] file, string fileName)> ExportList(ReportFilterDto search);
    }
}
