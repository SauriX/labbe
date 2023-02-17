using Microsoft.AspNetCore.Http;
using Service.Report.Dtos;
using Service.Report.Dtos.Indicators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Report.Application.IApplication
{
    public interface IIndicatorsStatsApplication
    {
        Task<List<Dictionary<string, object>>> GetByFilter(ReportFilterDto search);
        Task<List<SamplesCostsDto>> GetBySamplesCosts(ReportModalFilterDto search);
        Task<InvoiceServicesDto> GetServicesCosts(ReportModalFilterDto search);
        Task Create(IndicatorsStatsDto indicators);
        Task Update(IndicatorsStatsDto indicators);
        Task UpdateSample(SamplesCostsDto sample);
        Task UpdateService(UpdateServiceDto service);
        Task GetIndicatorForm(IndicatorsStatsDto indicators);
        Task SaveServiceFile(IFormFile archivo, Guid userId);
        Task<(byte[] file, string fileName)> ExportSamplingsCost(ReportModalFilterDto search);
        Task<(byte[] file, string fileName)> ExportServicesCost(ReportModalFilterDto search);
        Task<(byte[] file, string fileName)> ExportList(ReportFilterDto search);
        Task<(byte[] file, string fileName)> ExportServicesCostSample();
    }
}
