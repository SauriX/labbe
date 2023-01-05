using Service.MedicalRecord.Dtos.RelaseResult;
using System.Collections.Generic;
using System.Linq;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.Catalogs;
using Service.MedicalRecord.Dictionary;

namespace Service.MedicalRecord.Mapper
{
    public static class RelaseResultMapper
    {
        public static List<RelaceList> ToRelaseListDto(this List<Request> model)
        {
            if (model == null) return null;

            return model.Select(x => new RelaceList
            {
                Solicitud = x.Clave,
                Nombre = x.Expediente.NombreCompleto,
                Registro = x.FechaCreo.ToString(),
                Sucursal = x.Sucursal.Nombre,
                Edad = x.Expediente.Edad.ToString(),
                Sexo = x.Expediente.Genero,
                Compañia = x.Compañia?.Nombre,
                Estudios = x.Estudios.ToRelasestudyDto(x.Urgencia),
                Id = x.Id,
                Order = x.ExpedienteId.ToString(),
                Parcialidad = x.Parcialidad,
                
            }).ToList();
        }

        public static List<RelaceStudyDto> ToRelasestudyDto(this ICollection<RequestStudy> model,byte urgencia)
        {
            return model.Select(x => new RelaceStudyDto
            {
                Id = x.EstudioId,
                Study = $"{x.Clave}-{x.Nombre}",
                Area = "",
                Status = x.Estatus.Nombre,
                Estatus = x.EstatusId,
                Registro = x.EstatusId == Status.RequestStudy.Pendiente
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
                Entrega = x.FechaEntrega.ToString(),
                Tipo = urgencia == 1 ? true : false, 
                SolicitudId = x.SolicitudId,
                Nombre = x.Nombre,
                Clave = x.Clave.ToString(),

            }).ToList();
        }
    }
}
