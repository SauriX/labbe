using Service.MedicalRecord.Dtos;
using System.Collections.Generic;
using System.Linq;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.Sampling;

namespace Service.MedicalRecord.Mapper
{
    public static class ClinicResultsMapper
    {
        public static List<ClinicResultsDto> ToClinicResultsDto(this List<Request> model)
        {
            if (model == null) return null;

            return model.Select(x => new ClinicResultsDto
            {
                Solicitud = x.Clave,
                ExpedienteId = x.ExpedienteId,
                Nombre = x.Expediente.NombreCompleto,
                Registro = x.FechaCreo.ToString(),
                Sucursal = x.Sucursal.Clave,
                Edad = x.Expediente.Edad.ToString(),
                Sexo = x.Expediente.Genero,
                Compañia = x.Compañia?.Nombre,
                Estudios = x.Estudios.ToClinics(),
                Id = x.Id.ToString(),
                Procedencia = x.Procedencia,
                SucursalNombre = x.Sucursal.Nombre,
                NombreMedico = x.Medico.Nombre,
            }).ToList();
        }

        public static List<StudyDto> ToClinics(this ICollection<RequestStudy> model)
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
                NombreEstatus = x.Estatus.Nombre,
            }).ToList();
        }
    }
}
