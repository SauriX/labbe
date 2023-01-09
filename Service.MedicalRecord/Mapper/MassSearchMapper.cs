using Service.MedicalRecord.Dtos.MassSearch;
using System.Collections.Generic;
using System.Linq;

using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dictionary;

namespace Service.MedicalRecord.Mapper

{
    public static class MassSearchMapper
    {
        private const string ETIQUETA = "9";
        private const byte PARTICULARES = 2;
        public static MassSearchInfoDto ToMassSearchInfoDto(this List<Request> model, MassSearchFilterDto filter)
        {
            List<int?> filterAreas = new List<int?>() { 5, 34, 23, 44, 28, 17, 41, 9, 20 };

            var parameters = model.SelectMany(x => x.Estudios);

            if (filter.Estudios != null && filter.Estudios.Count > 0)
            {
                parameters = parameters.Where(x => filter.Estudios.Contains(x.EstudioId));

            }
            var resultsStudy = parameters
            .SelectMany(x => x.Resultados)
            .Where(x => x.TipoValorId != ETIQUETA)
            .GroupBy(x => x.TipoValorId)
            .Select(x => x.First())
            .ToList();

            List<MassSearchResult> results = new List<MassSearchResult>() { };

            for (int i = 0; i < model.Count; i++)
            {
                var solicitud = model[i];
                var estudios = solicitud.Estudios.ToList();

                if (filter.Estudios != null && filter.Estudios.Count > 0)
                {
                    estudios = estudios.Where(x => filter.Estudios.Contains(x.EstudioId)).ToList();

                }
                    
                for (int j = 0; j < estudios.Count(); j++)
                {
                    if (filterAreas.Contains(estudios[j].AreaId))
                    {
                        results.Add(new MassSearchResult
                        {
                            Id = solicitud.Id,
                            ReuqestStudyId = estudios[j].Id,
                            Clave = solicitud.Clave,
                            Paciente = solicitud.Expediente.NombreCompleto,
                            Edad = solicitud.Expediente.Edad,
                            Genero = solicitud.Expediente.Genero,
                            ExpedienteId = solicitud.ExpedienteId,
                            HoraSolicitud = solicitud.FechaCreo.ToString("HH:mm"),
                            NombreEstudio = estudios[j].Clave,
                            Parameters = estudios[j].Resultados
                            .Where(x => x.TipoValorId != "9")
                            .Select(x => new MassSearchParameter
                            {
                                Id = x.ParametroId,
                                SolicitudEstudioId = x.SolicitudEstudioId,
                                EstudioId = x.EstudioId,
                                Nombre = x.NombreCorto,
                                Clave = x.Clave,
                                Unidades = x.Unidades,
                                Valor = x.Resultado,
                                TipoValorId = x.TipoValorId
                            }).ToList(),
                        });
                    }
                }
            }
            return new MassSearchInfoDto
                {
                    Parameters = resultsStudy.Select(x => new MassSearchParameter
                    {
                        Id= x.ParametroId,
                        TipoValorId = x.TipoValorId,
                        Nombre = x.NombreCorto,
                        Unidades = x.Unidades,
                        Clave = x.Clave

                    }).ToList(),
                    Results = results
                    
            };
        }
        public static List<RequestsInfoDto> ToDeliverResultInfoDto(this List<Request> model)
        {
            if(model == null) return new List<RequestsInfoDto>();

            return model.Where(x => x.Estudios.Count > 0).Select(x => new RequestsInfoDto
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
                Parcialidad = x.Parcialidad ? "Sí" : "No",
                Saldo = x.Saldo,
                SaldoPendiente = x.Procedencia == PARTICULARES && x.Saldo > 0,
                EnvioCorreo = x.EnvioCorreo,
                EnvioWhatsapp = x.EnvioWhatsApp,
                Estudios = x.Estudios
                //.Where(y => y.EstatusId == Status.RequestStudy.Liberado || y.EstatusId == Status.RequestStudy.Enviado || y.EstatusId == Status.RequestStudy.Entregado)
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
                    FechaEntrega = y.FechaEntrega.ToString("dd/MM/yyyy hh:mm"),
                    Estatus = y.Estatus.Nombre,
                    Registro = y.EstatusId == Status.RequestStudy.Liberado 
                            ? $"{y.UsuarioLiberado} {y.FechaLiberado?.ToString("dd/MM/yyyy hh:mm")}" 
                            : y.EstatusId == Status.RequestStudy.Enviado 
                            ? $"{y.UsuarioEnviado} {y.FechaEnviado?.ToString("dd/MM/yyyy hh:mm")}" 
                            : "",
                    IsActiveCheckbox = y.EstatusId == Status.RequestStudy.Liberado || y.EstatusId == Status.RequestStudy.Enviado || y.EstatusId == Status.RequestStudy.Entregado
                }).ToList(),
            }).ToList();
        }
    }
    
}
