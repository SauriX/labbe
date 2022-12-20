﻿using Service.MedicalRecord.Domain.Request;
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
            return model.Select(x => new ValidationStudyDto
            {
                Id = x.EstudioId,
               Study = $"{x.Clave}-{x.Nombre}",
                Area = "",
                Status = x.Estatus.Nombre,
                Estatus= x.EstatusId,
                Registro = x.FechaModifico.ToString(),
                Entrega = x.FechaCreo.AddDays((double)x.Dias).ToString(),
                NombreEstatus = x.Estatus.Nombre,
                SolicitudId = x.SolicitudId,
                Nombre = x.Nombre,
                Clave = x.Clave.ToString(),
                Tipo = urgencia == 1 ? true : false,

            }).ToList();
        }
    }
}
