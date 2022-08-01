using Service.Report.Application.IApplication;
using Service.Report.Client.IClient;
using Service.Report.Domain.Branch;
using Service.Report.Domain.Medic;
using Service.Report.Dtos;
using Service.Report.Dtos.CompanyStats;
using Service.Report.Mapper;
using Service.Report.PdfModel;
using Service.Report.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Application
{
    public class CompanyStatsApplication : BaseApplication, ICompanyStatsApplication
    {
        public readonly IReportRepository _repository;
        private readonly IPdfClient _pdfClient;

        public CompanyStatsApplication(IReportRepository repository, IPdfClient pdfClient, IRepository<Branch> branchRepository, IRepository<Medic> medicRepository) : base(branchRepository, medicRepository)
        {
            _repository = repository;
            _pdfClient = pdfClient;
        }

        public async Task<IEnumerable<CompanyStatsDto>> GetByFilter(ReportFilterDto filter)
        {
            var data = await _repository.GetByFilter(filter);
            var results = data.ToCompanyStatsDto();

            return results;
        }

        public async Task<CompanyDto> GetTableByFilter(ReportFilterDto filter)
        {
            var data = await _repository.GetByFilter(filter);
            var results = data.ToCompanyDto();

            return results;
        }

        public async Task<IEnumerable<CompanyStatsChartDto>> GetChartByFilter(ReportFilterDto filter)
        {
            var data = await _repository.GetByFilter(filter);
            var results = data.ToCompanyStatsChartDto();

            return results;
        }

        public async Task<byte[]> DownloadReportPdf(ReportFilterDto filter)
        {
            var requestData = await GetTableByFilter(filter);
            var requestchartData = await GetChartByFilter(filter);

            List<Col> columns = new()
            {
                new Col("Solicitud", ParagraphAlignment.Left),
                new Col("Nombre del Paciente", ParagraphAlignment.Left),
                new Col("Nombre del Médico", ParagraphAlignment.Left),
                new Col("Tipo de compañía", ParagraphAlignment.Left),
                new Col("Estudios", ParagraphAlignment.Left, "C"),
                new Col("Desc. %", ParagraphAlignment.Left),
                new Col("Desc.", ParagraphAlignment.Left, "C"),
                new Col("Total", ParagraphAlignment.Left, "C"),
            };

            List<ChartSeries> series = new()
            {
                new ChartSeries("Compañía", true),
                new ChartSeries("Solicitudes"),
            };

            var data = requestData.CompanyStats.Select(x => new Dictionary<string, object>
            {
                { "Solicitud", x.Solicitud },
                { "Nombre del Paciente", x.Paciente },
                { "Nombre del Médico", x.Medico },
                { "Children", x.Estudio.Select(x => new Dictionary<string, object> { { "Clave", x.Clave}, { "Estudio", x.Estudio}, { "Precio", $"${x.Precio}"}  } )},
                { "Tipo de compañía", x.Convenio == 1 ? "Convenio" : "Todas"},
                { "Estudios", x.PrecioEstudios},
                { "Desc. %", $"{Math.Round(x.DescuentoPorcentual, 2)}%"},
                { "Desc.", x.Descuento},
                { "Total", x.TotalEstudios}
            }).ToList();

            var datachart = requestchartData.Select(x => new Dictionary<string, object>
            {
                { "Compañía", x.Compañia},
                { "Solicitudes", x.NoSolicitudes}
            }).ToList();

            var totales = new TotalData()
            {
                NoSolicitudes = requestData.CompanyTotal.NoSolicitudes,
                Precios = requestData.CompanyTotal.SumaEstudios,
                Descuento = requestData.CompanyTotal.SumaDescuentos,
                DescuentoPorcentual = requestData.CompanyTotal.SumaDescuentoPorcentual,
                Total = requestData.CompanyTotal.Total
            };

            var branches = await GetBranchNames(filter.SucursalId);

            var headerData = new HeaderData()
            {
                NombreReporte = "Solicitudes por Compañía",
                Sucursal = string.Join(", ", branches.Select(x => x)),
                Fecha = $"{filter.Fecha.Min():dd/MM/yyyy} - {filter.Fecha.Max():dd/MM/yyyy}"
            };

            var invoice = new InvoiceData()
            {
                Subtotal = requestData.CompanyTotal.Subtotal,
                IVA = requestData.CompanyTotal.IVA,
                Total = requestData.CompanyTotal.Total,
            };

            var reportData = new ReportData()
            {
                Columnas = columns,
                Series = filter.Grafica ? series : null,
                Datos = data,
                DatosGrafica = datachart,
                Header = headerData,
                Invoice = invoice,
                Totales = totales
            };

            var file = await _pdfClient.GenerateReport(reportData);

            return file;
        }
    }

}
