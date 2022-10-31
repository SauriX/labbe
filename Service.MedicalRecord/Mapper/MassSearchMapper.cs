using Service.MedicalRecord.Dtos.MassSearch;
using System.Collections.Generic;
using System.Linq;

using Service.MedicalRecord.Domain.Request;

namespace Service.MedicalRecord.Mapper

{
    public static class MassSearchMapper
    {
        public static MassSearchInfoDto ToMassSearchInfoDto(this List<Request> model)
        {
            List<int> filterAreas = new List<int>() { 5, 34, 23, 44, 28, 17, 41, 9, 20 };

            var parameters = model.SelectMany(x => x.Estudios)
                .SelectMany(x => x.Resultados)
                .Distinct()
                .Where(x => x.TipoValorId != "9")
                .ToList();

            List<MassSearchResult> results = new List<MassSearchResult>() { };

            for (int i = 0; i < model.Count; i++)
            {
                var solicitud = model[i];
                var estudios = solicitud.Estudios.ToList();
                for (int j = 0; j < solicitud.Estudios.Count; j++)
                {
                    if (filterAreas.Contains(estudios[j].AreaId))
                    {
                        results.Add(new MassSearchResult
                        {
                            Id = solicitud.Id,
                            Clave = solicitud.Clave,
                            paciente = solicitud.Expediente.NombreCompleto,
                            edad = solicitud.Expediente.Edad,
                            Genero = solicitud.Expediente.Genero,
                            NombreEstudio = estudios[j].Clave,
                            ExpedienteId = solicitud.ExpedienteId,
                            Parameters = estudios[j].Resultados.Where(x => x.TipoValorId != "9").Select(x => new MassSearchParameter
                            {

                                Nombre = x.NombreCorto,
                                unidades = x.Unidades,
                                Valor = x.Resultado
                            }).ToList(),
                        });
                    }
                }
            }
            return new MassSearchInfoDto
                {
                    Parameters = parameters.Select(x => new MassSearchParameter
                    {
                        
                        Nombre = x.NombreCorto,
                        unidades = x.Unidades,

                    }).ToList(),
                    Results = results
                    
            };
        }
    }
}
