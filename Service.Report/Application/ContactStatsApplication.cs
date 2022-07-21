using Service.Report.Application.IApplication;
using Service.Report.Client.IClient;
using Service.Report.Dtos;
using Service.Report.Dtos.ContactStats;
using Service.Report.PdfModel;
using Service.Report.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Application
{
    public class ContactStatsApplication : IContactStatsApplication
    {
        public readonly IReportRepository _repository;
        private readonly IPdfClient _pdfClient;

        public ContactStatsApplication(IReportRepository repository, IPdfClient pdfClient)
        {
            _repository = repository;
            _pdfClient = pdfClient;
        }

        public async Task<IEnumerable<ContactStatsDto>> GetByContact()
        {
            var req = await _repository.GetAll();
            var results = (from c in req
                           group c by new { c.Expediente, c.Medico.NombreMedico } into grupo
                           select new ContactStatsDto 
                           {
                                Expediente = grupo.Key.Expediente.Expediente,
                                NombrePaciente = grupo.Key.Expediente.Nombre,
                                NombreMedico = grupo.Key.NombreMedico,
                                Celular = grupo.Key.Expediente.Celular,
                                Correo = grupo.Key.Expediente.Correo
                           }).ToList();

            return results;
        }

        public async Task<IEnumerable<ContactStatsChartDto>> GetForChart()
        {
            var req = await _repository.GetAll();
            var results = (from c in req
                           group c by c.Expediente into grupo
                           select new ContactStatsChartDto
                           {
                               Cant_Celular = grupo.Count(x => !string.IsNullOrWhiteSpace(x.Expediente.Celular)),
                               Cant_Correo = grupo.Count(x => !string.IsNullOrWhiteSpace(x.Expediente.Correo))
                           }).ToList();

            results.Add(new ContactStatsChartDto
            {
                Cant_Celular = results.Sum(x => x.Cant_Celular),
                Cant_Correo = results.Sum(x => x.Cant_Correo),
            });

            return results;
        }

        public async Task<IEnumerable<ContactStatsDto>> GetFilter(ReportFiltroDto search)
        {
            var req = await _repository.GetFilter(search);
            var results = (from c in req
                           group c by new { c.Fecha.Year, c.Fecha.Month, c.Expediente, c.Medico.NombreMedico } into grupo
                           select new ContactStatsDto
                           {
                               Expediente = grupo.Key.Expediente.Expediente,
                               NombrePaciente = grupo.Key.Expediente.Nombre,
                               NombreMedico = grupo.Key.NombreMedico,
                               Celular = grupo.Key.Expediente.Celular,
                               Correo = grupo.Key.Expediente.Correo
                           }).ToList();

            return results;
        }

        public async Task<IEnumerable<ContactStatsChartDto>> GetFilterChart(ReportFiltroDto search)
        {
            var req = await _repository.GetFilter(search);
            var results = (from c in req
                           group c by c.Expediente into grupo
                           select new ContactStatsChartDto
                           {
                               Cant_Celular = grupo.Count(x => !string.IsNullOrWhiteSpace(x.Expediente.Celular)),
                               Cant_Correo = grupo.Count(x => !string.IsNullOrWhiteSpace(x.Expediente.Correo))
                           }).ToList();

            results.Add(new ContactStatsChartDto
            {
                Cant_Celular = results.Sum(x => x.Cant_Celular),
                Cant_Correo = results.Sum(x => x.Cant_Correo),
            });

            return results;
        }

        public async Task<byte[]> GenerateReportPDF(ReportFiltroDto search)
        {
            var requestData = await GetFilter(search);
            var requestchartData = await GetFilterChart(search);

            List<Col> columns = new()
            {
                new Col("Expediente", ParagraphAlignment.Left),
                new Col("Nombre del Paciente", ParagraphAlignment.Left),
                new Col("Nombre del Médico", ParagraphAlignment.Right, "C"),
                new Col("Contacto", ParagraphAlignment.Right),
            };

            List<ChartSeries> series = new()
            {
                new ChartSeries("Fecha", true),
                new ChartSeries("WhatsApp", null),
                new ChartSeries("Correo", "#c4c4c4"),
                new ChartSeries("Total", "#ea899a"),
            };

            var data = requestData.Select(x => new Dictionary<string, object>
            {
                { "Expendiente", x.Expediente },
                { "Nombre del Paciente", x.NombrePaciente},
                { "Nombre del Médico", x.NombreMedico },
                { "Contacto", new List<string>{ x.Celular, x.Correo } },
            }).ToList();

            var datachart = requestchartData.Select(x => new Dictionary<string, object>
            {
                { "WhatsApp", x.Cant_Celular },
                { "Correo", x.Cant_Correo},
                { "Total", x.Total },
            }).ToList();

            var headerData = new HeaderData()
            {
                NombreReporte = "Solicitudes por Contacto",
                Sucursal = search.Sucursal,
                Fecha = $"{ search.Fecha.Min():dd/MM/yyyy} - {search.Fecha.Max().ToString("dd/MM/yyyy")}"
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
