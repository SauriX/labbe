using ClosedXML.Excel;
using ClosedXML.Report;
using Service.Report.Application.IApplication;
using Service.Report.Client.IClient;
using Service.Report.Dictionary;
using Service.Report.Domain.Catalogs;
using Service.Report.Dtos;
using Service.Report.Dtos.Indicators;
using Service.Report.Mapper;
using Service.Report.Repository.IRepository;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Report.Application
{
    public class IndicatorsStatsApplication : BaseApplication, IIndicatorsStatsApplication
    {
        public readonly IReportRepository _repository;
        private readonly ICatalogClient _catalogService;
        private readonly IMedicalRecordClient _medicalRecordService;

        public IndicatorsStatsApplication(IReportRepository repository,
            IMedicalRecordClient medicalRecordService,
            ICatalogClient catalogService,
            IRepository<Branch> branchRepository) : base(branchRepository)
        {
            _catalogService = catalogService;
            _medicalRecordService = medicalRecordService;
            _repository = repository;
        }

        public async Task<(byte[] file, string fileName)> ExportSamplingsCost(ReportFilterDto search)
        {
            var samplings = await _medicalRecordService.GetRequestByFilter(search);

            var path = Assets.SamplingsCost;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Costo de Toma");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("CostosToma", samplings);

            template.Generate();

            var range = template.Workbook.Worksheet("CostosToma").Range("CostosToma");
            var table = template.Workbook.Worksheet("CostosToma").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return (template.ToByteArray(), "Costos de Toma.xlsx");
        }

        public Task<(byte[] file, string fileName)> ExportList(ReportFilterDto search)
        {
            throw new System.NotImplementedException();
        }

        public async Task<(byte[] file, string fileName)> ExportServicesCost(ReportFilterDto search)
        {
            var service = await _catalogService.GetBudgetsByBranch(search.SucursalId);

            var path = Assets.ServicesCost;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Costos Fijos Mensual");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("CostoFijo", service);

            template.Generate();

            var range = template.Workbook.Worksheet("CostoFijo").Range("CostoFijo");
            var table = template.Workbook.Worksheet("CostoFijo").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return (template.ToByteArray(), "Costos Fijos Mensual.xlsx");
        }

        public async Task<List<Dictionary<string, object>>> GetByFilter(ReportFilterDto search)
        {
            var data = await _medicalRecordService.GetRequestByFilter(search);
            var servicesCost = await _catalogService.GetBudgetsByBranch(search.SucursalId);

            List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();

            foreach(var item in servicesCost)
            {
                results = data.ToIndicatorsStatsDto(item.CostoFijo, item.CostoFijo);
            }

            return results;
        }

        public async Task<List<ServicesCostDto>> GetServicesCosts(ReportFilterDto search)
        {
            var servicesCost = await _catalogService.GetBudgetsByBranch(search.SucursalId);

            return servicesCost.ServicesCostGeneric();
        }
    }
}
