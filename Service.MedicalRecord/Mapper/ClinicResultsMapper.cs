using Service.MedicalRecord.Dtos;
using System.Collections.Generic;
using System.Linq;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.Sampling;
using Service.MedicalRecord.Dtos.ClinicResults;
using Service.MedicalRecord.Domain;
using System;
using Service.MedicalRecord.Dtos.Request;

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
                Status = x.EstatusId,
                Registro = x.FechaCreo.ToString("G"),
                Entrega = x.FechaCreo.AddDays((double)x.Dias).ToString("G"),
                Seleccion = false,
                Clave = x.Clave,
                NombreEstatus = x.Estatus.Nombre,
            }).ToList();
        }

        public static List<ClinicResults> ToCaptureResults(this List<ClinicResultsFormDto> model)
        {
            return model.Select(x => new ClinicResults
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
                Unidades = x.UnidadNombre,
                Formula = x?.Formula,
                NombreCorto = x?.NombreCorto,
                DeltaCheck = x.DeltaCheck,
                UltimoResultado = x?.UltimoResultado

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

        public static ClinicResultsPdfDto ToResults(this IEnumerable<ClinicResults> model, bool ImprimirLogos, bool ImprimirCriticos, bool ImprimirPrevios)
        {
            if (model == null || !model.Any()) return new ClinicResultsPdfDto();

            var results = ResultsGeneric(model);
            var requestInfo = model.First();

            var request = new ClinicResultsRequestDto
            {
                Id = (Guid)(requestInfo.Solicitud.Id),
                Clave = requestInfo.Solicitud.Clave,
                Paciente = requestInfo.Solicitud.Expediente.NombrePaciente,
                Medico = requestInfo.Solicitud.Medico?.Nombre,
                Compañia = requestInfo.Solicitud.Compañia?.Nombre,
                Expediente = requestInfo.Solicitud.Expediente.Expediente,
                Edad = requestInfo.Solicitud.Expediente.Edad,
                Sexo = requestInfo.Solicitud.Expediente.Genero,
                FechaAdmision = requestInfo.Solicitud.FechaCreo.ToString("d"),
                FechaEntrega = requestInfo.Solicitud.Estudios.Max(x => x.FechaEntrega).ToString("d"),
                User = requestInfo.Solicitud.UsuarioCreo
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

        public static List<ClinicResultsFormDto> ResultsGeneric(this IEnumerable<ClinicResults> model)
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
                    DeltaCheck = results.DeltaCheck
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

        public static ClinicResultPathologicalPdfDto toInformationPdf(this ClinicalResultsPathological result, Request request, string Departamento, bool ImprimirLogos)
        {
            return new ClinicResultPathologicalPdfDto
            {
                //Medico = request.Medico.Nombre,
                //FechaEntrega = DateTime.Now.ToString("MM/dd/yyyy"),
                //Paciente = request.Expediente.NombrePaciente,
                //Edad = request.Expediente.Edad,
                //Estudio = request.Clave,
                //Departamento = Departamento,
                //MuestraRecibida = result.MuestraRecibida,
                //DescripcionMacroscopica = result.DescripcionMacroscopica,
                //DescripcionMicroscopica = result.DescripcionMicroscopica,
                //Diagnostico = result.Diagnostico,
                //NombreFirma = result.Medico.Nombre,
                //ImprimrLogos = ImprimirLogos

            };
        }
    }
}
