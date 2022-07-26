using Service.Report.Application.IApplication;
using Service.Report.Client.IClient;
using Service.Report.Domain.Branch;
using Service.Report.Domain.Company;
using Service.Report.Domain.Medic;
using Service.Report.Dtos;
using Service.Report.Mapper;
using Service.Report.Dtos.StudyStats;
using Service.Report.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.Report.PdfModel;

namespace Service.Report.Application
{
    public class StudyStatsApplicatios : BaseApplication, IStudyStatsApplication
    {
        public readonly IReportRepository _repository;
        private readonly IPdfClient _pdfClient;

        public StudyStatsApplicatios(IReportRepository repository, IPdfClient pdfClient, IRepository<Branch> branchRepository, IRepository<Medic> medicRepository, IRepository<Company> companyRepository) : base(branchRepository, medicRepository, companyRepository)
        {
            _repository = repository;
            _pdfClient = pdfClient;
        }

        public async Task<IEnumerable<StudyStatsDto>> GetByFilter(ReportFilterDto filter)
        {
            var data = await _repository.GetByFilter(filter);
            var results = data.ToStudyStatsDto();

            return results;
        }

        public async Task<IEnumerable<StudyStatsChartDto>> GetCharByFilter(ReportFilterDto filter)
        {
            var data = await _repository.GetByFilter(filter);
            var results = data.ToStudyStatsChartDto();

            return results;
        }

        public async Task<byte[]> DownloadReportPdf(ReportFilterDto filter)
        {
            var requestData = await GetByFilter(filter);
            var requestChartData = await GetCharByFilter(filter);

            List<Col> columns = new()
            {
                new Col("Solicitud", ParagraphAlignment.Left),
                new Col("Nombre del Paciente", ParagraphAlignment.Left),
                new Col("Edad", ParagraphAlignment.Left),
                new Col("Sexo", ParagraphAlignment.Left),
                new Col("Nombre del Médico", ParagraphAlignment.Left),
                new Col("Fecha de Entrega", ParagraphAlignment.Left),
                new Col("Fecha de Estudio", ParagraphAlignment.Left),
                new Col("Parcialidad", ParagraphAlignment.Left),
            };

            List<ChartSeries> series = new()
            {
                new ChartSeries("Estudios", true),
                new ChartSeries("Pendiente", "#C4DAE8"),
                new ChartSeries("Toma", "#C4DAE8"),
                new ChartSeries("Solicitado", "#86B6D5"),
                new ChartSeries("Capturado", "#9ECAE1"),
                new ChartSeries("Validado", "#2D83BE"),
                new ChartSeries("En Ruta", "#C4DAE8"),
                new ChartSeries("Liberado", "#C4DAE8"),
                new ChartSeries("Enviado", "#86B6D5"),
                new ChartSeries("Entregado", "#9ECAE1"),
                new ChartSeries("Cancelado", "#2D83BE"),
            };

            var data = requestData.Select(x => new Dictionary<string, object>
            {
                { "Solicitud", x.Solicitud },
                { "Nombre del Paciente", x.Paciente},
                { "Edad", x.Edad},
                { "Sexo", x.Sexo},
                { "Nombre del Médico", x.Medico },
                { "Fecha de Entrega", x.FechaEntrega},
                { "Fecha de Estudio", x.Fecha},
                { "Parcialidad", x.Parcialidad},
            }).ToList();

            var datachart = requestChartData.Select(x => new Dictionary<string, object>
            {
                { "Estudios", "Estatus de Estudios"},
                { "Pendiente", x.CantidadPendiente},
                { "Toma", x.CantidadTomaDeMuestra},
                { "Solicitado", x.CantidadSolicitado},
                { "Capturado", x.CantidadCapturado},
                { "Validado", x.CantidadValidado},
                { "En Ruta", x.CantidadEnRuta},
                { "Liberado", x.CantidadLiberado},
                { "Enviado", x.CantidadEnviado},
                { "Entregado", x.CantidadEntregado},
                { "Cancelado", x.CantidadCancelado},

            }).ToList();

            var branches = await GetBranchNames(filter.SucursalId);

            var headerData = new HeaderData()
            {
                NombreReporte = "Relación estudios por Paciente",
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
