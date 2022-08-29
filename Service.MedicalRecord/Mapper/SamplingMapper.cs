using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.Sampling;
using System.Collections.Generic;
using System.Linq;


namespace Service.MedicalRecord.Mapper
{
    public static class SamplingMapper
    {
        public static List<SamplingListDto> ToSamplingListDto(this List<Service.MedicalRecord.Domain.Request.Request> model)
        {
            if (model == null) return null;

            return model.Select(x => new SamplingListDto
            {
                Solicitud = x.Clave,
                Nombre = x.Expediente.NombreCompleto,
                Registro = x.FechaCreo.ToString(),
                Sucursal = x.Sucursal.Nombre,
                Edad = x.Expediente.Edad.ToString(),
                Sexo = x.Expediente.Genero,
                Compañia = x.Compañia?.Nombre,
                Seleccion = false,
                Estudios = x.Estudios.ToStudySamplingDto(),
                Id = x.Id.ToString(),
                Order = x.ExpedienteId.ToString()
            }).ToList();
        }

        public static List<StudyDto> ToStudySamplingDto(this ICollection<RequestStudy> model)
        {


            return model.Select(x => new StudyDto
            {
                Id = x.EstudioId,
                Nombre = x.Nombre,
                Area = "",
                Status = x.EstatusId,
                Registro = x.FechaCreo.ToString(),
                Entrega = x.FechaCreo.AddDays((double)x.Dias).ToString(),
                Seleccion = false,
                Clave = x.Clave,
            }).ToList();
        }
    }
}
