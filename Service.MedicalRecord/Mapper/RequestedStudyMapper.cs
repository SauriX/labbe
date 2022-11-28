﻿using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.Sampling;
using System.Collections.Generic;
using System.Linq;

namespace Service.MedicalRecord.Mapper
{
    public static class RequestedStudyMapper
    {
        public static List<SamplingListDto> ToRequestedStudyDto(this List<Request> model)
        {
            if (model == null) return null;

            return model.Select(x => new SamplingListDto
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
