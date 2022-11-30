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

            if (search.Estatus != null)
            {
                foreach (var request in model)
                {
                    request.Estudios = request.Estudios.Where(x => search.Estatus.Contains(x.EstatusId)).ToList();
                }
            }

            foreach (var request in model)
            {
                request.Estudios = request.Estudios.Where(x => x.EstatusId == Status.RequestStudy.TomaDeMuestra || x.EstatusId == Status.RequestStudy.Pendiente).ToList();
            }

            return model.Select(x => new SamplingListDto
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
            }).ToList();
        }

        public static List<StudyDto> ToStudySamplingDto(this ICollection<RequestStudy> model)
        {
            return model.Select(x => new StudyDto
            {
                Id = x.EstudioId,
                Nombre = x.Nombre,
                Area = "",
                Estatus = x.EstatusId,
                Registro = x.FechaCreo.ToString(),
                Entrega = x.FechaCreo.AddDays((double)x.Dias).ToString(),
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
                    : x.EstatusId == Status.RequestStudy.Capturado
                    ? x.FechaCaptura?.ToString("dd/MM/yyyy HH:mm")
                    : x.EstatusId == Status.RequestStudy.Validado
                    ? x.FechaValidacion?.ToString("dd/MM/yyyy HH:mm")
                    : x.EstatusId == Status.RequestStudy.Liberado
                    ? x.FechaLiberado?.ToString("dd/MM/yyyy HH:mm")
                    : x.EstatusId == Status.RequestStudy.Enviado
                    ? x.FechaEnviado?.ToString("dd/MM/yyyy HH:mm")
                    : "",
                UsuarioActualizacion = x.EstatusId == Status.RequestStudy.Pendiente
                    ? x.UsuarioPendiente
                    : x.EstatusId == Status.RequestStudy.TomaDeMuestra
                    ? x.UsuarioTomaMuestra
                    : x.EstatusId == Status.RequestStudy.Solicitado
                    ? x.UsuarioSolicitado
                    : x.EstatusId == Status.RequestStudy.Capturado
                    ? x.UsuarioCaptura
                    : x.EstatusId == Status.RequestStudy.Validado
                    ? x.UsuarioValidacion
                    : x.EstatusId == Status.RequestStudy.Liberado
                    ? x.UsuarioLiberado
                    : x.EstatusId == Status.RequestStudy.Enviado
                    ? x.UsuarioEnviado
                    : "",
            }).ToList();
        }
    }
}
