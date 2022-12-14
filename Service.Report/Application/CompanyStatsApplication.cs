﻿using Service.Report.Application.IApplication;
using Service.Report.Client.IClient;
using Service.Report.Domain.Catalogs;
using Service.Report.Dtos;
using Service.Report.Dtos.CompanyStats;
using Service.Report.Mapper;
using Service.Report.PdfModel;
using Service.Report.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Application
{
    public class CompanyStatsApplication : BaseApplication, ICompanyStatsApplication
    {
        public readonly IReportRepository _repository;
        private readonly IMedicalRecordClient _medicalRecordService;
        private readonly IPdfClient _pdfClient;

        public CompanyStatsApplication(IReportRepository repository,
            IMedicalRecordClient medicalRecordService,
            IPdfClient pdfClient,
            IRepository<Branch> branchRepository,
            IRepository<Medic> medicRepository,
            IRepository<Company> companyRepository) : base(branchRepository, medicRepository, companyRepository)
        {
            _medicalRecordService = medicalRecordService;
            _repository = repository;
            _pdfClient = pdfClient;

        }

        public async Task<IEnumerable<CompanyStatsDto>> GetByFilter(ReportFilterDto filter)
        {
            var data = await _medicalRecordService.GetRequestByFilter(filter);
            var results = data.ToCompanyStatsDto();

            return results;
        }

        public async Task<CompanyDto> GetTableByFilter(ReportFilterDto filter)
        {
            var data = await _medicalRecordService.GetRequestByFilter(filter);
            var results = data.ToCompanyDto();

            return results;
        }

        public async Task<IEnumerable<CompanyStatsChartDto>> GetChartByFilter(ReportFilterDto filter)
        {
            var data = await _medicalRecordService.GetRequestByFilter(filter);
            var results = data.ToCompanyStatsChartDto();

            return results;
        }

        public async Task<byte[]> DownloadReportPdf(ReportFilterDto filter)
        {
            var requestData = await GetTableByFilter(filter);
            var requestchartData = await GetChartByFilter(filter);

            List<Col> columns = new()
            {
                new Col("Solicitud", ParagraphAlignment.Left),
                new Col("Nombre del Paciente", ParagraphAlignment.Left),
                new Col("Nombre del Médico", ParagraphAlignment.Left),
                new Col("Tipo de compañía", ParagraphAlignment.Left),
                new Col("Estudios", ParagraphAlignment.Left, "C"),
                new Col("Promoción", ParagraphAlignment.Left, "C"),
                new Col("Desc.", ParagraphAlignment.Left, "C"),
                new Col("Total", ParagraphAlignment.Left, "C"),
            };

            List<ChartSeries> series = new()
            {
                new ChartSeries("Compañía", true),
                new ChartSeries("Solicitudes"),
            };

            var data = requestData.CompanyStats.Select(x => new Dictionary<string, object>
            {
                { "Solicitud", x.Solicitud },
                { "Nombre del Paciente", x.Paciente },
                { "Nombre del Médico", x.Medico },
                { "Children", x.Estudio.Select(x => new Dictionary<string, object> { { "Clave", x.Clave}, { "Estudio", x.Estudio}, { "Precio", $"${x.PrecioFinal}"},
                    { "Desc.", x.Descuento == 0 ? $" - " : $"${x.Descuento}" },
                    { "Paquete", x.Paquete == null ? " - " : $"Paquete\n {x.Paquete}" },
                    { "Promoción", x.Promocion == 0 || x.Promocion == null ? $" - " : $"${x.Promocion}" }  } )},
                { "Tipo de compañía", x.Convenio == 1 ? "Convenio" : "Todas"},
                { "Estudios", x.PrecioEstudios},
                { "Promoción", x.Promocion},
                { "Desc.", x.Descuento},
                { "Total", x.TotalEstudios}
            }).ToList();

            var datachart = requestchartData.Select(x => new Dictionary<string, object>
            {
                { "Compañía", x.Compañia},
                { "Solicitudes", x.NoSolicitudes}
            }).ToList();

            List<Col> totalColumns = new()
            {
                new Col("Solicitudes"),
                new Col("Estudios", ParagraphAlignment.Center, "C"),
                new Col("Desc. %", ParagraphAlignment.Center),
                new Col("Desc.", ParagraphAlignment.Center, "C"),
                new Col("Total", ParagraphAlignment.Center, "C"),
            };

            var totales = new Dictionary<string, object>
            {
                { "Solicitudes", requestData.CompanyTotal.NoSolicitudes},
                { "Estudios", requestData.CompanyTotal.SumaEstudios},
                { "Desc. %", $"{Math.Round(requestData.CompanyTotal.TotalDescuentoPorcentual, 2)}%" },
                { "Desc.", requestData.CompanyTotal.SumaDescuentos},
                { "Total", requestData.CompanyTotal.Total}
            };

            var branches = await GetBranchNames(filter.SucursalId);

            var headerData = new HeaderData()
            {
                NombreReporte = "Solicitudes por Compañía",
                Sucursal = string.Join(", ", branches.Select(x => x)),
                Fecha = $"{filter.Fecha.Min():dd/MM/yyyy} - {filter.Fecha.Max():dd/MM/yyyy}"
            };

            var invoice = new InvoiceData()
            {
                Subtotal = requestData.CompanyTotal.Subtotal,
                IVA = requestData.CompanyTotal.IVA,
                Total = requestData.CompanyTotal.Total,
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