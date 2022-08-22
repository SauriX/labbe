using Service.Report.Application.IApplication;
using Service.Report.Client.IClient;
using Service.Report.Domain.Catalogs;
using Service.Report.Dtos;
using Service.Report.Dtos.BondedRequest;
using Service.Report.Mapper;
using Service.Report.PdfModel;
using Service.Report.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Application
{
    public class MaquilaInternApplication : BaseApplication, IMaquilaInternApplication
    {
        public readonly IReportRepository _repository;
        private readonly IPdfClient _pdfClient;

        public MaquilaInternApplication(IReportRepository repository, IPdfClient pdfClient, IRepository<Branch> branchRepository, IRepository<Medic> medicRepository) : base(branchRepository, medicRepository)
        {
            _repository = repository;
            _pdfClient = pdfClient;
        }

        public async Task<IEnumerable<MaquilaRequestDto>> GetByStudies(ReportFilterDto filter)
        {
            var data = await _repository.GetByStudies(filter);
            var results = data.ToMaquilaInternDto();

            return results;
        }
        public async Task<IEnumerable<MaquilaRequestChartDto>> GetChartByFilter(ReportFilterDto filter)
        {
            var data = await _repository.GetByFilter(filter);
            var results = data.ToMaquilaInternChartDto();

            return results;
        }

        public async Task<byte[]> DownloadReportPdf(ReportFilterDto filter)
        {
            var requestData = await GetByStudies(filter);
            var requestChartData = await GetChartByFilter(filter);

            List<Col> columns = new()
            {
                new Col("Solicitud", ParagraphAlignment.Left),
                new Col("Nombre del Paciente", ParagraphAlignment.Left),
                new Col("Edad", ParagraphAlignment.Left),
                new Col("Sexo", ParagraphAlignment.Left),
                new Col("Nombre del Médico", ParagraphAlignment.Left),
                new Col("Fecha de Entrega", ParagraphAlignment.Left),
            };

            List<ChartSeries> series = new()
            {
                new ChartSeries("Maquila", true),
                new ChartSeries("Cantidad de Solicitudes"),
            };

            var data = requestData.Select(x => new Dictionary<string, object>
            {
                { "Solicitud", x.Solicitud },
                { "Nombre del Paciente", x.Paciente },
                { "Children", new List<Dictionary<string, object>> { { new Dictionary<string, object> { { "Clave", x.ClaveEstudio }, { "Estudio", x.NombreEstudio }, { "Estatus", x.Estatus }, { "Maquila", $"Maquila {x.Sucursal.ToUpper()}" } } } } },
                { "Edad", x.Edad},
                { "Sexo", x.Sexo},
                { "Nombre del Médico", x.Medico },
                { "Fecha de Entrega", x.FechaEntrega},
            }).ToList();

            var datachart = requestChartData.Select(x => new Dictionary<string, object>
            {
                { "Maquila", x.Maquila},
                { "Cantidad de Solicitudes", x.NoSolicitudes}

            }).ToList();

            var branches = await GetBranchNames(filter.SucursalId);

            var headerData = new HeaderData()
            {
                NombreReporte = "Relación Estudios con Maquila Interna por Paciente",
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
