﻿using Service.Report.Application.IApplication;
using Service.Report.Client.IClient;
using Service.Report.Domain.Branch;
using Service.Report.Domain.Medic;
using Service.Report.Dtos;
using Service.Report.Dtos.ContactStats;
using Service.Report.Mapper;
using Service.Report.PdfModel;
using Service.Report.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Application
{
    public class ContactStatsApplication : BaseApplication, IContactStatsApplication
    {
        public readonly IReportRepository _repository;
        private readonly IPdfClient _pdfClient;

        public ContactStatsApplication(IReportRepository repository, IPdfClient pdfClient, IRepository<Branch> branchRepository, IRepository<Medic> medicRepository) : base(branchRepository, medicRepository)
        {
            _repository = repository;
            _pdfClient = pdfClient;
        }

        public async Task<IEnumerable<ContactStatsDto>> GetByFilter(ReportFilterDto filter)
        {
            var data = await _repository.GetByFilter(filter);
            var results = data.ToContactStatsDto();

            return results;
        }

        public async Task<IEnumerable<ContactStatsChartDto>> GetCharByFilter(ReportFilterDto filter)
        {
            var data = await _repository.GetByFilter(filter);
            var results = data.ToContactStatsChartDto();

            return results;
        }

        public async Task<byte[]> DownloadReportPdf(ReportFilterDto filter)
        {
            var requestData = await GetByFilter(filter);
            var requestchartData = await GetCharByFilter(filter);

            List<Col> columns = new()
            {
                new Col("Expediente", ParagraphAlignment.Left),
                new Col("Nombre del Paciente", ParagraphAlignment.Left),
                new Col("Nombre del Médico", ParagraphAlignment.Left),
                new Col("Estatus", ParagraphAlignment.Left),
                new Col("Contacto", ParagraphAlignment.Right),
            };

            List<ChartSeries> series = new()
            {
                new ChartSeries("Fecha", true),
                new ChartSeries("Solicitudes", "#C4DAE8"),
                new ChartSeries("WhatsApp", "#86B6D5"),
                new ChartSeries("Correo", "#9ECAE1"),
                new ChartSeries("Total Medio de Contacto", "#2D83BE"),
            };

            var data = requestData.Select(x => new Dictionary<string, object>
            {
                { "Expediente", x.Expediente },
                { "Nombre del Paciente", x.Paciente},
                { "Nombre del Médico", x.Medico },
                { "Estatus", x.Estatus},
                { "Contacto", new List<string>{ x.Celular, x.Correo } },
            }).ToList();

            var datachart = requestchartData.Select(x => new Dictionary<string, object>
            {
                { "Fecha", x.Fecha},
                { "Solicitudes", x.Solicitudes},
                { "WhatsApp", x.CantidadTelefono },
                { "Correo", x.CantidadCorreo},
                { "Total Medio de Contacto", x.Total },
            }).ToList();

            var branches = await GetBranchNames(filter.SucursalId);

            var headerData = new HeaderData()
            {
                NombreReporte = "Solicitudes por Contacto",
                Sucursal = string.Join(", ", branches.Select(x => x)),
                Fecha = $"{filter.Fecha.Min():dd/MM/yyyy} - {filter.Fecha.Max().ToString("dd/MM/yyyy")}"
            };

            var reportData = new ReportData()
            {
                Columnas = columns,
                Series = series,
                Datos = data,
                DatosGrafica = datachart,
                Header = headerData,
            };

            var file = await _pdfClient.GenerateReport(reportData);

            return file;
        }
    }
}
