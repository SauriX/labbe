﻿using Service.Report.Application.IApplication;
using Service.Report.Client.IClient;
using Service.Report.Domain.Catalogs;
using Service.Report.Dtos;
using Service.Report.Dtos.CanceledRequest;
using Service.Report.Mapper;
using Service.Report.PdfModel;
using Service.Report.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Application
{
    public class CanceledRequestApplication : BaseApplication, ICanceledRequestApplication
    {
        public readonly IReportRepository _repository;
        private readonly IMedicalRecordClient _medicalRecordService;
        private readonly IPdfClient _pdfClient;

        public CanceledRequestApplication(IReportRepository repository,
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

        public async Task<IEnumerable<CanceledRequestDto>> GetByFilter(ReportFilterDto filter)
        {
            var data = await _medicalRecordService.GetRequestByFilter(filter);
            var results = data.ToCanceledRequestDto();

            return results;
        }

        public async Task<IEnumerable<CanceledRequestChartDto>> GetChartByFilter(ReportFilterDto filter)
        {
            var data = await _medicalRecordService.GetRequestByFilter(filter);
            var results = data.ToCanceledRequestChartDto();

            return results;
        }

        public async Task<CanceledDto> GetTableByFilter(ReportFilterDto filter)
        {
            var data = await _medicalRecordService.GetRequestByFilter(filter);
            var results = data.ToCanceledDto();

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
                new Col("Compañía", ParagraphAlignment.Left),
                new Col("Subtotal", ParagraphAlignment.Left, "C"),
                new Col("Promoción", ParagraphAlignment.Left, "C"),
                new Col("Desc.", ParagraphAlignment.Left, "C"),
                new Col("IVA", ParagraphAlignment.Left, "C"),
                new Col("Total", ParagraphAlignment.Left, "C"),
            };

            List<ChartSeries> series = new()
            {
                new ChartSeries("Sucursal", true),
                new ChartSeries("Cancelada"),
            };

            var data = requestData.CanceledRequest.Select(x => new Dictionary<string, object>
            {
                { "Solicitud", x.Solicitud },
                { "Paciente", x.Paciente },
                { "Médico", x.Medico },
                { "Children", x.Estudio.Select(x => new Dictionary<string, object> { { "Clave", x.Clave}, { "Estudio", x.Estudio}, { "Precio", $"Precio Estudio ${x.PrecioFinal}"},
                    { "Promoción Estudio", x.Descuento == 0 ? $"Sin Promoción" : $"Promoción Estudio ${x.Descuento}" },
                    { "Paquete", x.Paquete == null ? "Sin paquete" : $"Paquete {x.Paquete}" },
                    { "Promoción paquete", x.Promocion == 0 || x.Promocion == null ? $"Sin Promoción" : $"Promoción Paquete ${x.Promocion}" }  } )},
                { "Compañía", x.Empresa},
                { "Subtotal", x.Subtotal},
                { "Promoción", x.Promocion},
                { "Desc.", x.Descuento},
                { "IVA", x.IVA},
                { "Total", x.TotalEstudios}
            }).ToList();

            var datachart = requestchartData.Select(x => new Dictionary<string, object>
            {
                { "Sucursal", x.Sucursal},
                { "Cancelada", x.Cantidad}
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
                { "Desc. %", $"{Math.Round(requestData.CanceledTotal.TotalDescuentoPorcentual, 2)}%" },
                { "Desc.", requestData.CanceledTotal.SumaDescuentos},
                { "IVA", requestData.CanceledTotal.IVA},
                { "Total", requestData.CanceledTotal.Total}
            };

            var branches = await GetBranchNames(filter.SucursalId);
            var companies = await GetCompanyNames(filter.CompañiaId);

            var headerData = new HeaderData()
            {
                NombreReporte = "Solicitudes Canceladas",
                Extra = companies.Any() ? "Compañía(s): " + string.Join(", ", companies.Select(x => x)) : "Todas las Compañías",
                Sucursal = string.Join(", ", branches.Select(x => x)),
                Fecha = $"{filter.Fecha.Min():dd/MM/yyyy} - {filter.Fecha.Max():dd/MM/yyyy}"
            };

            var invoice = new InvoiceData()
            {
                Subtotal = requestData.CanceledTotal.Subtotal,
                IVA = requestData.CanceledTotal.IVA,
                Total = requestData.CanceledTotal.Total,
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
