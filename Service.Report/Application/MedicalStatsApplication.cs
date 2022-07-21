using Microsoft.Extensions.Configuration;
using Service.Report.Application.IApplication;
using Service.Report.Client.IClient;
using Service.Report.Dtos;
using Service.Report.Dtos.MedicalStats;
using Service.Report.PdfModel;
using Service.Report.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Application
{
    public class MedicalStatsApplication : IMedicalStatsApplication
    {
        public readonly IReportRepository _repository;
        private readonly IPdfClient _pdfClient;

        public MedicalStatsApplication(IReportRepository repository, IPdfClient pdfClient)
        {
            _repository = repository;
            _pdfClient = pdfClient;
        }
        public async Task<IEnumerable<MedicalStatsDto>> GetByFilter(ReportFilterDto search)
        {
            var req = await _repository.GetByFilter(search);
            var results = (from c in req
                           group c by new { c.Medico.NombreMedico, c.Medico.ClaveMedico, c.MedicoId } into grupo
                           select new MedicalStatsDto
                           {
                               ClaveMedico = grupo.Key.ClaveMedico,
                               Medico = grupo.Key.NombreMedico,
                               Total = grupo.Sum(x => x.PrecioFinal),
                               NoSolicitudes = grupo.Count(),
                               NoPacientes = grupo.Select(x => x.ExpedienteId).Distinct().Count(),
                           }).ToList();

            results.Add(new MedicalStatsDto
            {
                ClaveMedico = "Total",
                Medico = " ",
                Total = results.Sum(x => x.Total),
                NoSolicitudes = results.Sum(x => x.NoSolicitudes),
                NoPacientes = results.Sum(x => x.NoPacientes),
            });

            return results;
        }

        public async Task<byte[]> DownloadReportPdf(ReportFilterDto search)
        {
            var requestData = await GetByFilter(search);

            List<Col> columns = new()
            {
                new Col("Clave", ParagraphAlignment.Left),
                new Col("Nombre del Médico", ParagraphAlignment.Left),
                new Col("Importe", ParagraphAlignment.Right, "C"),
                new Col("Solicitudes", ParagraphAlignment.Right),
                new Col("Pacientes", ParagraphAlignment.Right),
            };

            List<ChartSeries> series = new()
            {
                new ChartSeries("Clave", true),
                new ChartSeries("Importe", null, "C"),
                new ChartSeries("Solicitudes", "#c4c4c4"),
                new ChartSeries("Pacientes", "#ea899a"),
            };

            var data = requestData.Select(x => new Dictionary<string, object>
            {
                { "Clave", x.ClaveMedico},
                { "Nombre del Médico", x.Medico},
                { "Importe", x.Total },
                { "Solicitudes", x.NoSolicitudes },
                { "Pacientes", x.NoPacientes },
            }).ToList();

            var headerData = new HeaderData()
            {
                NombreReporte = "Solicitudes por Médico Condensado",
                Sucursal = "",
                Fecha = $"{search.Fecha.Min():dd/MM/yyyy} - {search.Fecha.Max().ToString("dd/MM/yyyy")}"
            };

            var reportData = new ReportData()
            {
                Columnas = columns,
                Series = series,
                Datos = data,
                Header = headerData,
            };


            var file = await _pdfClient.GenerateReport(reportData);

            return file;

        }
    }
}