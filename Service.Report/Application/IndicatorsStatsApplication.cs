using ClosedXML.Excel;
using ClosedXML.Report;
using ClosedXML.Report.Utils;
using DocumentFormat.OpenXml.Bibliography;
using MassTransit.Logging;
using Microsoft.AspNetCore.Http;
using MoreLinq;
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

        public async Task<(byte[] file, string fileName)> ExportSamplingsCost(ReportModalFilterDto search)
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
            var reportFilter = new ReportModalFilterDto
            {
                SucursalId = search.SucursalId,
            };

            var data = await _medicalRecordService.GetRequestByFilter(search);
            var servicesCost = await _catalogService.GetBudgetsByBranch(reportFilter);
            var budget = await _repository.GetBudgetByDate(search.FechaInicial, search.FechaFinal);
            var samplesCost = await _repository.GetSamplesByDate(search.FechaFinal, search.FechaFinal);

            var path = Assets.Indicators;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("TituloDiario", "Reporte Indicadores Diario");
            template.AddVariable("TituloSemanal", "Reporte Indicadores Semanal");
            template.AddVariable("TituloMensual", "Reporte Indicadores Mensual");

            var dateOfFilter = search.FechaInicial.ToString("dd/MM/yyyy") + " AL " + search.FechaFinal.ToString("dd/MM/yyyy");
            template.AddVariable("Fecha", $"{dateOfFilter}");

            int daysInMonth = DateTime.DaysInMonth(year: search.FechaFinal.Year, month: search.FechaFinal.Month);
            var firstDayOfMonth = new DateTime(search.FechaFinal.Year, search.FechaFinal.Month, 1).ToString("dd/MM/yyyy");
            var lastDayOfMonth = new DateTime(search.FechaFinal.Year, search.FechaFinal.Month, daysInMonth).ToString("dd/MM/yyyy");
            template.AddVariable("FechaMensual", $"{firstDayOfMonth} AL {lastDayOfMonth}");

            var daily = template.Workbook.Worksheet("Diario");
            var weekly = template.Workbook.Worksheet("Semanal");
            var monthly = template.Workbook.Worksheet("Mensual");

            DailyData(data, servicesCost, budget, samplesCost, daily);

            WeeklyData(data, servicesCost, budget, samplesCost, weekly, search.FechaInicial);

            await MonthlyData(data, servicesCost, budget, samplesCost, monthly, search.FechaInicial);

            template.Generate();
            template.Format();

            template.Workbook.CalculateMode = XLCalculateMode.Auto;

            return (template.ToByteArray(), "Reporte Indicadores.xlsx");
        }

        public async Task<(byte[] file, string fileName)> ExportServicesCost(ReportModalFilterDto search)
        {
            var servicesCost = await _catalogService.GetBudgetsByBranch(search);

            var path = Assets.ServicesCost;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Costos Fijos Mensual");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("CostoFijo", servicesCost);

            var service = template.Workbook.Worksheet("CostoFijo");
            ServiceData(servicesCost, service);

            template.Generate();
            template.Format();

            return (template.ToByteArray(), "Costos Fijos Mensual.xlsx");
        }

        public async Task<(byte[] file, string fileName)> ExportServicesCostSample()
        {
            var path = Assets.ServicesCostExample;
            var template = new XLTemplate(path);

            template.Generate();
            template.Format();

            return (template.ToByteArray(), "Costos Fijos Ejemplo.xlsx");
        }

        public async Task SaveServiceFile(IFormFile archivo, Guid userId)
        {
            XLWorkbook serviceWorKbook = new XLWorkbook(archivo.OpenReadStream());
            IXLWorksheet serviceSheet = serviceWorKbook.Worksheet(1);

            var rows = serviceSheet.RangeUsed().RowsUsed();
            var columns = serviceSheet.RangeUsed().ColumnsUsed();

            List<BudgetFormDto> budget = new();

            var serviceName = new BudgetFormDto();

            foreach (var row in rows)
            {
                var rowNumber = row.RowNumber();

                foreach (var column in columns)
                {
                    if (column.Cell(1).IsEmpty())
                    {
                        continue;
                    }

                    var colNumber = column.ColumnNumber() - 1;
                    if (!row.Cell(colNumber).IsEmpty() && rowNumber > 2)
                    {
                        if (colNumber > 1)
                        {
                            var branchName = column.Cell(1).GetString();
                            var getBranch = await _catalogService.GetBranchByName(branchName);

                            serviceName = new BudgetFormDto
                            {
                                NombreServicio = row.Cell(1).GetString(),
                                CostoFijo = (decimal)row.Cell(colNumber).GetDouble(),
                                Sucursal = new BudgetBranchListDto
                                {
                                    Ciudad = getBranch.Ciudad,
                                    SucursalId = Guid.Parse(getBranch.IdSucursal)
                                },
                            };
                        }

                        budget.Add(serviceName);
                    }
                }
            }

            var list = budget.GroupBy(x => new { x.CostoFijo, x.NombreServicio }).ToList();

            List<BudgetFormDto> toBudgetForm = new();
            foreach (var item in list)
            {
                toBudgetForm.Add(new BudgetFormDto
                {
                    Id = 0,
                    Clave = item.First().NombreServicio,
                    CostoFijo = item.First().CostoFijo,
                    NombreServicio = item.First().NombreServicio,
                    Activo = true,
                    Fecha = DateTime.Now,
                    UsuarioId = userId,
                    Sucursales = item.Select(x => x.Sucursal).ToList()
                });
            }

            try
            {
                await _catalogService.CreateList(toBudgetForm);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Dictionary<string, object>>> GetByFilter(ReportFilterDto search)
        {
            var reportFilter = new ReportModalFilterDto
            {
                SucursalId = search.SucursalId,
            };

            var data = await _medicalRecordService.GetRequestByFilter(search);
            var servicesCost = await _catalogService.GetBudgetsByBranch(reportFilter);
            var budget = await _repository.GetBudgetByDate(search.FechaInicial, search.FechaFinal);

            var records = data.GroupBy(x => x.Expediente).Count();

            var stats = data.ToIndicatorsStatsDto().ToList();

            var missingSamplesCosts = new List<SamplesCostsDto>();

            foreach (var item in stats)
            {
                item.CostoFijo = servicesCost.Where(x => x.Sucursales.Contains(item.Sucursal)).Sum(x => x.CostoFijo);
                item.CostoReactivo = budget.Where(x => x.SucursalId == item.SucursalId).Sum(x => x.CostoReactivo);

                var city = await _catalogService.GetBranchByName(item.Sucursal);

                var sampleBudget = await _repository.GetSampleCostById(item.SucursalId, search.FechaIndividual);

                if (sampleBudget != null)
                {
                    item.CostoTomaCalculado = item.Expedientes * sampleBudget.CostoToma;
                }
                else
                {
                    missingSamplesCosts.Add(new SamplesCostsDto
                    {
                        SucursalId = item.SucursalId,
                        Sucursal = item.Sucursal,
                        FechaAlta = search.FechaIndividual,
                        CostoToma = 8.5m,
                        Ciudad = city.Ciudad,
                        FechaMod = DateTime.Now
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
                FechaModificacion = x.FechaMod,
                Ciudad = x.Ciudad
            }).ToList();

            await _repository.CreateSamples(newSamples);

            if (missingSamplesCosts != null || missingSamplesCosts.Count > 0)
            {
                foreach (var item in stats)
                {
                    var sampleBudget = await _repository.GetSampleCostById(item.SucursalId, search.FechaIndividual);

                    if (sampleBudget != null)
                    {
                        item.CostoTomaCalculado = item.Expedientes * sampleBudget.CostoToma;
                    }
                }
            }

            var results = stats.ToTableIndicatorsStatsDto();

            return results;
        }

        public async Task<List<SamplesCostsDto>> GetBySamplesCosts(ReportModalFilterDto search)
        {
            var samples = await _repository.GetSamplesCostsByFilter(search);

            return samples.ToSamplesCostsDto().ToList();
        }

        public async Task<InvoiceServicesDto> GetServicesCosts(ReportModalFilterDto search)
        {
            var servicesCost = await _catalogService.GetServiceCostByBranch(search);

            return servicesCost.ToServiceCostGroupDto();
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

        public async Task UpdateService(UpdateServiceDto service)
        {
            await _catalogService.UpdateService(service);
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

        private void DailyData(List<RequestInfo> data, List<ServicesCost> servicesCost, List<Indicators> budget, List<SamplesCosts> samplesCost, IXLWorksheet daily)
        {
            List<IGrouping<DateTime, RequestInfo>> dailyList = data.GroupBy(x => x.Fecha.Date).OrderBy(x => x.Key).ToList();
            for (int i = 0; i < dailyList.Count; i++)
            {
                IGrouping<DateTime, RequestInfo> item = dailyList[i];

                DateTime date = item.Key;

                var stats = item.ToIndicatorsStatsDto().ToList();

                foreach (var dailyItem in stats)
                {
                    dailyItem.CostoFijo = servicesCost.Where(x => x.Sucursales.Contains(dailyItem.Sucursal)).Sum(x => x.CostoFijo);
                    dailyItem.CostoReactivo = budget.Where(x => x.SucursalId == dailyItem.SucursalId).Sum(x => x.CostoReactivo);
                    dailyItem.CostoTomaCalculado = samplesCost.Where(x => x.SucursalId == dailyItem.SucursalId).Select(x => x.CostoToma).FirstOrDefault() * dailyItem.Expedientes;
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

        private void WeeklyData(List<RequestInfo> data, List<ServicesCost> servicesCost, List<Indicators> budget, List<SamplesCosts> samplesCost, IXLWorksheet weekly, DateTime yearOfWeek)
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
                    weeklyItem.CostoTomaCalculado = samplesCost.Where(x => x.SucursalId == weeklyItem.SucursalId).Select(x => x.CostoToma).FirstOrDefault() * weeklyItem.Expedientes;
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

        private async Task MonthlyData(List<RequestInfo> data, List<ServicesCost> servicesCost, List<Indicators> budget, List<SamplesCosts> samplesCost, IXLWorksheet monthly, DateTime month)
        {
            var stats = data.ToIndicatorsStatsDto().ToList();

            foreach (var monthlyItem in stats)
            {
                monthlyItem.CostoFijo = servicesCost.Where(x => x.Sucursales.Contains(monthlyItem.Sucursal)).Sum(x => x.CostoFijo);
                monthlyItem.CostoReactivo = budget.Where(x => x.SucursalId == monthlyItem.SucursalId).Sum(x => x.CostoReactivo);
                monthlyItem.CostoTomaCalculado = samplesCost.Where(x => x.SucursalId == monthlyItem.SucursalId).Select(x => x.CostoToma).FirstOrDefault() * monthlyItem.Expedientes;
            }

            var results = stats.ToTableIndicatorsStatsDto();

            var dataTable = GetTable(month.ToString("MMMM yy"), results);

            var tableWithData = monthly.Cell(3, 1).InsertTable(dataTable.AsEnumerable());

            var formRow = 3 - 1;
            int lastRow = new();

            List<string> list = results[0].Keys.Where(x => x != "NOMBRE").ToList();
            for (int monthlyData = 0; monthlyData < list.Count; monthlyData++)
            {
                var colName = monthly.Column(monthlyData + 2).ColumnLetter();
                var fr1 = formRow + 7;
                monthly.Cell(fr1, monthlyData + 2).FormulaA1 = $"={colName}{fr1 - 4}-{colName}{fr1 - 3}-{colName}{fr1 - 2}-{colName}{fr1 - 1}";

                if((monthlyData + 1) == list.Count)
                {
                    lastRow = fr1;
                }
            }

            var totalStats = data.ToTotals().ToList();

            foreach (var totalItem in totalStats)
            {
                var getBranch = await _catalogService.GetBranchByName(totalItem.Ciudad);

                totalItem.Sucursal = getBranch.Nombre;
                totalItem.SucursalId = Guid.Parse(getBranch.IdSucursal);

                totalItem.CostoFijo = servicesCost.Where(x => x.Sucursales.Contains(totalItem.Sucursal)).Sum(x => x.CostoFijo);
                totalItem.CostoReactivo = budget.Where(x => x.SucursalId == totalItem.SucursalId).Sum(x => x.CostoReactivo);
                totalItem.CostoTomaCalculado = samplesCost.Where(x => x.SucursalId == totalItem.SucursalId).Select(x => x.CostoToma).FirstOrDefault() * totalItem.Expedientes;
            }

            var totals = totalStats.ToTableTotals();

            var totalsTable = GetTable("Total Acumulado", totals);

            var tableWithTotalData = monthly.Cell(lastRow + 2, 1).InsertTable(totalsTable.AsEnumerable());

            var totalRow = lastRow + 1;

            List<string> totalList = totals[0].Keys.Where(x => x != "NOMBRE").ToList();
            for (int totalData = 0; totalData < totalList.Count; totalData++)
            {
                var colName = monthly.Column(totalData + 2).ColumnLetter();
                var fr1 = totalRow + 7;
                monthly.Cell(fr1, totalData + 2).FormulaA1 = $"={colName}{fr1 - 4}-{colName}{fr1 - 3}-{colName}{fr1 - 2}-{colName}{fr1 - 1}";
            }
        }

        private void ServiceData(List<ServicesCost> servicesCost, IXLWorksheet serviceSheet)
        {
            var stats = servicesCost.ToServiceCostDto().ToList();

            var groupData = servicesCost.GroupBy(x => new { ((DateTime)x.FechaAlta).Month, ((DateTime)x.FechaAlta).Year }).ToList();

            for (int i = 0; i < groupData.Count; i++)
            {
                var group = groupData[i];
                var date = new DateTime(group.Key.Year, group.Key.Month, 1).ToString("MMMM yy", new CultureInfo("ES"));
                var data = group.ToList();

                var results = data.ToTableServiceCostDto();
                var serviceName = data.Select(x => x.Nombre).Distinct().ToList();

                var dataTable = GetTable(date, results);

                var tableWithData = serviceSheet.Cell(3 + ((results.Count + 3) * i), 1).InsertTable(dataTable.AsEnumerable());

                var formRow = 3 + ((results.Count + 3) * i) - 1;

                List<string> list = results[0].Keys.Where(x => x != "NOMBRE").ToList();
                for (int serviceData = 0; serviceData < list.Count; serviceData++)
                {
                    var colName = serviceSheet.Column(serviceData + 2).ColumnLetter();

                    var frDaily = formRow + serviceName.Count + 5;
                    var frWeekly = formRow + serviceName.Count + 4;
                    var frMonthly = formRow + serviceName.Count + 3;

                    var serviceRange = serviceSheet.Range($"{colName}{formRow + 2}:{colName}{formRow + serviceName.Count + 1}");

                    serviceSheet.Cell(frDaily, serviceData + 2).FormulaA1 = $"=SUM({serviceRange})/ 24";
                    serviceSheet.Cell(frWeekly, serviceData + 2).FormulaA1 = $"=SUM({serviceRange}) / 6";
                    serviceSheet.Cell(frMonthly, serviceData + 2).FormulaA1 = $"=SUM({serviceRange})";
                }
            }
        }
    }
}
