using ClosedXML.Excel;
using ClosedXML.Report;
using Service.Report.Application.IApplication;
using Service.Report.Client.IClient;
using Service.Report.Dictionary;
using Service.Report.Domain.Catalogs;
using Service.Report.Domain.Indicators;
using Service.Report.Dtos;
using Service.Report.Dtos.Indicators;
using Service.Report.Mapper;
using Service.Report.Repository.IRepository;
using Shared.Dictionary;
using Shared.Error;
using Shared.Extensions;
using Shared.Helpers;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public async Task<(byte[] file, string fileName)> ExportList(ReportFilterDto search)
        {
            var service = await _catalogService.GetBudgetsByBranch(search.SucursalId);

            List<string> sucursales = new List<string>()
            {
                "U200",
                "U300",
                "Cumbres"
            };

            List<string> pacientes = new List<string>()
            {
                "PACIENTES",
                "90",
                "12",
                "30"
            };

            List<string> ingresos = new List<string>()
            {
                "INGRESOS",
                "900",
                "1200",
                "3000"
            };

            List<object> data = new List<object>()
            {
                pacientes,
                ingresos
            };

            var path = Assets.Indicators;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Costos Fijos Mensual");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Sucursales", sucursales);
            template.AddVariable("Indicadores_Mensuales", data);
            template.AddVariable("Dia", "Diciembre 2022");

            template.Format();

            return (template.ToByteArray(), "Reporte Indicadores.xlsx");
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
            var budget = await _repository.GetBudgetByDate(search.FechaInicial, search.FechaFinal);

            //List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();

            var stats = data.ToIndicatorsStatsDto().ToList();

            foreach (var item in stats)
            {
                item.CostoFijo = servicesCost.Where(x => x.Sucursales.Contains(item.Sucursal)).Sum(x => x.CostoFijo);
                item.CostoReactivo = budget.Where(x => x.SucursalId == item.SucursalId).Sum(x => x.CostoReactivo);
            }

            var results = stats.ToTableIndicatorsStatsDto();

            return results;
        }

        public async Task<List<ServicesCostDto>> GetServicesCosts(ReportFilterDto search)
        {
            var servicesCost = await _catalogService.GetBudgetsByBranch(search.SucursalId);

            return servicesCost.ServicesCostGeneric();
        }

        public async Task Create(IndicatorsStatsDto indicators)
        {
            if (indicators.CostoFijo <= 0)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            var newIndicator = indicators.ToModelCreate();

            await _repository.CreateIndicators(newIndicator);
        }

        public async Task Update(IndicatorsStatsDto indicators)
        {
            var existing = await _repository.GetIndicatorById(indicators.Id, indicators.FechaAlta);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedIndicator = indicators.ToModelUpdate(existing);

            await _repository.UpdateIndicators(updatedIndicator);

        }

        public async Task GetIndicatorForm(IndicatorsStatsDto indicators)
        {
            try
            {
                var existing = await _repository.GetIndicatorById(indicators.SucursalId, indicators.FechaAlta);

                if (existing == null)
                {
                    var newIndicator = indicators.ToModelCreate();
                    await _repository.CreateIndicators(newIndicator);
                }
                else
                {
                    var updatedIndicator = indicators.ToModelUpdate(existing);
                    await _repository.UpdateIndicators(updatedIndicator);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
