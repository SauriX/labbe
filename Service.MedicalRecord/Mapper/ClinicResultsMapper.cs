using Service.MedicalRecord.Dtos;
using System.Collections.Generic;
using System.Linq;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.Sampling;
using Service.MedicalRecord.Dtos.ClinicResults;
using Service.MedicalRecord.Domain;
using System;
using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Dtos.Catalogs;

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
                Registro = x.FechaCreo.ToString("G"),
                Sucursal = x.Sucursal.Clave,
                Edad = x.Expediente.Edad.ToString(),
                Sexo = x.Expediente.Genero,
                Compañia = x.Compañia?.Nombre,
                Estudios = x.Estudios.ToClinics(),
                Id = x.Id.ToString(),
                Procedencia = x.Procedencia,
                SucursalNombre = x.Sucursal.Nombre,
                NombreMedico = x.Medico.Nombre,
                UsuarioCreo = x.UsuarioCreo,
                ClavePatologica = x.ClavePatologica,
            }).ToList();
        }

        public static List<StudyDto> ToClinics(this ICollection<RequestStudy> model)
        {
            return model.Select(x => new StudyDto
            {
                Id = x.EstudioId,
                Nombre = x.Nombre,
                Area = "",
                Estatus = x.EstatusId,
                Registro = x.FechaCreo.ToString("dd/MM/yyyy HH:mm"),
                Entrega = x.FechaEntrega.ToString("dd/MM/yyyy HH:mm"),
                Seleccion = false,
                Clave = x.Clave,
                NombreEstatus = x.Estatus.Nombre,
                FechaActualizacion = x.EstatusId == Status.RequestStudy.TomaDeMuestra
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
                UsuarioActualizacion = x.EstatusId == Status.RequestStudy.TomaDeMuestra
                    ? x.UsuarioTomaMuestra
                    : x.EstatusId == Status.RequestStudy.Solicitado
                    ? x.UsuarioSolicitado
                    : x.EstatusId == Status.RequestStudy.Capturado
                    ? x.UsuarioCaptura
                    : x.EstatusId == Status.RequestStudy.Validado
                    ? x.UsuarioValidacion
                    : x.EstatusId == Status.RequestStudy.Liberado
                    ? x.UsuarioLiberado
                    : x.EstatusId == Status.RequestStudy.Enviado
                    ? x.UsuarioEnviado
                    : "",
                Urgencia = x.Solicitud.Urgencia,
                SolicitudEstudioId = x.Id
            }).ToList();
        }

        public static List<ClinicResults> ToCaptureResults(this List<ClinicResultsFormDto> model)
        {
            return model.Select((x, i) => new ClinicResults
            {
                Id = x.Id,
                Nombre = x.Nombre,
                SolicitudId = x.SolicitudId,
                EstudioId = x.EstudioId,
                SolicitudEstudioId = x.SolicitudEstudioId,
                TipoValorId = x.TipoValorId,
                ValorInicial = x?.ValorInicial,
                ValorFinal = x?.ValorFinal,
                CriticoMinimo = x.CriticoMinimo,
                CriticoMaximo = x.CriticoMaximo,
                ParametroId = Guid.Parse(x.ParametroId),
                Resultado = x.Resultado,
                ObservacionesId = x.ObservacionesId,
                Unidades = x.UnidadNombre,
                Formula = x?.Formula,
                NombreCorto = x?.NombreCorto,
                DeltaCheck = x.DeltaCheck,
                UltimoResultado = x?.UltimoResultado,
                UltimaSolicitudId = x.UltimaSolicitudId,
                Orden = i,
                Clave = x.Clave,
                FCSI = x.FCSI,
            }).ToList();
        }

        public static ClinicalResultsPathological ToClinicalResultPathological(this ClinicalResultPathologicalFormDto dto)
        {
            if (dto == null) return null;

            return new ClinicalResultsPathological
            {
                SolicitudId = dto.SolicitudId,
                SolicitudEstudioId = dto.EstudioId,
                RequestStudyId = dto.RequestStudyId,
                DescripcionMacroscopica = dto.DescripcionMacroscopica,
                DescripcionMicroscopica = dto.DescripcionMicroscopica,
                ImagenPatologica = dto.ImagenPatologica == null ? "" : string.Join(",", dto.ImagenPatologica.Select(x => x.FileName)),
                Diagnostico = dto.Diagnostico,
                MuestraRecibida = dto.MuestraRecibida,
                MedicoId = dto.MedicoId,
            };
        }
        public static ClinicResultsPathologicalInfoDto ToPathologicalInfoDto(this ClinicalResultsPathological model)
        {
            return new ClinicResultsPathologicalInfoDto
            {
                Id = model.Id,
                SolicitudId = model.SolicitudId,
                //DepartamentoEstudio = model.SolicitudEstudio.DepartamentoId,
                DescripcionMacroscopica = model.DescripcionMacroscopica,
                DescripcionMicroscopica = model.DescripcionMicroscopica,
                Diagnostico = model.Diagnostico,
                MedicoId = model.MedicoId,
                MuestraRecibida = model.MuestraRecibida,
                ImagenPatologica = model.ImagenPatologica,


            };
        }
        public static ClinicalResultsPathological ToUpdateClinicalResultPathological(this ClinicalResultPathologicalFormDto dto, ClinicalResultsPathological model)
        {
            if (dto == null) return null;

            string[] actualNameFiles = new string[] { };
            if (model.ImagenPatologica != null)
            {
                actualNameFiles = model.ImagenPatologica.Split(",");
            }
            string[] newNameFiles = new string[] { };
            if (dto.ListaImagenesCargadas != null)
            {
                newNameFiles = actualNameFiles.Where(name => !dto.ListaImagenesCargadas.Contains(name)).ToArray();

            }
            else
            {
                newNameFiles = actualNameFiles;
            }

            string fullNamesImages = null;

            if (newNameFiles.Length > 0)
            { 
                fullNamesImages = string.Join(",", newNameFiles);
            }
            else
            {
                fullNamesImages = null;
            }
            if (dto.ImagenPatologica == null)
            {
                fullNamesImages += null;
            }
            else
            {
                fullNamesImages += "," + string.Join(",", dto.ImagenPatologica.Select(x => x.FileName));
            }

            return new ClinicalResultsPathological
            {
                Id = model.Id,
                SolicitudId = dto.SolicitudId,
                SolicitudEstudioId = dto.EstudioId,
                RequestStudyId = dto.RequestStudyId,
                DescripcionMacroscopica = dto.DescripcionMacroscopica,
                DescripcionMicroscopica = dto.DescripcionMicroscopica,
                ImagenPatologica = fullNamesImages,
                Diagnostico = dto.Diagnostico,
                MuestraRecibida = dto.MuestraRecibida,
                MedicoId = dto.MedicoId,
            };
        }

        public static ClinicResultsPdfDto ToResults(this IEnumerable<ClinicResults> model, bool ImprimirLogos, bool ImprimirCriticos, bool ImprimirPrevios, List<ParameterValueDto> valoresReferencia = null)
        {
            if (model == null || !model.Any()) return new ClinicResultsPdfDto();

            var results = ResultsGeneric(model, valoresReferencia);
            var requestInfo = model.First();

            var request = new ClinicResultsRequestDto
            {
                Id = (Guid)(requestInfo.Solicitud.Id),
                Clave = requestInfo.Solicitud.Clave,
                Paciente = requestInfo.Solicitud.Expediente.NombreCompleto,
                Medico = requestInfo.Solicitud.Medico?.Nombre,
                Compañia = requestInfo.Solicitud.Compañia?.Nombre,
                Expediente = requestInfo.Solicitud.Expediente.Expediente,
                Edad = requestInfo.Solicitud.Expediente.Edad,
                Sexo = requestInfo.Solicitud.Expediente.Genero,
                FechaAdmision = requestInfo.Solicitud.FechaCreo.ToString("d"),
                FechaEntrega = requestInfo.Solicitud.Estudios.Max(x => x.FechaEntrega).ToString("d"),
                User = requestInfo.Solicitud.UsuarioCreo,
                Metodo = requestInfo.Solicitud.Estudios?.Select(x => x.Metodo).FirstOrDefault(),
            };

            var data = new ClinicResultsPdfDto
            {
                CapturaResultados = results,
                SolicitudInfo = request,
                ImprimrLogos = ImprimirLogos,
                ImprimirCriticos = ImprimirCriticos,
                ImprimirPrevios = ImprimirPrevios
            };

            return data;
        }

        public static List<ClinicResultsFormDto> ResultsGeneric(this IEnumerable<ClinicResults> model, List<ParameterValueDto> valoresReferencia = null)
        {
            return model.Select(results =>
            {
                return new ClinicResultsFormDto
                {
                    Id = results.Id,
                    SolicitudEstudioId = results.SolicitudEstudioId,
                    Nombre = results.Nombre,
                    TipoValorId = results.TipoValorId,
                    ValorInicial = results?.ValorInicial,
                    SolicitudId = results.SolicitudId,
                    EstudioId = results.EstudioId,
                    ValorFinal = results?.ValorFinal,
                    CriticoMinimo = results.CriticoMinimo,
                    CriticoMaximo = results.CriticoMaximo,
                    ParametroId = results.ParametroId.ToString(),
                    Resultado = results.Resultado,
                    UnidadNombre = results.Unidades,
                    Estudio = results.SolicitudEstudio.Nombre,
                    UltimoResultado = results.UltimoResultado,
                    UltimaSolicitudId = results.UltimaSolicitudId,
                    DeltaCheck = results.DeltaCheck,
                    Orden = results.Orden,
                    Clave = results.Clave,
                    FCSI = results.FCSI,
                    ValoresReferencia = valoresReferencia
                };
            }).ToList();
        }

        public static List<ClinicResults> ToUpdateCapture(this IEnumerable<ClinicResultsFormDto> dto, IEnumerable<ClinicResults> model)
        {
            if (dto == null || model == null) return null;

            return dto.Select(x =>
            {
                var result = model.FirstOrDefault(s => s.ParametroId.ToString() == x.ParametroId);

                return new ClinicResults
                {
                    Id = result.Id,
                    SolicitudEstudioId = x.SolicitudEstudioId,
                    Nombre = x.Nombre,
                    TipoValorId = x.TipoValorId,
                    ValorInicial = x?.ValorInicial,
                    SolicitudId = x.SolicitudId,
                    EstudioId = x.EstudioId,
                    ValorFinal = x?.ValorInicial,
                    CriticoMinimo = x.CriticoMinimo,
                    CriticoMaximo = x.CriticoMaximo,
                    ParametroId = Guid.Parse(x.ParametroId),
                    Resultado = x.Resultado,
                    ObservacionesId = x.ObservacionesId,
                    Unidades = x.UnidadNombre,
                    Formula = x?.Formula,
                    NombreCorto = x?.NombreCorto
                };
            }).ToList();
        }

        public static ClinicResultPathologicalPdfDto toInformationPdfResult(this List<ClinicalResultsPathological> result, bool ImprimirLogos)
        {
            return new ClinicResultPathologicalPdfDto
            {
                Information = result.Select(res => new Information
                {
                    Medico = res.Medico.Nombre,
                    FechaEntrega = DateTime.Now.ToString("MM/dd/yyyy"),
                    Paciente = res.Solicitud.Expediente.NombrePaciente,
                    Edad = res.Solicitud.Expediente.Edad,
                    Estudio = res.SolicitudEstudio.Clave,
                    Departamento = res.SolicitudEstudio.DepartamentoId.ToString(),
                    MuestraRecibida = res.MuestraRecibida,
                    DescripcionMacroscopica = res.DescripcionMacroscopica,
                    DescripcionMicroscopica = res.DescripcionMicroscopica,
                    Diagnostico = res.Diagnostico,
                    NombreFirma = res.Medico.Nombre,
                }).ToList(),
                ImprimrLogos = ImprimirLogos

            };
        }
    }
}
