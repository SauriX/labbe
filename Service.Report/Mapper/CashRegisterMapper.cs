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
        public static CashDto ToCashRegisterDto(this IEnumerable<RequestPayment> model)
        {
            if (model == null) return null;

            var perday = CashGeneric(model);
            var canceled = CashGeneric(model.Where(x => x.Estatus == 3));
            var otherday = ToOtherDay(model);

            var data = new CashDto
            {
                PerDay = perday,
                Canceled = canceled,
                OtherDay = otherday,
            };

            return data;
        }


        public static List<CashRegisterDto> CashGeneric(IEnumerable<RequestPayment> model)
        {
            var results = model.Select(request =>
            {
                return new CashRegisterDto
                {
                    Id = Guid.NewGuid(),
                    Solicitud = request.Solicitud.Clave,
                    Paciente = request.Solicitud.Expediente.Nombre,
                    Factura = request.Factura,
                    Total = request.Total,
                    ACuenta = request.ACuenta,
                    Efectivo = request.Efectivo,
                    TDC = request.TDC,
                    Transferecia = request.Transferecia,
                    Cheque = request.Cheque,
                    TDD = request.TDD,
                    PP = request.PP,
                    Fecha = request.Fecha.ToString("HH:mm"),
                    UsuarioModifico = request.UsuarioModifico,
                    Saldo = request.Saldo,
                    Empresa = request.Empresa.NombreEmpresa,
                    Estatus = request.Estatus
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
                Transferecia = results.Sum(x => x.Transferecia),
                Cheque = results.Sum(x => x.Cheque),
                TDD = results.Sum(x => x.TDD),
                PP = results.Sum(x => x.PP),
                Fecha = " ",
                UsuarioModifico = " ",
                Saldo = results.Sum(x => x.Saldo),
                Empresa = " ",
                Estatus = 0,
            });

            return results;
        }

        public static List<CashRegisterDto> ToOtherDay(IEnumerable<RequestPayment> model)
        {
            var results = model.Where(x => x.Solicitud.Fecha != DateTime.Now).Select(request =>
            {
                return new CashRegisterDto
                {
                    Id = Guid.NewGuid(),
                    Solicitud = request.Solicitud.Clave,
                    Paciente = request.Solicitud.Expediente.Nombre,
                    Factura = request.Factura,
                    Total = request.Total,
                    ACuenta = request.ACuenta,
                    Efectivo = request.Efectivo,
                    TDC = request.TDC,
                    Transferecia = request.Transferecia,
                    Cheque = request.Cheque,
                    TDD = request.TDD,
                    PP = request.PP,
                    Fecha = request.Fecha.ToString("HH:mm"),
                    UsuarioModifico = request.UsuarioModifico,
                    Saldo = request.Saldo,
                    Empresa = request.Empresa.NombreEmpresa,
                    Estatus = request.Estatus
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
                Transferecia = results.Sum(x => x.Transferecia),
                Cheque = results.Sum(x => x.Cheque),
                TDD = results.Sum(x => x.TDD),
                PP = results.Sum(x => x.PP),
                Fecha = " ",
                UsuarioModifico = " ",
                Saldo = results.Sum(x => x.Saldo),
                Empresa = " ",
                Estatus = 0,
            });

            return results;
        }
    }
}
