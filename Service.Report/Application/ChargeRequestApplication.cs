using Service.Report.Application.IApplication;
using Service.Report.Client.IClient;
using Service.Report.Domain.Catalogs;
using Service.Report.Dtos;
using Service.Report.Dtos.TypeRequest;
using Service.Report.Mapper;
using Service.Report.PdfModel;
using Service.Report.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Application
{
    public class ChargeRequestApplication : BaseApplication, IChargeRequestApplication
    {
        public readonly IReportRepository _repository;
        private readonly IMedicalRecordClient _medicalRecordService;
        private readonly IPdfClient _pdfClient;

        public ChargeRequestApplication(IReportRepository repository,
            IMedicalRecordClient medicalRecordService,
            IPdfClient pdfClient,
            IRepository<Branch> branchRepository,
            IRepository<Medic> medicRepository,
            IRepository<Company> companyRepository) : base(branchRepository, medicRepository, companyRepository)
        {
            _medicalRecordService = medicalRecordService;
            _repository = repository;
            _pdfClient = pdfClient;

        }

        public async Task<IEnumerable<TypeRequestDto>> GetByFilter(ReportFilterDto filter)
        {
            var data = await _medicalRecordService.GetRequestByFilter(filter);
            var results = data.ToChargeRequestDto();

            return results;
        }

        public async Task<TypeDto> GetTableByFilter(ReportFilterDto filter)
        {
            var data = await _medicalRecordService.GetRequestByFilter(filter);
            var results = data.ToChargeDto();

            return results;
        }

        public async Task<IEnumerable<TypeRequestChartDto>> GetChartByFilter(ReportFilterDto filter)
        {
            var data = await _medicalRecordService.GetRequestByFilter(filter);
            var results = data.ToChargeRequestChartDto();

            return results;
        }


        public async Task<byte[]> DownloadReportPdf(ReportFilterDto filter)
        {
            var requestData = await GetTableByFilter(filter);
            var requestchartData = await GetChartByFilter(filter);

            List<Col> columns = new()
            {
                new Col("Solicitud", ParagraphAlignment.Left),
                new Col("Paciente", ParagraphAlignment.Left),
                new Col("Médico", ParagraphAlignment.Left),
                new Col("Compañía", ParagraphAlignment.Left),
                new Col("Subtotal", ParagraphAlignment.Left, "C"),
                new Col("Promoción", ParagraphAlignment.Left, "C"),
                new Col("Cargo", ParagraphAlignment.Left, "C"),
                new Col("IVA", ParagraphAlignment.Left, "C"),
                new Col("Total", ParagraphAlignment.Left, "C"),
            };

            List<ChartSeries> series = new()
            {
                new ChartSeries("Sucursal", true),
                new ChartSeries("Cantidad con Cargo"),
            };

            var data = requestData.TypeChargeRequest.Select(x => new Dictionary<string, object>
            {
                { "Solicitud", x.Solicitud },
                { "Paciente", x.Paciente },
                { "Médico", x.Medico },
                { "Children", x.Estudio.Select(x => new Dictionary<string, object> { { "Clave", x.Clave}, { "Estudio", x.Estudio}, { "Precio", $"${x.PrecioFinal}"},
                    { "Desc.", x.Descuento == 0 ? $" - " : $"${x.Descuento}" },
                    { "Paquete", x.Paquete == null ? " - " : $"Paquete\n {x.Paquete}" },
                    { "Promoción", x.Promocion == 0 || x.Promocion == null ? $" - " : $"${x.Promocion}" }  } )},
                { "Compañía", x.Empresa},
                { "Subtotal", x.SubtotalCargo},
                { "Promoción",x.Promocion},
                { "Cargo", x.Cargo},
                { "IVA", x.IVACargo},
                { "Total", x.TotalCargo}
            }).ToList();

            var datachart = requestchartData.Select(x => new Dictionary<string, object>
            {
                { "Sucursal", x.Sucursal},
                { "Cantidad con Cargo", x.Cantidad}
            }).ToList();

            List<Col> totalColumns = new()
            {
                new Col("Cargo %", ParagraphAlignment.Center),
                new Col("Cargo", ParagraphAlignment.Center, "C"),
                new Col("IVA", ParagraphAlignment.Center, "C"),
                new Col("Total", ParagraphAlignment.Center, "C"),
            };

            var totales = new Dictionary<string, object>
            {
                { "Cargo %", $"{Math.Round(requestData.TypeChargeTotal.TotalDescuentoPorcentual, 2)}%" },
                { "Cargo", requestData.TypeChargeTotal.SumaDescuentos},
                { "IVA", requestData.TypeChargeTotal.IVACargo},
                { "Total", requestData.TypeChargeTotal.TotalCargo}
            };

            var branches = await GetBranchNames(filter.SucursalId);
            var doctors = await GetDoctorNames(filter.MedicoId);

            var headerData = new HeaderData()
            {
                NombreReporte = "Solicitudes con Cargo",
                Extra = doctors.Any() ? "Médico(s): " + string.Join(", ", doctors.Select(x => x)) : "Todos los Médicos",
                Sucursal = string.Join(", ", branches.Select(x => x)),
                Fecha = $"{filter.Fecha.Min():dd/MM/yyyy} - {filter.Fecha.Max():dd/MM/yyyy}"
            };

            var invoice = new InvoiceData()
            {
                Subtotal = requestData.TypeChargeTotal.SubtotalCargo,
                IVA = requestData.TypeChargeTotal.IVACargo,
                Total = requestData.TypeChargeTotal.TotalCargo,
            };

            var reportData = new ReportData()
            {
                Columnas = columns,
                Series = filter.Grafica ? series : null,
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
