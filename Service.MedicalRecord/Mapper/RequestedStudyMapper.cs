using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.RequestedStudy;
using Service.MedicalRecord.Dtos.Sampling;
using System.Collections.Generic;
using Service.MedicalRecord.Dictionary;
using System.Linq;

namespace Service.MedicalRecord.Mapper
{
    public static class RequestedStudyMapper
    {
        public static List<SamplingListDto> ToRequestedStudyDto(this List<Request> model, RequestedStudySearchDto search)
        {
            if (model == null) return null;

            if(search.Estatus != null && search.Estatus.Count > 0)
            {
                foreach (var request in model)
                {
                    request.Estudios = request.Estudios.Where(x => search.Estatus.Contains(x.EstatusId)).ToList();
                }
            }

            foreach (var request in model)
            {
                request.Estudios = request.Estudios.Where(x => x.EstatusId == Status.RequestStudy.TomaDeMuestra || x.EstatusId == Status.RequestStudy.Solicitado).ToList();
            }

            return model.Where(x => x.Estudios.Count > 0).Select(x => new SamplingListDto
            {
                Solicitud = x.Clave,
                Nombre = x.Expediente.NombreCompleto,
                Order = x.ExpedienteId.ToString(),
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
    }
}
