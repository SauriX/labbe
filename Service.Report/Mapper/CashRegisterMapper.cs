using Service.Report.Domain.MedicalRecord;
using Service.Report.Domain.Request;
using Service.Report.Dtos;
using Service.Report.Dtos.CashRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Mapper
{
    public static class CashRegisterMapper
    {
        public static CashDto ToCashRegisterDto(this IEnumerable<RequestRegister> model)
        {
            if (model == null) return null;

            var filter = new ReportFilterDto();

            var perday = CashGeneric(model.Where(x => x.FechaPago.Date == x.FechaSolicitud.Date));
            var canceled = CashGeneric(model.Where(x => x.EstatusId == 3 && x.FechaPago.Date == x.FechaSolicitud.Date));
            var otherday = CashGeneric(model.Where(x => x.FechaPago.Date != x.FechaSolicitud.Date));

            var totals = new CashInvoiceDto
            {
                SumaEfectivo = perday.Select(x => x.Efectivo).LastOrDefault() - canceled.Select(x => x.Efectivo).LastOrDefault() + otherday.Select(x => x.Efectivo).LastOrDefault(),
                SumaTDC = perday.Select(x => x.TDC).LastOrDefault() - canceled.Select(x => x.TDC).LastOrDefault() + otherday.Select(x => x.TDC).LastOrDefault(),
                SumaTransferencia = perday.Select(x => x.Transferencia).LastOrDefault() - canceled.Select(x => x.Transferencia).LastOrDefault() + otherday.Select(x => x.Transferencia).LastOrDefault(),
                SumaCheque = perday.Select(x => x.Cheque).LastOrDefault() - canceled.Select(x => x.Cheque).LastOrDefault() + otherday.Select(x => x.Cheque).LastOrDefault(),
                SumaTDD = perday.Select(x => x.TDD).LastOrDefault() - canceled.Select(x => x.TDD).LastOrDefault() + otherday.Select(x => x.TDD).LastOrDefault(),
                SumaPP = perday.Select(x => x.PP).LastOrDefault() - canceled.Select(x => x.PP).LastOrDefault() + otherday.Select(x => x.PP).LastOrDefault(),
                SumaOtroMetodo = perday.Select(x => x.OtroMetodo).LastOrDefault() - canceled.Select(x => x.OtroMetodo).LastOrDefault() + otherday.Select(x => x.OtroMetodo).LastOrDefault(),
            };

            var data = new CashDto
            {
                PerDay = perday,
                Canceled = canceled,
                OtherDay = otherday,
                CashTotal = totals,
            };

            return data;
        }


        public static List<CashRegisterDto> CashGeneric(IEnumerable<RequestRegister> model)
        {
            var results = model.Select(request =>
            {
                return new CashRegisterDto
                {
                    Id = Guid.NewGuid(),
                    Solicitud = request.Solicitud,
                    Paciente = request.NombreCompleto,
                    Factura = request.Factura ?? "",
                    Total = request.Total,
                    ACuenta = request.Factura != null ? request.Cantidad : 0m,
                    Efectivo = request.Efectivo,
                    TDC = request.TDC,
                    Transferencia = request.Transferencia,
                    Cheque = request.Cheque,
                    TDD = request.TDD,
                    PP = request.PP,
                    OtroMetodo = request.OtroMetodo,
                    Fecha = request.FechaPago.ToString("g"),
                    UsuarioRegistra = request.UsuarioRegistra,
                    Saldo = request.Saldo,
                    Empresa = request.Compañia,
                    Estatus = request.EstatusId
                };
            }).ToList();

            results.Add(new CashRegisterDto
            {
                Id = Guid.NewGuid(),
                Solicitud = " ",
                Paciente = " ",
                Factura = "Total",
                Total = results.Sum(x => x.Total),
                ACuenta = results.Sum(x => x.ACuenta),
                Efectivo = results.Sum(x => x.Efectivo),
                TDC = results.Sum(x => x.TDC),
                Transferencia = results.Sum(x => x.Transferencia),
                Cheque = results.Sum(x => x.Cheque),
                TDD = results.Sum(x => x.TDD),
                PP = results.Sum(x => x.PP),
                OtroMetodo = results.Sum(x => x.OtroMetodo),
                Fecha = " ",
                UsuarioRegistra = " ",
                Saldo = results.Sum(x => x.Saldo),
                Empresa = " ",
                Estatus = 0,
            });

            return results;
        }
    }
}
