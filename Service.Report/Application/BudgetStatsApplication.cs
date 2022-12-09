﻿using Service.Report.Application.IApplication;
using Service.Report.Client.IClient;
using Service.Report.Dtos;
using Service.Report.Dtos.BudgetStats;
using Service.Report.PdfModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Application
{
    public class BudgetStatsApplication : BaseApplication, IBudgetStatsApplication
    {
        private readonly IMedicalRecordClient _medicalRecordService;
        private readonly IPdfClient _pdfClient;

        public BudgetStatsApplication(IMedicalRecordClient medicalRecordService,
            IPdfClient pdfClient)
        {
            _medicalRecordService = medicalRecordService;
            _pdfClient = pdfClient;

        }

        public async Task<List<BudgetStatsDto>> GetByFilter(ReportFilterDto filter)
        {
            var results = await _medicalRecordService.GetQuotationByFilter(filter);

            return results;
        }

        public async Task<BudgetDto> GetTableByFilter(ReportFilterDto filter)
        {
            var results = await _medicalRecordService.GetQuotationTableByFilter(filter);

            return results;
        }

        public async Task<List<BudgetStatsChartDto>> GetChartByFilter(ReportFilterDto filter)
        {
            var results = await _medicalRecordService.GetQuotationChartByFilter(filter);

            return results;
        }

        public async Task<byte[]> DownloadReportPdf(ReportFilterDto filter)
        {
            var requestData = await GetTableByFilter(filter);
            var requestchartData = await GetChartByFilter(filter);

            List<Col> columns = new()
            {
                new Col("Solicitud", ParagraphAlignment.Left),
                new Col("Paciente", ParagraphAlignment.Left),
                new Col("Médico", ParagraphAlignment.Left),
                new Col("Subtotal", ParagraphAlignment.Left, "C"),
                new Col("Promoción", ParagraphAlignment.Left, "C"),
                new Col("Desc.", ParagraphAlignment.Left, "C"),
                new Col("IVA", ParagraphAlignment.Left, "C"),
                new Col("Total", ParagraphAlignment.Left, "C"),
            };

            List<ChartSeries> series = new()
            {
                new ChartSeries("Sucursal", true),
                new ChartSeries("Presupuesto en pesos mexicanos"),
            };

            var data = requestData.BudgetStats.Select(x => new Dictionary<string, object>
            {
                { "Solicitud", x.Solicitud },
                { "Paciente", x.NombrePaciente },
                { "Médico", x.NombreMedico },
                { "Children", x.Estudio.Select(x => new Dictionary<string, object> { { "Clave", x.Clave}, { "Estudio", x.Estudio}, { "Precio", $"Precio Estudio ${x.PrecioFinal}"},
                    { "Promoción Estudio", x.Descuento == 0 ? $"Sin Promoción" : $"Promoción Estudio ${x.Descuento}" },
                    { "Paquete", x.Paquete == null ? "Sin paquete" : $"Paquete {x.Paquete}" },
                    { "Promoción paquete", x.Promocion == 0 || x.Promocion == null ? $"Sin Promoción" : $"Promoción Paquete ${x.Promocion}" }  } )},
                { "Subtotal", x.Subtotal},
                { "Promoción", x.Promocion},
                { "Desc.", x.Descuento},
                { "IVA", x.IVA},
                { "Total", x.TotalEstudios}
            }).ToList();

            var datachart = requestchartData.Select(x => new Dictionary<string, object>
            {
                { "Sucursal", x.Sucursal},
                { "Presupuesto en pesos mexicanos", x.Total}
            }).ToList();

            List<Col> totalColumns = new()
            {
                new Col("Desc. %", ParagraphAlignment.Center),
                new Col("Desc.", ParagraphAlignment.Center, "C"),
                new Col("IVA", ParagraphAlignment.Center, "C"),
                new Col("Total", ParagraphAlignment.Center, "C"),
            };

            var totales = new Dictionary<string, object>
            {
                { "Desc. %", $"{Math.Round(requestData.BudgetTotal.TotalDescuentoPorcentual, 2)}%" },
                { "Desc.", requestData.BudgetTotal.SumaDescuentos},
                { "IVA", requestData.BudgetTotal.IVA},
                { "Total", requestData.BudgetTotal.Total}
            };

            var branches = await GetBranchNames(filter.SucursalId);
            var doctors = await GetDoctorNames(filter.MedicoId);

            var headerData = new HeaderData()
            {
                NombreReporte = "Presupuesto por Sucursal",
                Sucursal = string.Join(", ", branches.Select(x => x)),
                Fecha = $"{filter.Fecha.Min():dd/MM/yyyy} - {filter.Fecha.Max():dd/MM/yyyy}"
            };

            var invoice = new InvoiceData()
            {
                Subtotal = requestData.BudgetTotal.Subtotal,
                IVA = requestData.BudgetTotal.IVA,
                Total = requestData.BudgetTotal.Total,
            };

            var reportData = new ReportData()
            {
                Columnas = columns,
                Series = filter.Grafica ? series : null,
                Datos = data,
                DatosGrafica = datachart,
                ColumnasTotales = totalColumns,
                Header = headerData,
                Invoice = invoice,
                Totales = totales
            };

            var file = await _pdfClient.GenerateReport(reportData);

            return file;
        }
    }
}
