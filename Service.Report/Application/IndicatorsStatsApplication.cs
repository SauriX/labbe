using ClosedXML.Excel;
using ClosedXML.Report;
using DocumentFormat.OpenXml.Bibliography;
using Service.Report.Application.IApplication;
using Service.Report.Client.IClient;
using Service.Report.Dictionary;
using Service.Report.Domain.Catalogs;
using Service.Report.Domain.Indicators;
using Service.Report.Domain.MedicalRecord;
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
using System.Data;
using System.Globalization;
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
            var samplings = await GetBySamplesCosts(search);

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
            var data = await _medicalRecordService.GetRequestByFilter(search);
            var servicesCost = await _catalogService.GetBudgetsByBranch(search.SucursalId);
            var budget = await _repository.GetBudgetByDate(search.FechaInicial, search.FechaFinal);

            var path = Assets.Indicators;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("TituloDiario", "Reporte Indicadores Diario");
            template.AddVariable("TituloSemanal", "Reporte Indicadores Semanal");
            template.AddVariable("TituloMensual", "Reporte Indicadores Mensual");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));

            var daily = template.Workbook.Worksheet("Diario");
            var weekly = template.Workbook.Worksheet("Semanal");
            var monthly = template.Workbook.Worksheet("Mensual");

            // Diario
            DailyData(data, servicesCost, budget, daily);

            // Semana
            WeeklyData(data, servicesCost, budget, weekly, search.FechaInicial);
            
            // Mes            
            MonthlyData(data, servicesCost, budget, monthly, search.FechaInicial);            

            template.Generate();
            template.Format();

            template.Workbook.CalculateMode = XLCalculateMode.Auto;

            return (template.ToByteArray(), "Reporte Indicadores.xlsx");
        }

        public async Task<(byte[] file, string fileName)> ExportServicesCost(ReportFilterDto search)
        {
            var servicesCost = await _catalogService.GetBudgetsByBranch(search.SucursalId);

            var path = Assets.ServicesCost;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Costos Fijos Mensual");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("CostoFijo", servicesCost);

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

            var stats = data.ToIndicatorsStatsDto().ToList();

            var missingSamplesCosts = new List<SamplesCostsDto>();

            foreach (var item in stats)
            {
                item.CostoFijo = servicesCost.Where(x => x.Sucursales.Contains(item.Sucursal)).Sum(x => x.CostoFijo);
                item.CostoReactivo = budget.Where(x => x.SucursalId == item.SucursalId).Sum(x => x.CostoReactivo);

                var sampleBudget = await _repository.GetSampleCostById(item.SucursalId, item.FechaAlta);
                
                if(sampleBudget != null)
                {
                    item.CostoTomaCalculado = sampleBudget.CostoToma;
                }
                else
                {
                    missingSamplesCosts.Add(new SamplesCostsDto
                    {
                        SucursalId = item.SucursalId,
                        Sucursal = item.Sucursal,
                        FechaAlta = item.FechaAlta,
                        CostoToma = 8.5m
                    });
                }
            }

            var newSamples = missingSamplesCosts.Select(x => new SamplesCosts
            {
                Id = Guid.NewGuid(),
                SucursalId = x.SucursalId,
                Sucursal = x.Sucursal,
                CostoToma = x.CostoToma,
                FechaAlta = x.FechaAlta,
            }).ToList();

            await _repository.CreateSamples(newSamples);

            var results = stats.ToTableIndicatorsStatsDto();

            return results;
        }

        public async Task<List<SamplesCostsDto>> GetBySamplesCosts(ReportFilterDto search)
        {
            var samples = await _repository.GetSamplesCostsByDate(search);

            return samples.ToSamplesCostsDto().ToList();
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

        public async Task UpdateSample(SamplesCostsDto sample)
        {
            var existing = await _repository.GetSampleCostById(sample.SucursalId, sample.FechaAlta);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedSample = sample.ToSampleUpdate(existing);

            await _repository.UpdateSamples(updatedSample);
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

        private DataTable GetTable(string date, List<Dictionary<string, object>> data)
        {
            DataTable table = new DataTable();
            table.Columns.Add(date, typeof(string));

            foreach (var item in data[0].Keys.Where(x => x != "NOMBRE"))
            {
                table.Columns.Add(item, typeof(double));
            }

            foreach (var item in data)
            {
                var rowData = new List<object>();

                foreach (var key in item.Keys)
                {
                    rowData.Add(item[key]);
                }

                table.Rows.Add(rowData.ToArray());
            }

            return table;
        }

        private static DateTime FirstDateOfWeek(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);

            int daysOffset = (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;

            DateTime firstMonday = jan1.AddDays(daysOffset);

            int firstWeek = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(jan1, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

            if (firstWeek <= 1)
            {
                weekOfYear -= 1;
            }

            return firstMonday.AddDays(weekOfYear * 7);
        }

        private void DailyData(List<RequestInfo> data, List<ServicesCost> servicesCost, List<Indicators> budget, IXLWorksheet daily)
        {
            List<IGrouping<DateTime, RequestInfo>> dailyList = data.GroupBy(x => x.Fecha.Date).OrderBy(x => x.Key).ToList();
            for (int i = 0; i < dailyList.Count; i++)
            {
                IGrouping<DateTime, RequestInfo> item = dailyList[i];

                DateTime date = item.Key;

                var stats = item.ToIndicatorsStatsDto().ToList();

                foreach (var item2 in stats)
                {
                    item2.CostoFijo = servicesCost.Where(x => x.Sucursales.Contains(item2.Sucursal)).Sum(x => x.CostoFijo);
                    item2.CostoReactivo = budget.Where(x => x.SucursalId == item2.SucursalId).Sum(x => x.CostoReactivo);
                }

                var results = stats.ToTableIndicatorsStatsDto();
                var dataTable = GetTable(date.ToString("d"), results);

                var tableWithData = daily.Cell(3 + (8 * i), 1).InsertTable(dataTable.AsEnumerable());

                var formRow = 3 + (8 * i) - 1;

                List<string> listD = results[0].Keys.Where(x => x != "NOMBRE").ToList();
                for (int dailyData = 0; dailyData < listD.Count; dailyData++)
                {
                    var colName = daily.Column(dailyData + 2).ColumnLetter();
                    var fr1 = formRow + 7;
                    daily.Cell(fr1, dailyData + 2).FormulaA1 = $"={colName}{fr1 - 4}-{colName}{fr1 - 3}-{colName}{fr1 - 2}-{colName}{fr1 - 1}";
                }
            }
        }
        
        private void WeeklyData(List<RequestInfo> data, List<ServicesCost> servicesCost, List<Indicators> budget, IXLWorksheet weekly, DateTime yearOfWeek)
        {
            List<IGrouping<int, RequestInfo>> weeklyList = data.GroupBy(i => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(i.Fecha, CalendarWeekRule.FirstDay, DayOfWeek.Monday)).OrderBy(x => x.Key).ToList();
            for (int i = 0; i < weeklyList.Count; i++)
            {
                IGrouping<int, RequestInfo> item = weeklyList[i];

                DateTime date = FirstDateOfWeek(yearOfWeek.Year, item.Key);

                var stats = item.ToIndicatorsStatsDto().ToList();

                foreach (var weeklyItem in stats)
                {
                    weeklyItem.CostoFijo = servicesCost.Where(x => x.Sucursales.Contains(weeklyItem.Sucursal)).Sum(x => x.CostoFijo);
                    weeklyItem.CostoReactivo = budget.Where(x => x.SucursalId == weeklyItem.SucursalId).Sum(x => x.CostoReactivo);
                }

                var results = stats.ToTableIndicatorsStatsDto();

                var dataTable = GetTable(date.ToString("d") + "-" + date.AddDays(6).ToString("d"), results);

                var tableWithData = weekly.Cell(3 + (8 * i), 1).InsertTable(dataTable.AsEnumerable());

                var formRow = 3 + (8 * i) - 1;

                List<string> listW = results[0].Keys.Where(x => x != "NOMBRE").ToList();
                for (int weeklyData = 0; weeklyData < listW.Count; weeklyData++)
                {
                    var colName = weekly.Column(weeklyData + 2).ColumnLetter();
                    var fr1 = formRow + 7;
                    weekly.Cell(fr1, weeklyData + 2).FormulaA1 = $"={colName}{fr1 - 4}-{colName}{fr1 - 3}-{colName}{fr1 - 2}-{colName}{fr1 - 1}";
                }
            }
        }
        
        private void MonthlyData(List<RequestInfo> data, List<ServicesCost> servicesCost, List<Indicators> budget, IXLWorksheet monthly, DateTime month)
        {
            var stats = data.ToIndicatorsStatsDto().ToList();

            foreach (var monthlyItem in stats)
            {
                monthlyItem.CostoFijo = servicesCost.Where(x => x.Sucursales.Contains(monthlyItem.Sucursal)).Sum(x => x.CostoFijo);
                monthlyItem.CostoReactivo = budget.Where(x => x.SucursalId == monthlyItem.SucursalId).Sum(x => x.CostoReactivo);
            }

            var results = stats.ToTableIndicatorsStatsDto();

            var dataTable = GetTable(month.ToString("MMMM yy"), results);

            var tableWithData = monthly.Cell(3, 1).InsertTable(dataTable.AsEnumerable());

            var formRow = 3 - 1;

            List<string> list = results[0].Keys.Where(x => x != "NOMBRE").ToList();
            for (int monthlyData = 0; monthlyData < list.Count; monthlyData++)
            {
                var colName = monthly.Column(monthlyData + 2).ColumnLetter();
                var fr1 = formRow + 7;
                monthly.Cell(fr1, monthlyData + 2).FormulaA1 = $"={colName}{fr1 - 4}-{colName}{fr1 - 3}-{colName}{fr1 - 2}-{colName}{fr1 - 1}";
            }
        }
        
        private void ServiceData(List<RequestInfo> data, List<ServicesCost> servicesCost, List<Indicators> budget, IXLWorksheet monthly, DateTime month)
        {
            var stats = servicesCost.ServicesCostGeneric().ToList();

            foreach (var monthlyItem in stats)
            {
                monthlyItem.CostoFijo = servicesCost.Where(x => x.Sucursales.Contains(monthlyItem.Sucursal)).Sum(x => x.CostoFijo);
                monthlyItem.CostoReactivo = budget.Where(x => x.SucursalId == monthlyItem.SucursalId).Sum(x => x.CostoReactivo);
            }

            var results = stats.ToTableIndicatorsStatsDto();

            var dataTable = GetTable(month.ToString("MMMM yy"), results);

            var tableWithData = monthly.Cell(3, 1).InsertTable(dataTable.AsEnumerable());

            var formRow = 3 - 1;

            List<string> list = results[0].Keys.Where(x => x != "NOMBRE").ToList();
            for (int monthlyData = 0; monthlyData < list.Count; monthlyData++)
            {
                var colName = monthly.Column(monthlyData + 2).ColumnLetter();
                var fr1 = formRow + 7;
                monthly.Cell(fr1, monthlyData + 2).FormulaA1 = $"={colName}{fr1 - 4}-{colName}{fr1 - 3}-{colName}{fr1 - 2}-{colName}{fr1 - 1}";
            }
        }
    }
}
