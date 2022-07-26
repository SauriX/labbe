using Service.Report.Dictionary;
using Service.Report.Domain.Request;
using Service.Report.Dtos.ContactStats;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Mapper
{
    public static class ContactStatsMapper
    {
        public static IEnumerable<ContactStatsDto> ToContactStatsDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;

            var results = (from c in model
                           group c by new { c.Expediente.Expediente, c.Expediente.Nombre, c.Expediente.Celular, c.Expediente.Correo, c.Medico.NombreMedico, c.SolicitudId, c.Clave } into grupo
                           select new ContactStatsDto
                           {
                               Expediente = grupo.Key.Expediente,
                               Paciente = grupo.Key.Nombre,
                               Medico = grupo.Key.NombreMedico,
                               Clave = grupo.Key.Clave,
                               Estatus = grupo.Sum(x => x.Estudios.Count) == 0 ? "" : GetStatus(grupo.SelectMany(x => x.Estudios)),
                               Celular = grupo.Key.Celular,
                               Correo = grupo.Key.Correo
                           }).ToList();

            return results;
        }
        public static IEnumerable<ContactStatsChartDto> ToContactStatsChartDto(this IEnumerable<Request> model)
        {
            if (model == null) return null;

            var results = (from c in model
                           group c by new { c.Fecha.Year, c.Fecha.Month } into grupo
                           select new ContactStatsChartDto
                           {
                               Fecha = new DateTime(grupo.Key.Year, grupo.Key.Month, 1).ToString("MM/yyyy"),
                               Solicitudes = grupo.Count(),
                               CantidadTelefono = grupo.GroupBy(x => new { x.Expediente.Expediente, x.Expediente.Celular}).Count(x => !string.IsNullOrWhiteSpace(x.Key.Celular)),
                               CantidadCorreo = grupo.GroupBy(x => new { x.Expediente.Expediente, x.Expediente.Correo }).Count(x => !string.IsNullOrWhiteSpace(x.Key.Correo)),
                           }).ToList();

            return results;
        }

        private static string GetStatus(IEnumerable<RequestStudy> studies)
        {
            string status = string.Empty;
            if (studies == null || !studies.Any()) return status;

            if (studies.Any(x => x.EstatusId.In(Status.Request.Pendiente, Status.Request.TomaDeMuestra, Status.Request.Solicitado, Status.Request.Capturado, Status.Request.Validado, Status.Request.EnRuta)))
            {
                status = "Vigente";
            }
            else if (studies.All(x => x.EstatusId.In(Status.Request.Enviado, Status.Request.Liberado, Status.Request.Entregado)))
            {
                status = "Completado";
            }
            else if (studies.All(x => x.EstatusId == Status.Request.Cancelado))
            {
                status = "Cancelado";
            }

            return status;
        }

    }
}
