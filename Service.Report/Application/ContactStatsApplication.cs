using Service.Report.Application.IApplication;
using Service.Report.Client.IClient;
using Service.Report.Dtos;
using Service.Report.Dtos.ContactStats;
using Service.Report.PdfModel;
using Service.Report.Repository.IRepository;
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

        public async Task<IEnumerable<ContactStatsDto>> GetByFilter(ReportFilterDto search)
        {
            var req = await _repository.GetByFilter(search);
            var results = (from c in req
                           group c by new { c.Fecha.Year, c.Fecha.Month, c.Expediente, c.Medico.NombreMedico } into grupo
                           select new ContactStatsDto
                           {
                               Expediente = grupo.Key.Expediente.Expediente,
                               Paciente = grupo.Key.Expediente.Nombre,
                               Medico = grupo.Key.NombreMedico,
                               Celular = grupo.Key.Expediente.Celular,
                               Correo = grupo.Key.Expediente.Correo
                           }).ToList();

            return results;
        }

        public async Task<IEnumerable<ContactStatsChartDto>> GetCharByFilter(ReportFilterDto search)
        {
            var req = await _repository.GetByFilter(search);
            var results = (from c in req
                           group c by c.Expediente into grupo
                           select new ContactStatsChartDto
                           {
                               CantidadTelefono = grupo.Count(x => !string.IsNullOrWhiteSpace(x.Expediente.Celular)),
                               CantidadCorreo = grupo.Count(x => !string.IsNullOrWhiteSpace(x.Expediente.Correo))
                           }).ToList();

            results.Add(new ContactStatsChartDto
            {
                CantidadTelefono = results.Sum(x => x.CantidadTelefono),
                CantidadCorreo = results.Sum(x => x.CantidadCorreo),
            });

            return results;
        }

        public async Task<byte[]> DownloadReportPdf(ReportFilterDto search)
        {
            var requestData = await GetByFilter(search);
            var requestchartData = await GetCharByFilter(search);

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
                { "Nombre del Paciente", x.Paciente},
                { "Nombre del Médico", x.Medico },
                { "Contacto", new List<string>{ x.Celular, x.Correo } },
            }).ToList();

            var datachart = requestchartData.Select(x => new Dictionary<string, object>
            {
                { "WhatsApp", x.CantidadTelefono },
                { "Correo", x.CantidadCorreo},
                { "Total", x.Total },
            }).ToList();

            var headerData = new HeaderData()
            {
                NombreReporte = "Solicitudes por Contacto",
                Sucursal = "",
                Fecha = $"{search.Fecha.Min():dd/MM/yyyy} - {search.Fecha.Max().ToString("dd/MM/yyyy")}"
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
