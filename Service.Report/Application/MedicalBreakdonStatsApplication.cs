using Service.Report.Application.IApplication;
using Service.Report.Client.IClient;
using Service.Report.Domain.Catalogs;
using Service.Report.Dtos;
using Service.Report.Dtos.MedicalBreakdownStats;
using Service.Report.Mapper;
using Service.Report.PdfModel;
using Service.Report.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Application
{
    public class MedicalBreakdonStatsApplication : BaseApplication, IMedicalBreakdownStatsApplication
    {
        public readonly IReportRepository _repository;
        private readonly IPdfClient _pdfClient;

        public MedicalBreakdonStatsApplication(IReportRepository repository, IPdfClient pdfClient, IRepository<Branch> branchRepository, IRepository<Medic> medicRepository) : base(branchRepository, medicRepository)
        {
            _repository = repository;
            _pdfClient = pdfClient;
        }
        public async Task<IEnumerable<MedicalBreakdownRequestDto>> GetByFilter(ReportFilterDto search)
        {
            var data = await _repository.GetByFilter(search);
            var results = data.ToMedicalBreakdownRequestDto();
            return results;
        }
        public async Task<IEnumerable<MedicalBreakdownRequestChartDto>> GetChartByFilter(ReportFilterDto search)
        {
            var data = await _repository.GetByFilter(search);
            var results = data.ToMedicalBreakdownRequestChartDto();
            return results;

        }
        public async Task<MedicalBreakdownDto> GetTableByFilter(ReportFilterDto search)
        {
            var data = await _repository.GetByFilter(search);
            var results = data.ToMedicalBreakdownDto();
            return results;
        }
        public async Task<byte[]> DownloadReportPdf(ReportFilterDto search)
        {
            var requestData = await GetTableByFilter(search);
            var requestchartData = await GetChartByFilter(search);

            List<Col> columns = new()
            {
                new Col("Solicitud", ParagraphAlignment.Left),
                new Col("Nombre del Paciente", ParagraphAlignment.Left),
                new Col("Nombre del Médico", ParagraphAlignment.Left),
                new Col("Compañía", ParagraphAlignment.Left),
                new Col("Subtotal", ParagraphAlignment.Left, "C"),
                new Col("Promoción", ParagraphAlignment.Left, "C"),
                new Col("Desc.", ParagraphAlignment.Left, "C"),
                new Col("IVA", ParagraphAlignment.Left, "C"),
                new Col("Total", ParagraphAlignment.Left, "C"),
            };

            List<ChartSeries> series = new()
            {
                new ChartSeries("Clave de Médico", true),
                new ChartSeries("Solicitudes por Médico"),
            };

            var data = requestData.MedicalBreakdownRequest.Select(x => new Dictionary<string, object>
            {
                { "Solicitud", x.Solicitud },
                { "Nombre del Paciente", x.Paciente },
                { "Nombre del Médico", x.Medico },
                { "Children", x.Estudio.Select(x => new Dictionary<string, object> { { "Clave", x.Clave}, { "Estudio", x.Estudio}, { "Precio", $"Precio Estudio ${x.PrecioFinal}"},
                    { "Promoción Estudio", x.Descuento == 0 ? $"Sin Promoción" : $"Promoción Estudio ${x.Descuento}" },
                    { "Paquete", x.Paquete == null ? "Sin paquete" : $"Paquete {x.Paquete}" },
                    { "Promoción paquete", x.Promocion == 0 || x.Promocion == null ? $"Sin Promoción" : $"Promoción Paquete ${x.Promocion}" }  } )},
                { "Compañía", x.Empresa},
                { "Subtotal", x.Subtotal},
                { "Promoción", x.Promocion},
                { "Desc.", x.Descuento},
                { "IVA", x.IVA},
                { "Total", x.TotalEstudios}
            }).ToList();

            var datachart = requestchartData.Select(x => new Dictionary<string, object>
            {
                { "Clave de Médico", x.ClaveMedico},
                { "Solicitudes por Médico", x.NoSolicitudes}
            }).ToList();

            List<Col> totalColumns = new()
            {
                new Col("Desc. %", ParagraphAlignment.Center),
                new Col("Desc.", ParagraphAlignment.Center, "C"),
                new Col("IVA", ParagraphAlignment.Center, "C"),
                new Col("Total", ParagraphAlignment.Center, "C"),
            };

            var totales = new Dictionary<string, object>
            {
                { "Desc. %", $"{Math.Round(requestData.MedicalBreakdownTotal.TotalDescuentoPorcentual, 2)}%" },
                { "Desc.", requestData.MedicalBreakdownTotal.SumaDescuentos},
                { "IVA", requestData.MedicalBreakdownTotal.IVA},
                { "Total", requestData.MedicalBreakdownTotal.Total}
            };

            var branches = await GetBranchNames(search.SucursalId);
            var doctors = await GetDoctorNames(search.MedicoId);

            var headerData = new HeaderData()
            {
                NombreReporte = "Solicitudes Médicos desglosados",
                Extra = doctors.Any() ? "Médico(s): " + string.Join(", ", doctors.Select(x => x)) : "Todos los Médicos",
                Sucursal = string.Join(", ", branches.Select(x => x)),
                Fecha = $"{search.Fecha.Min():dd/MM/yyyy} - {search.Fecha.Max():dd/MM/yyyy}"
            };

            var invoice = new InvoiceData()
            {
                Subtotal = requestData.MedicalBreakdownTotal.Subtotal,
                IVA = requestData.MedicalBreakdownTotal.IVA,
                Total = requestData.MedicalBreakdownTotal.Total,
            };

            var reportData = new ReportData()
            {
                Columnas = columns,
                Series = search.Grafica ? series : null,
                Datos = data,
                DatosGrafica = datachart,
                ColumnasTotales = totalColumns,
                Header = headerData,
                Invoice = invoice,
                Totales = totales
            };

            var file = await _pdfClient.GenerateReport(reportData);

            return file;
        }



    }
}
