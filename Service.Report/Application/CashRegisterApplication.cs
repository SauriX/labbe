using Integration.Pdf.Models;
using Service.Report.Application.IApplication;
using Service.Report.Client.IClient;
using Service.Report.Domain.Catalogs;
using Service.Report.Dtos;
using Service.Report.Dtos.CashRegister;
using Service.Report.Mapper;
using Service.Report.PdfModel;
using Service.Report.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Application
{
    public class CashRegisterApplication : BaseApplication, ICashRegisterApplication
    {
        public readonly IReportRepository _repository;
        private readonly IPdfClient _pdfClient;

        public CashRegisterApplication(IReportRepository repository, IPdfClient pdfClient, IRepository<Branch> branchRepository) : base(branchRepository)
        {
            _repository = repository;
            _pdfClient = pdfClient;
        }

        public async Task<CashDto> GetByFilter(ReportFilterDto filter)
        {
            var data = await _repository.GetPaymentByFilter(filter);
            var results = data.ToCashRegisterDto();

            return results;
        }

        public async Task<byte[]> DownloadReportPdf(ReportFilterDto filter)
        {
            var requestData = await GetByFilter(filter);

            List<Col> columns = new()
            {
                new Col("Solicitud", ParagraphAlignment.Left),
                new Col("Paciente", ParagraphAlignment.Left),
                new Col("Factura", ParagraphAlignment.Left),
                new Col("Total", ParagraphAlignment.Right, "C"),
                new Col("A cuenta", ParagraphAlignment.Right, "C"),
                new Col("Efectivo", ParagraphAlignment.Right),
                new Col("TDC", ParagraphAlignment.Right, "C"),
                new Col("Tranf. E", ParagraphAlignment.Right, "C"),
                new Col("Cheque", ParagraphAlignment.Right, "C"),
                new Col("TDD", ParagraphAlignment.Right, "C"),
                new Col("PP", ParagraphAlignment.Right, "C"),
                new Col("Hora", ParagraphAlignment.Left),
                new Col("Usuario", ParagraphAlignment.Left),
                new Col("Saldo", ParagraphAlignment.Right, "C"),
                new Col("Compañía", ParagraphAlignment.Left),
            };

            var perDay = requestData.PerDay.Select(x => new Dictionary<string, object>
            {
                { "Solicitud", x.Solicitud },
                { "Paciente", x.Paciente },
                { "Factura", x.Factura },
                { "Total", x.Total },
                { "A cuenta", x.ACuenta },
                { "Efectivo", x.Efectivo },
                { "TDC", x.TDC },
                { "Tranf. E", x.Transferencia },
                { "Cheque", x.Cheque },
                { "TDD", x.TDD },
                { "PP", x.PP },
                { "Hora", x.Fecha},
                { "Usuario", x.UsuarioModifico},
                { "Saldo", x.Saldo},
                { "Compañía", x.Empresa},
            }).ToList();

            var canceled = requestData.Canceled.Select(x => new Dictionary<string, object>
            {
                { "Solicitud", x.Solicitud },
                { "Paciente", x.Paciente },
                { "Factura", x.Factura },
                { "Total", x.Total },
                { "A cuenta", x.ACuenta },
                { "Efectivo", x.Efectivo },
                { "TDC", x.TDC },
                { "Tranf. E", x.Transferencia },
                { "Cheque", x.Cheque },
                { "TDD", x.TDD },
                { "PP", x.PP },
                { "Hora", x.Fecha},
                { "Usuario", x.UsuarioModifico},
                { "Saldo", x.Saldo},
                { "Compañía", x.Empresa},
            }).ToList();

            var otherDay = requestData.OtherDay.Select(x => new Dictionary<string, object>
            {
                { "Solicitud", x.Solicitud },
                { "Paciente", x.Paciente },
                { "Factura", x.Factura },
                { "Total", x.Total },
                { "A cuenta", x.ACuenta },
                { "Efectivo", x.Efectivo },
                { "TDC", x.TDC },
                { "Tranf. E", x.Transferencia },
                { "Cheque", x.Cheque },
                { "TDD", x.TDD },
                { "PP", x.PP },
                { "Hora", x.Fecha},
                { "Usuario", x.UsuarioModifico},
                { "Saldo", x.Saldo},
                { "Compañía", x.Empresa},
            }).ToList();

            List<Col> totalColumns = new()
            {
                new Col("Efectivo", ParagraphAlignment.Center, "C"),
                new Col("TDC", ParagraphAlignment.Center, "C"),
                new Col("Transf. E", ParagraphAlignment.Center, "C"),
                new Col("Cheque", ParagraphAlignment.Center, "C"),
                new Col("TDD", ParagraphAlignment.Center, "C"),
            };

            var totales = new Dictionary<string, object>
            {
                { "Efectivo", requestData.CashTotal.SumaEfectivo },
                { "TDC", requestData.CashTotal.SumaTDC},
                { "Transf. E", requestData.CashTotal.SumaTransferencia},
                { "Cheque", requestData.CashTotal.SumaCheque},
                { "TDD", requestData.CashTotal.SumaTDD}
            };

            var invoice = new InvoiceData()
            {
                Total = requestData.CashTotal.Total,
            };

            var branches = await GetBranchNames(filter.SucursalId);

            var headerData = new HeaderData()
            {
                Sucursal = branches.Any() ? string.Join(", ", branches.Select(x => x)) : "Todas las Sucursales",
                Fecha = filter.FechaIndividual.ToString("dd/MM/yyyy"),
                Hora = $"DE LAS {filter.Hora.Min():HH:mm} A LAS {filter.Hora.Max():HH:mm}"
            };

            var cashData = new CashData()
            {
                Columnas = columns,
                PerDay = perDay,
                Canceled = canceled,
                OtherDay = otherDay,
                Header = headerData,
                ColumnasTotales = totalColumns,
                Totales = totales,
                Invoice = invoice,
                User = filter.User
            };

            var file = await _pdfClient.CashRegisterReport(cashData);

            return file;
        }
    }
}
