using Service.MedicalRecord.Dtos.Reports;
using Service.MedicalRecord.Domain.Request;
using System.Collections.Generic;
using System.Linq;
using System;
using Service.MedicalRecord.Dtos.Reports.StudyStats;
using Service.MedicalRecord.Dictionary;

namespace Service.MedicalRecord.Mapper.Reports
{
    public static class StudyStatsMapper
    {
        public static IEnumerable<ReportInfoDto> ToStudyStatsDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;

            var results = (from c in model
                           group c by new { c.Id, c.Clave, c.Expediente.NombreCompleto, c.Medico.Nombre, c.FechaCreo, c.Expediente, c.Parcialidad } into grupo
                           select grupo)
                           .Select(grupo =>
                           {
                               var studies = grupo.SelectMany(x => x.Estudios);
                               var dueDate = studies.Max(x => x.Dias);

                               return new ReportInfoDto
                               {
                                   Id = Guid.NewGuid(),
                                   Solicitud = grupo.Key.Clave,
                                   Paciente = grupo.Key.NombreCompleto,
                                   Edad = grupo.Key.Expediente.Edad,
                                   Sexo = grupo.Key.Expediente.Genero,
                                   Estudio = studies.ToStudiesDto(),
                                   Medico = grupo.Key.Nombre,
                                   FechaEntrega = grupo.Key.FechaCreo.AddDays((double)dueDate).ToString("dd/MM/yyyy"),
                                   Fecha = grupo.Key.FechaCreo.ToString("dd/MM/yyyy"),
                                   Parcialidad = grupo.Key.Parcialidad,
                               };
                           });

            return results;
        }

        public static IEnumerable<ReportInfoDto> ToUrgentStatsDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;

            var results = (from c in model.Where(x => x.Urgencia != 1)
                           group c by new { c.Id, c.Clave, c.Expediente.NombreCompleto, c.Medico.Nombre, c.FechaCreo, c.Expediente, c.Urgencia } into grupo
                           select grupo)
                           .Select(grupo =>
                           {
                               var studies = grupo.SelectMany(x => x.Estudios);
                               var dueDate = studies.Max(x => x.Dias);

                               return new ReportInfoDto
                               {
                                   Id = Guid.NewGuid(),
                                   Solicitud = grupo.Key.Clave,
                                   Paciente = grupo.Key.Nombre,
                                   Edad = grupo.Key.Expediente.Edad,
                                   Sexo = grupo.Key.Expediente.Genero,
                                   Estudio = studies.ToStudiesDto(),
                                   Medico = grupo.Key.Nombre,
                                   FechaEntrega = grupo.Key.FechaCreo.AddDays((double)dueDate).ToString("dd/MM/yyyy"),
                                   Urgencia = grupo.Key.Urgencia,
                               };
                           });

            return results;
        }

        public static IEnumerable<StudyStatsChartDto> ToStudyStatsChartDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;
            var studies = model.Where(x => x.Urgencia != 1).SelectMany(x => x.Estudios);

            var results = (from c in studies
                           group c by new { c.Estatus.Nombre, c.EstatusId } into grupo
                           select new StudyStatsChartDto
                           {
                               Id = Guid.NewGuid(),
                               Estatus = grupo.Key.Nombre,
                               Cantidad = grupo.Count(),
                               Color = GetColor(grupo.Key.EstatusId),
                           });


            return results;
        }

        public static IEnumerable<StudyStatsChartDto> ToUrgentStatsChartDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;
            var studies = model.Where(x => x.Urgencia != 1).SelectMany(x => x.Estudios);

            var results = (from c in studies
                           group c by new { c.Estatus.Nombre, c.EstatusId } into grupo
                           select new StudyStatsChartDto
                           {
                               Id = Guid.NewGuid(),
                               Estatus = grupo.Key.Nombre,
                               Cantidad = grupo.Count(),
                               Color = GetColor(grupo.Key.EstatusId),
                           });


            return results;
        }

        public static List<StudiesDto> ToStudiesDto(this IEnumerable<RequestStudy> studies)
        {
            return studies.Select(x => new StudiesDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Nombre = x.Nombre,
                Estatus = x.Estatus.Nombre,
            }).ToList();
        }

        public static string GetColor(byte statusId)
        {
            if (statusId == Status.RequestStudy.Pendiente) return "#536FC6";
            else if (statusId == Status.RequestStudy.TomaDeMuestra) return "#91CC75";
            else if (statusId == Status.RequestStudy.Solicitado) return "#F9C857";
            else if (statusId == Status.RequestStudy.Capturado) return "#71BFDD";
            else if (statusId == Status.RequestStudy.Validado) return "#3AA271";
            else if (statusId == Status.RequestStudy.EnRuta) return "#FB8350";
            else if (statusId == Status.RequestStudy.Liberado) return "#9960B3";
            else if (statusId == Status.RequestStudy.Enviado) return "#EA7BCC";
            else if (statusId == Status.RequestStudy.Entregado) return "#E7EFC5";
            else if (statusId == Status.RequestStudy.Cancelado) return "#EE6665";

            return "#2f54eb";
        }

        public static List<StudiesDto> PromotionStudies(this IEnumerable<RequestStudy> studies)
        {
            return studies.Select(x => new StudiesDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Nombre = x.Nombre,
                Estatus = x.Estatus.Nombre,
                Precio = x.Precio,
                Descuento = x.Descuento,
                Paquete = x.Paquete?.Nombre,
                Promocion = x.Paquete?.Descuento / studies.Count(),
                PrecioFinal = x.Precio - (x.Precio * x.Paquete?.DescuentoPorcentaje ?? 0) - (x.Descuento == 0 ? 0 : x.Descuento),
            }).ToList();
        }
    }
}
