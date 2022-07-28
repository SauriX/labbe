using Microsoft.Extensions.Configuration;
using Service.Report.Application.IApplication;
using Service.Report.Client.IClient;
using Service.Report.Domain.Branch;
using Service.Report.Domain.Medic;
using Service.Report.Dtos;
using Service.Report.Dtos.MedicalStats;
using Service.Report.Mapper;
using Service.Report.PdfModel;
using Service.Report.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Application
{
    public class MedicalStatsApplication : BaseApplication, IMedicalStatsApplication
    {
        public readonly IReportRepository _repository;
        private readonly IPdfClient _pdfClient;

        public MedicalStatsApplication(IReportRepository repository, IPdfClient pdfClient, IRepository<Branch> branchRepository, IRepository<Medic> medicRepository) : base(branchRepository, medicRepository)
        {
            _repository = repository;
            _pdfClient = pdfClient;
        }
        public async Task<IEnumerable<MedicalStatsDto>> GetByFilter(ReportFilterDto filter)
        {
            var data = await _repository.GetByFilter(filter);
            var results = data.ToMedicalStatsDto();

            return results;
        }

        public async Task<byte[]> DownloadReportPdf(ReportFilterDto filter)
        {
            var requestData = await GetByFilter(filter);

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

            var branches = await GetBranchNames(filter.SucursalId);

            var headerData = new HeaderData()
            {
                NombreReporte = "Solicitudes por Médico Condensado",
                Sucursal = string.Join(", ", branches.Select(x => x)),
                Fecha = $"{filter.Fecha.Min():dd/MM/yyyy} - {filter.Fecha.Max().ToString("dd/MM/yyyy")}"
            };

            var reportData = new ReportData()
            {
                Columnas = columns,
                Series = filter.Grafica ? series : null,
                Datos = data,
                Header = headerData,
            };


            var file = await _pdfClient.GenerateReport(reportData);

            return file;

        }
    }
}