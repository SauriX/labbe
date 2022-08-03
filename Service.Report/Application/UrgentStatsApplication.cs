using Service.Report.Application.IApplication;
using Service.Report.Client.IClient;
using Service.Report.Domain.Branch;
using Service.Report.Domain.Medic;
using Service.Report.Dtos;
using Service.Report.Dtos.StudyStats;
using Service.Report.Mapper;
using Service.Report.PdfModel;
using Service.Report.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Application
{
    public class UrgentStatsApplication : BaseApplication, IUrgentStatsApplication
    {
        public readonly IReportRepository _repository;
        private readonly IPdfClient _pdfClient;

        public UrgentStatsApplication(IReportRepository repository, IPdfClient pdfClient, IRepository<Branch> branchRepository, IRepository<Medic> medicRepository) : base(branchRepository, medicRepository)
        {
            _repository = repository;
            _pdfClient = pdfClient;
        }

        public async Task<IEnumerable<StudyStatsDto>> GetByFilter(ReportFilterDto filter)
        {
            var data = await _repository.GetByFilter(filter);
            var results = data.ToUrgentStatsDto();

            return results;
        }

        public async Task<IEnumerable<StudyStatsChartDto>> GetChartByFilter(ReportFilterDto filter)
        {
            var data = await _repository.GetByFilter(filter);
            var results = data.ToUrgentStatsChartDto();

            return results;
        }
        public async Task<byte[]> DownloadReportPdf(ReportFilterDto filter)
        {
            var requestData = await GetByFilter(filter);
            var requestChartData = await GetChartByFilter(filter);

            List<Col> columns = new()
            {
                new Col("Solicitud", ParagraphAlignment.Left),
                new Col("Nombre del Paciente", ParagraphAlignment.Left),
                new Col("Edad", ParagraphAlignment.Left),
                new Col("Sexo", ParagraphAlignment.Left),
                new Col("Nombre del Médico", ParagraphAlignment.Left),
                new Col("Fecha de Entrega", ParagraphAlignment.Left),
                new Col("Urgencia", ParagraphAlignment.Left),
            };

            List<ChartSeries> series = new()
            {
                new ChartSeries("Estatus", true),
                new ChartSeries("Cantidad"),
            };

            var data = requestData.Select(x => new Dictionary<string, object>
            {
                { "Solicitud", x.Solicitud },
                { "Nombre del Paciente", x.Paciente },
                { "Children", x.Estudio.Select(x => new Dictionary<string, object> { { "Clave", x.Clave}, { "Estudio", x.Estudio}, { "Estatus", x.Estatus}  } )},
                { "Edad", x.Edad},
                { "Sexo", x.Sexo},
                { "Nombre del Médico", x.Medico },
                { "Fecha de Entrega", x.FechaEntrega.ToString("dd/MM/yyyy")},
                { "Urgencia", x.Urgencia == 2 ? "Urgencia" : "Urgencia con cargo"},
            }).ToList();

            var datachart = requestChartData.Select(x => new Dictionary<string, object>
            {
                { "Estatus", x.Estatus},
                { "Cantidad", x.Cantidad},
                { "Color", x.Color}

            }).ToList();


            var branches = await GetBranchNames(filter.SucursalId);

            var headerData = new HeaderData()
            {
                NombreReporte = "Relación estudios por Paciente Urgente",
                Sucursal = string.Join(", ", branches.Select(x => x)),
                Fecha = $"{filter.Fecha.Min():dd/MM/yyyy} - {filter.Fecha.Max().ToString("dd/MM/yyyy")}"
            };

            var reportData = new ReportData()
            {
                Columnas = columns,
                Series = filter.Grafica ? series : null,
                Datos = data,
                DatosGrafica = datachart,
                Header = headerData,
            };

            var file = await _pdfClient.GenerateReport(reportData);

            return file;
        }
    }
}
