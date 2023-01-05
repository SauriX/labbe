using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.Catalogs;
using Service.MedicalRecord.Dtos.RequestedStudy;
using Service.MedicalRecord.Dtos.Sampling;
using Service.MedicalRecord.Dictionary;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Service.MedicalRecord.Mapper
{
    public static class SamplingMapper
    {
        public static List<SamplingListDto> ToSamplingListDto(this List<Request> model, RequestedStudySearchDto search)
        {
            if (model == null) return null;

            if (search.Estatus != null && search.Estatus.Count > 0)
            {
                foreach (var request in model)
                {
                    request.Estudios = request.Estudios.Where(x => search.Estatus.Contains(x.EstatusId)).ToList();
                }
            }

            foreach (var request in model)
            {
                request.Estudios = request.Estudios.ToList();
            }

            return model.Where(x => x.Estudios.Count > 0).Select(x => new SamplingListDto
            {
                Solicitud = x.Clave,
                Nombre = x.Expediente.NombreCompleto,
                Registro = x.FechaCreo.ToString("dd/MM/yyyy HH:mm"),
                Sucursal = x.Sucursal.Nombre,
                Edad = x.Expediente.Edad.ToString(),
                Sexo = x.Expediente.Genero,
                Compañia = x.Compañia?.Nombre,
                Seleccion = false,
                Estudios = x.Estudios.ToStudySamplingDto(),
                Id = x.Id.ToString(),
                ExpedienteId = x.ExpedienteId.ToString(),
                ClavePatologica = x.ClavePatologica,
                Observacion = x.Observaciones
            }).ToList();
        }

        public static List<StudyDto> ToStudySamplingDto(this ICollection<RequestStudy> model)
        {
            return model.Select(x => new StudyDto
            {
                Id = x.EstudioId,
                SolicitudEstudioId = x.Id,
                Nombre = x.Nombre,
                Observacion = x.Solicitud.Observaciones,
                Area = "",
                Estatus = x.EstatusId,
                Registro = x.FechaCreo.ToString("dd/MM/yyyy HH:mm"),
                Entrega = x.FechaCreo.AddDays((double)x.Dias).ToString("dd/MM/yyyy HH:mm"),
                Seleccion = false,
                Clave = x.Clave,
                NombreEstatus = x.Estatus.Nombre,
                SolicitudId = x.SolicitudId,
                FechaActualizacion = x.EstatusId == Status.RequestStudy.Pendiente
                    ? x.FechaPendiente?.ToString("dd/MM/yyyy HH:mm") 
                    : x.EstatusId == Status.RequestStudy.TomaDeMuestra
                    ? x.FechaTomaMuestra?.ToString("dd/MM/yyyy HH:mm")
                    : x.EstatusId == Status.RequestStudy.Solicitado 
                    ? x.FechaSolicitado?.ToString("dd/MM/yyyy HH:mm")
                    : "",
                UsuarioActualizacion = x.EstatusId == Status.RequestStudy.Pendiente
                    ? x.UsuarioPendiente
                    : x.EstatusId == Status.RequestStudy.TomaDeMuestra
                    ? x.UsuarioTomaMuestra
                    : x.EstatusId == Status.RequestStudy.Solicitado
                    ? x.UsuarioSolicitado
                    : "",
                Urgencia = x.Solicitud.Urgencia,
            }).ToList();
        }

    }
}
