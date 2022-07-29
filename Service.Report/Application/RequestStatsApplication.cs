using Service.Report.Application.IApplication;
using Service.Report.Client.IClient;
using Service.Report.Domain.Branch;
using Service.Report.Dtos;
using Service.Report.Dtos.Request;
using Service.Report.Mapper;
using Service.Report.PdfModel;
using Service.Report.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Application
{
    public class RequestStatsApplication : BaseApplication, IRequestStatsApplication
    {
        private readonly IReportRepository _repository;
        private readonly IPdfClient _pdfClient;

        public RequestStatsApplication(IReportRepository repository, IPdfClient pdfClient, IRepository<Branch> branchRepository) : base(branchRepository)
        {
            _repository = repository;
            _pdfClient = pdfClient;
        }

        public async Task<IEnumerable<RequestStatsDto>> GetByFilter(ReportFilterDto filter)
        {
            var data = await _repository.GetByFilter(filter);

            var results = data.ToRequestStatsDto();

            return results;
        }

        public async Task<byte[]> DownloadReportPdf(ReportFilterDto filter)
        {
            var requestsData = await GetByFilter(filter);

            List<Col> columns = new()
            {
                new Col("Clave", ParagraphAlignment.Left),
                new Col("Paciente", ParagraphAlignment.Left),
                new Col("Visitas"),
            };

            List<ChartSeries> series = new()
            {
                new ChartSeries("Clave", true),
                new ChartSeries("Visitas"),
            };

            var data = requestsData.Select(x => new Dictionary<string, object>
            {
                ["Clave"] = x.Expediente,
                ["Visitas"] = x.NoSolicitudes,
                ["Paciente"] = x.Paciente
            }).ToList();

            var branches = await GetBranchNames(filter.SucursalId);

            var headerData = new HeaderData()
            {
                NombreReporte = "Estadística de Solicitudes por Paciente",
                Sucursal = string.Join(", ", branches.Select(x => x)),
                Fecha = $"{filter.Fecha.Min():dd/MM/yyyy} - {filter.Fecha.Max():dd/MM/yyyy}"
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
