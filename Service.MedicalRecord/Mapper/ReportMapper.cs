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
                    Expediente = request.Expediente.Expediente,
                    NombreCompleto = request.Expediente.NombreCompleto,
                    Edad = request.Expediente.Edad,
                    Sexo = request.Expediente.Genero,
                    Celular = request.Expediente.Celular,
                    Correo = request.Expediente.Correo,
                    ClavePatalogica = request.ClavePatologica,
                    SucursalId = request.SucursalId,
                    Sucursal = request.Sucursal.Nombre,
                    EstatusId = request.EstatusId,
                    NombreEstatus = request.Estatus.Nombre,
                    Procedencia = request.Procedencia,
                    CompañiaId = (Guid)request.CompañiaId,
                    Compañia = request.Compañia.Nombre,
                    MedicoId = (Guid)request.MedicoId,
                    Medico = request.Medico.Nombre,
                    ClaveMedico = request.Medico.Clave,
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
                Precio = x.Precio,
                Descuento = x.Descuento,
                Paquete = x.Paquete?.Nombre,
                Promocion = x.Paquete?.Descuento / studies.Count(),
                PrecioFinal = x.Precio - (x.Precio * x.Paquete?.DescuentoPorcentaje ?? 0) - (x.Descuento == 0 ? 0 : x.Descuento),
            }).ToList();
        }
    }
}

