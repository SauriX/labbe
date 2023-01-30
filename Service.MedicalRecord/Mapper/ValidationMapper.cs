using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.Catalogs;
using Service.MedicalRecord.Dtos.ResultValidation;
using Service.MedicalRecord.Dtos.Sampling;
using System.Collections.Generic;
using System.Linq;


namespace Service.MedicalRecord.Mapper
{
    public static class ValidationMapper
    {
        public static List<ValidationListDto> ToValidationListDto(this List<Request> model)
        {
            if (model == null) return null;

            return model.Select(x => new ValidationListDto
            {
                Solicitud = x.Clave,
                Nombre = x.Expediente.NombreCompleto,
                Registro = x.FechaCreo.ToString(),
                Sucursal = x.Sucursal.Nombre,
                Edad = x.Expediente.Edad.ToString(),
                Sexo = x.Expediente.Genero,
                Compañia = x.Compañia?.Nombre,
                Estudios = x.Estudios.ToStudyValidationDto(x.Urgencia),
                Id = x.Id,
                Order = x.ExpedienteId.ToString()
            }).ToList();
        }
         
        public static List<ValidationStudyDto> ToStudyValidationDto(this ICollection<RequestStudy> model, byte urgencia)
        {
            List < ValidationStudyDto > studyList = new List < ValidationStudyDto >();



            foreach (var x in model) {

                studyList.Add(new ValidationStudyDto
                {
                    Id = x.Id,
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
                    NombreEstatus = x.Estatus.Nombre,
                    SolicitudId = x.SolicitudId,
                    Nombre = x.Nombre,
                    Clave = x.Clave.ToString(),
                    Tipo = urgencia == 1 ? true : false,
               

                });

            }


            return studyList;
        }
    }
}
