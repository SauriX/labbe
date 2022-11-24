using Service.MedicalRecord.Dtos.MassSearch;
using System.Collections.Generic;
using System.Linq;

using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dictionary;

namespace Service.MedicalRecord.Mapper

{
    public static class MassSearchMapper
    {
        public static MassSearchInfoDto ToMassSearchInfoDto(this List<Request> model)
        {
            List<int?> filterAreas = new List<int?>() { 5, 34, 23, 44, 28, 17, 41, 9, 20 };

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
        public static List<RequestsInfoDto> ToDeliverResultInfoDto(this List<Request> model)
        {
            if(model == null) return new List<RequestsInfoDto>();

            return model.Select(x => new RequestsInfoDto
            {
                SolicitudId = x.Id,
                ExpedienteId= x.ExpedienteId,
                Solicitud = x.Clave,
                ClavePatologica = x.ClavePatologica,
                Nombre = x.Expediente.NombreCompleto,
                Registro = x.FechaCreo.ToString("dd/MM/yyyy"),
                Sucursal = x.Sucursal?.Nombre,
                Edad = x.Expediente.Edad,
                Sexo = x.Expediente.Genero == "F" ? "Femenino" : "Masculino",
                Compania = x.Compañia?.Nombre,
                Estudios = x.Estudios
                .Where(y => y.EstatusId == Status.RequestStudy.Liberado || y.EstatusId == Status.RequestStudy.Enviado || y.EstatusId == Status.RequestStudy.Entregado)
                .Select(y => new RequestsStudiesInfoDto {
                    EstudioId = y.Id,
                    Estudio = y.Nombre,
                    isPathological = y.DepartamentoId == Shared.Dictionary.Catalogs.Department.PATOLOGIA,
                    MedioSolicitado = !string.IsNullOrEmpty(y.MedioSolicitado)
                                      ? string.Join("/", y.MedioSolicitado.Split(","))
                                      : !string.IsNullOrEmpty(x.EnvioCorreo) && !string.IsNullOrEmpty(x.EnvioWhatsApp)
                                      ? $"Disponibles: Whatsapp/Correo"
                                      : !string.IsNullOrEmpty(x.EnvioCorreo)
                                      ? $"Disponible: Correo"
                                      : !string.IsNullOrEmpty(x.EnvioWhatsApp)
                                      ? $"Disponible: Whatsapp"
                                      : "No disponible",
                    FechaEntrega = y.FechaEntrega.ToString("dd/MM/yyyy"),
                    Estatus = y.Estatus.Nombre,
                    Registro = y.EstatusId == 6 
                            ? $"{y.UsuarioLiberado} - {y.FechaLiberado?.ToString("dd/MM/yyyy")}" 
                            : y.EstatusId == 7 
                            ? $"{y.UsuarioEnviado} - {y.FechaEnviado?.ToString("dd/MM/yyyy")}" 
                            : ""
                }).ToList(),
            }).ToList();
        }
    }
    
}
