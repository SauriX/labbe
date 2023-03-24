using Service.MedicalRecord.Dtos.Reports.BudgetStats;
using Service.MedicalRecord.Dtos.Reports.StudyStats;
using Service.MedicalRecord.Domain.Quotation;
using System.Collections.Generic;
using System.Linq;
using System;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.Request;
using Service.MedicalRecord.Dtos.Reports;

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
                var descount = studies.Sum(x => x.Descuento);
                var promotion = studies.Sum(x => x.Descuento) + pack.Sum(x => x.Descuento);
                var porcentualDescount = (descount * 100) / priceStudies;
                var descRequest = descount / 100;
                var charge = request.Cargo;
                var porcentualCharge = (charge * 100) / priceStudies;

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
                    DescuentoPorcentual = porcentualDescount,
                    Promocion = promotion,
                    Cargo = request.Cargo,
                    CargoPorcentual = porcentualCharge,
                    Copago = request.Copago,
                    PrecioEstudios = priceStudies,
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

        public static List<RequestPaymentStatsDto> RequestPayment(this IEnumerable<RequestPayment> payments, string user)
        {

            var paymentStats = payments
            .GroupBy(p => p.SolicitudId)
            .Select(g => new RequestPaymentStatsDto
            {
                SolicitudId = g.Key,
                Efectivo = g.Where(x => x.FormaPagoId == EFECTIVO).Sum(x => x.Cantidad),
                Cheque = g.Where(x => x.FormaPagoId == CHEQUE).Sum(x => x.Cantidad),
                Transferencia = g.Where(x => x.FormaPagoId == TRANSF).Sum(x => x.Cantidad),
                TDC = g.Where(x => x.FormaPagoId == TDC).Sum(x => x.Cantidad),
                PP = g.Where(x => x.FormaPagoId == PP).Sum(x => x.Cantidad),
                TDD = g.Where(x => x.FormaPagoId == TDD).Sum(x => x.Cantidad),
                OtroMetodo = g.Where(x => x.FormaPagoId != EFECTIVO && x.FormaPagoId != CHEQUE && x.FormaPagoId != TRANSF && x.FormaPagoId != TDC && x.FormaPagoId != PP && x.FormaPagoId != TDD).Sum(x => x.Cantidad)
            })
            .ToList();

            paymentStats.ForEach(ps =>
            {
                var solicitud = payments.LastOrDefault(p => p.SolicitudId == ps.SolicitudId)?.Solicitud;
                var date = payments.LastOrDefault().FechaPago;

                if (solicitud != null)
                {
                    ps.Solicitud = solicitud.Clave;
                    ps.FechaSolicitud = solicitud.FechaCreo;
                    ps.NombreCompleto = solicitud.Expediente?.NombreCompleto;
                    ps.Saldo = solicitud.Saldo;
                    ps.FormaPagoId = ps.FormaPagoId;
                    ps.FormaPago = ps.FormaPago;
                    ps.FechaPago = ps.FechaPago;
                    ps.EstatusId = ps.EstatusId;
                    ps.FacturaId = ps.FacturaId;
                    ps.FechaPago = date;
                    ps.Factura = ps.Factura;
                    ps.Estatus = solicitud.Estatus?.Nombre;
                    ps.CompañiaId = (Guid)solicitud.CompañiaId;
                    ps.Compañia = solicitud.Compañia?.Nombre;
                    ps.Total = solicitud.Total;
                    ps.UsuarioRegistra = user;
                }
            });

            return paymentStats;
        }
    }
}

