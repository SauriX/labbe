using Service.Report.Application.IApplication;
using Service.Report.Client.IClient;
using Service.Report.Domain.Catalogs;
using Service.Report.Dtos;
using Service.Report.Dtos.CanceledRequest;
using Service.Report.Mapper;
using Service.Report.PdfModel;
using Service.Report.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Application
{
    public class CanceledRequestApplication : BaseApplication, ICanceledRequestApplication
    {
        public readonly IReportRepository _repository;
        private readonly IPdfClient _pdfClient;

        public CanceledRequestApplication(IReportRepository repository, IPdfClient pdfClient, IRepository<Branch> branchRepository, IRepository<Medic> medicRepository) : base(branchRepository, medicRepository)
        {
            _repository = repository;
            _pdfClient = pdfClient;
        }

        public async Task<IEnumerable<CanceledRequestDto>> GetByFilter(ReportFilterDto filter)
        {
            var data = await _repository.GetByFilter(filter);
            var results = data.ToCanceledRequestDto();

            return results;
        }

        public async Task<IEnumerable<CanceledRequestChartDto>> GetChartByFilter(ReportFilterDto filter)
        {
            var data = await _repository.GetByFilter(filter);
            var results = data.ToCanceledRequestChartDto();

            return results;
        }

        public async Task<CanceledDto> GetTableByFilter(ReportFilterDto filter)
        {
            var data = await _repository.GetByFilter(filter);
            var results = data.ToCanceledDto();

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
                new Col("Compañía", ParagraphAlignment.Left),
                new Col("Subtotal", ParagraphAlignment.Left, "C"),
                new Col("Desc. %", ParagraphAlignment.Left),
                new Col("Desc.", ParagraphAlignment.Left, "C"),
                new Col("IVA", ParagraphAlignment.Left, "C"),
                new Col("Total", ParagraphAlignment.Left, "C"),
            };

            List<ChartSeries> series = new()
            {
                new ChartSeries("Solicitud", true),
                new ChartSeries("Cancelada"),
            };

            var data = requestData.CanceledRequest.Select(x => new Dictionary<string, object>
            {
                { "Solicitud", x.Solicitud },
                { "Nombre del Paciente", x.Paciente },
                { "Nombre del Médico", x.Medico },
                { "Children", x.Estudio.Select(x => new Dictionary<string, object> { { "Clave", x.Clave}, { "Estudio", x.Estudio}, { "Precio", $"${x.Precio}"}  } )},
                { "Compañía", x.Empresa},
                { "Subtotal", x.Subtotal},
                { "Desc. %", $"{Math.Round(x.DescuentoPorcentual, 2)}%"},
                { "Desc.", x.Descuento},
                { "IVA", x.IVA},
                { "Total", x.TotalEstudios}
            }).ToList();

            var datachart = requestchartData.Select(x => new Dictionary<string, object>
            {
                { "Solicitud", x.Solicitud},
                { "Cancelada", x.Cantidad}
            }).ToList();

            var totales = new TotalData()
            {
                NoSolicitudes = requestData.CanceledTotal.NoSolicitudes,
                Precios = requestData.CanceledTotal.SumaEstudios,
                Descuento = requestData.CanceledTotal.SumaDescuentos,
                DescuentoPorcentual = requestData.CanceledTotal.TotalDescuentoPorcentual,
                Total = requestData.CanceledTotal.Total
            };

            var branches = await GetBranchNames(filter.SucursalId);

            var headerData = new HeaderData()
            {
                NombreReporte = "Solicitudes Canceladas",
                Sucursal = string.Join(", ", branches.Select(x => x)),
                Fecha = $"{filter.Fecha.Min():dd/MM/yyyy} - {filter.Fecha.Max():dd/MM/yyyy}"
            };

            var invoice = new InvoiceData()
            {
                Subtotal = requestData.CanceledTotal.Subtotal,
                IVA = requestData.CanceledTotal.IVA,
                Total = requestData.CanceledTotal.Total,
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
