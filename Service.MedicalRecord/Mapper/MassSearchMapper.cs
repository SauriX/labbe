﻿using Service.MedicalRecord.Dtos.MassSearch;
using System.Collections.Generic;
using System.Linq;

using Service.MedicalRecord.Domain.Request;

namespace Service.MedicalRecord.Mapper

{
    public static class MassSearchMapper
    {
        public static MassSearchInfoDto ToMassSearchInfoDto(this List<Request> model)
        {
            var parameters = model.SelectMany(x => x.Estudios).SelectMany(x => x.Resultados).Distinct().ToList();
            List<MassSearchResult> results = new List<MassSearchResult>() { };
            for (int i = 0; i < model.Count; i++)
            {
                var solicitud = model[i];
                var estudios = solicitud.Estudios.ToList();
                for (int j = 0; j < solicitud.Estudios.Count; j++)
                {
                    results.Add(new MassSearchResult
                    {
                        Id = solicitud.Id,
                        Clave = solicitud.Clave,
                        paciente = solicitud.Expediente.NombreCompleto,
                        edad = solicitud.Expediente.Edad,
                        Genero = solicitud.Expediente.Genero,
                        NombreEstudio = estudios[j].Nombre,
                        ExpedienteId = solicitud.ExpedienteId,
                        Parameters = estudios[j].Resultados.Select(x => new MassSearchParameter
                        {
                            Nombre = x.Nombre,
                            unidades = x.Unidades,
                            Valor = x.Resultado
                        }).ToList(),
                    });
                }
            }
            return new MassSearchInfoDto
                {
                    Parameters = parameters.Select(x => new MassSearchParameter
                    {
                        Nombre = x.Nombre,
                        unidades = x.Unidades,

                    }).ToList(),
                    Results = results
                    
            };
        }
    }
}
