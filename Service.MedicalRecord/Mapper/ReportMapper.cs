using Service.MedicalRecord.Dtos.Reports.BudgetStats;
using Service.MedicalRecord.Dtos.Reports.StudyStats;
using Service.MedicalRecord.Domain.Quotation;
using System.Collections.Generic;
using System.Linq;
using System;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.Request;
using Service.MedicalRecord.Dtos.Reports;
using Service.MedicalRecord.Dtos.Reports.CashRegister;

namespace Service.MedicalRecord.Mapper
{
    public static class ReportMapper
    {
        private const int EFECTIVO = 1;
        private const int CHEQUE = 2;
        private const int TRANSF = 3;
        private const int TDC = 4;
        private const int PP = 5;
        private const int TDD = 18;

        public static IEnumerable<BudgetStatsDto> ToQuotationReportDto(this IEnumerable<Quotation> model)
        {
            if (model == null) return null;

            var results = QuotationReportGeneric(model);

            return results;
        }

        public static List<BudgetStatsDto> QuotationReportGeneric(IEnumerable<Quotation> model)
        {
            return model.Where(x => x.Estudios.Count > 0).Select(request =>
            {
                var studies = request.Estudios;
                var pack = request.Paquetes;

                var priceStudies = studies.Sum(x => x.Precio - (x.Precio * x.Paquete?.DescuentoPorcentaje ?? 0) - (x.Descuento == 0 ? 0 : x.Descuento));
                var descount = studies.Sum(x => x.Descuento);
                var promotion = studies.Sum(x => x.Descuento) + pack.Sum(x => x.Descuento);
                var porcentualDescount = (descount * 100) / priceStudies;
                var descRequest = descount / 100;

                return new BudgetStatsDto
                {
                    Id = Guid.NewGuid(),
                    Solicitud = request.Clave,
                    Sucursal = request.Sucursal.Nombre,
                    SucursalId = request.Sucursal.Id,
                    NombrePaciente = request.Expediente?.NombreCompleto,
                    NombreMedico = request.Medico?.Nombre,
                    Estudio = studies.QuotationStudies(),
                    Descuento = descount,
                    DescuentoPorcentual = porcentualDescount,
                    Promocion = promotion,
                    Fecha = request.FechaCreo,
                };
            }).ToList();
        }

        public static List<StudiesDto> QuotationStudies(this IEnumerable<QuotationStudy> studies)
        {
            return studies.Select(x => new StudiesDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Nombre = x.Nombre,
                Precio = x.Precio,
                Descuento = x.Descuento,
                Paquete = x.Paquete?.Nombre,
                Promocion = x.Paquete?.Descuento / studies.Count(),
                PrecioFinal = x.Precio - (x.Precio * x.Paquete?.DescuentoPorcentaje ?? 0) - (x.Descuento == 0 ? 0 : x.Descuento),
                Solicitud = new ReportInfoDto
                {
                    Id = x.Cotizacion.Id,
                    Solicitud = x.Cotizacion.Clave,
                    Expediente = x.Cotizacion.Expediente?.Expediente,
                    NombreCompleto = x.Cotizacion.Expediente?.NombreCompleto,
                    Edad = x.Cotizacion.Expediente?.Edad,
                    Sexo = x.Cotizacion.Expediente?.Genero,
                    Sucursal = x.Cotizacion.Sucursal.Nombre,
                    Medico = x.Cotizacion.Medico?.Nombre,
                    Fecha = x.Cotizacion.FechaCreo,
                },
            }).ToList();
        }

        public static IEnumerable<ReportInfoDto> ToReportRequestDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;

            return model.Where(x => x.Estudios.Count > 0).Select(request =>
            {
                var studies = request.Estudios;
                var pack = request.Paquetes;

                var priceStudies = studies.Sum(x => x.Precio - (x.Precio * x.Paquete?.DescuentoPorcentaje ?? 0) - (x.Descuento == 0 ? 0 : x.Descuento));
                var promotion = studies.Sum(x => x.Descuento) + pack.Sum(x => x.Descuento);
                var charge = request.Cargo;
                var porcentualCharge = request.TotalEstudios != 0 ? (charge * 100) / request.TotalEstudios : 0;

                return new ReportInfoDto
                {
                    Id = request.Id,
                    Solicitud = request.Clave,
                    ExpedienteId = request.ExpedienteId,
                    Expediente = request.Expediente?.Expediente,
                    NombreCompleto = request.Expediente?.NombreCompleto,
                    Edad = request.Expediente?.Edad,
                    Sexo = request.Expediente?.Genero,
                    Celular = request.Expediente?.Celular,
                    Correo = request.Expediente?.Correo,
                    ClavePatalogica = request.ClavePatologica ?? "",
                    SucursalId = request.SucursalId,
                    Sucursal = request.Sucursal.Nombre,
                    Ciudad = request.Sucursal.Ciudad,
                    EstatusId = request.EstatusId,
                    NombreEstatus = request.Estatus?.Nombre,
                    Procedencia = request.Procedencia,
                    CompañiaId = request.CompañiaId,
                    Compañia = request.Compañia?.Nombre,
                    MedicoId = request.MedicoId,
                    Medico = request.Medico?.Nombre,
                    ClaveMedico = request.Medico?.Clave,
                    Urgencia = request.Urgencia,
                    Parcialidad = request.Parcialidad,
                    TotalEstudios = request.TotalEstudios,
                    Descuento = request.Descuento,
                    DescuentoPorcentual = 0m,
                    Promocion = promotion,
                    Cargo = request.Cargo,
                    CargoPorcentual = porcentualCharge,
                    Copago = request.Copago,
                    PrecioEstudios = request.TotalEstudios,
                    Total = request.Total,
                    Fecha = request.FechaCreo,
                    Estudios = studies.RequestStudies(),
                };
            }).ToList();
        }

        public static List<StudiesDto> RequestStudies(this IEnumerable<RequestStudy> studies)
        {
            return studies.Select(x => new StudiesDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Nombre = x.Nombre,
                EstatusId = x.EstatusId,
                Estatus = x.Estatus.Nombre,
                MaquilaId = x.MaquilaId,
                Maquila = x.Maquila?.Nombre ?? "",
                Precio = x.Precio,
                Descuento = x.Descuento,
                DescuentoPorcentaje = x.DescuentoPorcentaje,
                PaqueteId = x.PaqueteId,
                Paquete = x.Paquete?.Nombre ?? "",
                Promocion = x.Paquete?.Descuento ?? 0 / studies.Count(),
                PrecioFinal = x.Precio - (x.Precio * x.Paquete?.DescuentoPorcentaje ?? 0) - (x.Descuento == 0 ? 0 : x.Descuento),
                Solicitud = new ReportInfoDto
                {
                    Id = x.Solicitud.Id,
                    Solicitud = x.Solicitud.Clave,
                    ExpedienteId = x.Solicitud.ExpedienteId,
                    Expediente = x.Solicitud.Expediente?.Expediente,
                    NombreCompleto = x.Solicitud.Expediente?.NombreCompleto,
                    Edad = x.Solicitud.Expediente?.Edad,
                    Sexo = x.Solicitud.Expediente?.Genero,
                    Sucursal = x.Solicitud.Sucursal.Nombre,
                    Medico = x.Solicitud.Medico?.Nombre,
                    Fecha = x.Solicitud.FechaCreo,
                },
            }).ToList();
        }

        public static CashDto RequestPayment(this IEnumerable<RequestPayment> model)
        {

            if (model == null) return null;

            List<CashRegisterDto> perday = CashGeneric(model.Where(x => x.EstatusId != 3 && x.FechaPago.Date == x.Solicitud.FechaCreo.Date));
            List<CashRegisterDto> canceled = CashGeneric(model.Where(x => x.EstatusId == 3));
            List<CashRegisterDto> otherday = CashGeneric(model.Where(x => x.EstatusId != 3 && x.FechaPago.Date != x.Solicitud.FechaCreo.Date));

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

        public static List<CashRegisterDto> CashGeneric(IEnumerable<RequestPayment> model)
        {
            var results = model.Select(request =>
            {
                var otherMetod = request.FormaPagoId != EFECTIVO && request.FormaPagoId != TDC && request.FormaPagoId != TDD && request.FormaPagoId != CHEQUE && request.FormaPagoId != PP;

                return new CashRegisterDto
                {
                    Id = Guid.NewGuid(),
                    Solicitud = request.Solicitud.Clave,
                    Paciente = request.Solicitud.Expediente?.NombreCompleto,
                    Factura = request.Serie ?? "",
                    Total = request.Solicitud.Total,
                    ACuenta = request.Serie != null ? request.Cantidad : 0m,
                    Efectivo = request.FormaPagoId == EFECTIVO ? request.Cantidad : 0m,
                    TDC = request.FormaPagoId == TDC ? request.Cantidad : 0m,
                    Transferencia = request.FormaPagoId == TRANSF ? request.Cantidad : 0m,
                    Cheque = request.FormaPagoId == CHEQUE ? request.Cantidad : 0m,
                    TDD = request.FormaPagoId == TDD ? request.Cantidad : 0m,
                    PP = request.FormaPagoId == PP ? request.Cantidad : 0m,
                    OtroMetodo = otherMetod ? request.Cantidad : 0m,
                    Fecha = request.FechaPago.ToString("g"),
                    UsuarioRegistra = request.UsuarioRegistra,
                    Saldo = request.Solicitud.Saldo,
                    Empresa = request.Solicitud.Compañia?.Nombre,
                    Estatus = request.EstatusId,
                    
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

