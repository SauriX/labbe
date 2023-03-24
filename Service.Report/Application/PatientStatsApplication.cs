using Service.Report.Application.IApplication;
using Service.Report.Client.IClient;
using Service.Report.Domain.Catalogs;
using Service.Report.Dtos;
using Service.Report.Dtos.PatientStats;
using Service.Report.Mapper;
using Service.Report.PdfModel;
using Service.Report.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Application
{
    public class PatientStatsApplication : BaseApplication, IPatientStatsApplication
    {
        public readonly IReportRepository _repository;
        private readonly IMedicalRecordClient _medicalRecordService;
        private readonly IPdfClient _pdfClient;

        public PatientStatsApplication(IReportRepository repository,
            IMedicalRecordClient medicalRecordService,
            IPdfClient pdfClient,
            IRepository<Branch> branchRepository) : base(branchRepository)
        {
            _medicalRecordService = medicalRecordService;
            _repository = repository;
            _pdfClient = pdfClient;

        }

        public async Task<IEnumerable<PatientStatsDto>> GetByFilter(ReportFilterDto filter)
        {
            var data = await _medicalRecordService.GetRequestByFilter(filter);

            var results = data.ToPatientStatsDto();

            return results;
        }

        public async Task<byte[]> DownloadReportPdf(ReportFilterDto filter)
        {
            var requestData = await GetByFilter(filter);

            List<Col> columns = new()
            {
                new Col("Nombre de Paciente", 3, ParagraphAlignment.Left),
                new Col("Solicitudes", ParagraphAlignment.Left),
                new Col("Total", ParagraphAlignment.Right ,"C"),
            };

            List<ChartSeries> series = new()
            {
                new ChartSeries("Iniciales Paciente", true),
                new ChartSeries("Solicitudes", "#c4c4c4"),
                new ChartSeries("Total", null, "C"),
            };

            var branches = await GetBranchNames(filter.SucursalId);

            var data = requestData.Select(x => new Dictionary<string, object>
            {
                ["Nombre de Paciente"] = x.Paciente,
                ["Solicitudes"] = x.NoSolicitudes,
                ["Total"] = x.Total,
                ["Iniciales Paciente"] = string.Join(" ", x.Paciente.Split(" ")
                                          .Where(y => !string.IsNullOrWhiteSpace(y))
                                          .Select(y => y[0]))
            }).ToList();

            var headerData = new HeaderData()
            {
                NombreReporte = "Estadística de Solicitudes por Paciente",
                Sucursal = string.Join(", ", branches.Select(x => x)),
                Fecha = $"{filter.Fecha.Min():dd/MM/yyyy} - {filter.Fecha.Max():dd/MM/yyyy}"
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
