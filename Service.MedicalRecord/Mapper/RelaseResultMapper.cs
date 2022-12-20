using Service.MedicalRecord.Dtos.RelaseResult;
using System.Collections.Generic;
using System.Linq;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.Catalogs;



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
                Registro = x.FechaModifico !=null? x.FechaModifico.ToString() : x.FechaCreo.ToString(),
                Entrega = x.FechaEntrega.ToString(),
                Tipo = urgencia == 1 ? true : false, 
                SolicitudId = x.SolicitudId,
                Nombre = x.Nombre,
                Clave = x.Clave.ToString(),

            }).ToList();
        }
    }
}
