using Service.MedicalRecord.Dtos;
using System.Collections.Generic;
using System.Linq;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.Sampling;
using Service.MedicalRecord.Dtos.ClinicResults;
using Service.MedicalRecord.Domain;

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

        public static List<ClinicResultsCaptureDto> ToCaptureResults(this ICollection<ClinicResults> model)
        {
            return model.Select(x => new ClinicResultsCaptureDto
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Edad = x.Solicitud.Expediente.Edad,
                ClaveSolicitud = x.Solicitud.Clave,
                TipoValor = x.TipoValorId,
                ValorInicial = x.ValorInicial,
                ValorFinal = x.ValorFinal,
                ParametroId = x.ParametroId.ToString(),
                Resultado = x.Resultado
            }).ToList();
        }
        public static ClinicalResultsPathological ToClinicalResultPathological(this ClinicalResultPathologicalFormDto dto)
        {
            if (dto == null) return null;

            return new ClinicalResultsPathological
            {
                  SolicitudId = dto.SolicitudId,
                  EstudioId = dto.EstudioId,
                  RequestStudyId = dto.RequestStudyId,
                  DescripcionMacroscopica = dto.DescripcionMacroscopica,
                  DescripcionMicroscopica = dto.DescripcionMicroscopica,
                  ImagenPatologica = dto.ImagenPatologica == null ? "" : string.Join(",", dto.ImagenPatologica.Select(x => x.FileName)),
                  Diagnostico = dto.Diagnostico,
                  MuestraRecibida = dto.MuestraRecibida,
                  MedicoId = dto.MedicoId,
            };
        }
        public static ClinicalResultsPathological ToUpdateClinicalResultPathological(this ClinicalResultPathologicalFormDto dto, ClinicalResultsPathological model)
        {
            if (dto == null) return null;

            string[] actualNameFiles = new string[] {};
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

            if(newNameFiles.Length > 0)
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
                fullNamesImages += ","+string.Join(",", dto.ImagenPatologica.Select(x => x.FileName));
            }

            return new ClinicalResultsPathological
            {
                Id = model.Id,
                SolicitudId = dto.SolicitudId,
                EstudioId = dto.EstudioId,
                RequestStudyId = dto.RequestStudyId,
                DescripcionMacroscopica = dto.DescripcionMacroscopica,
                DescripcionMicroscopica = dto.DescripcionMicroscopica,
                ImagenPatologica = fullNamesImages,
                Diagnostico = dto.Diagnostico,
                MuestraRecibida = dto.MuestraRecibida,
                MedicoId = dto.MedicoId,
            };
        }
    }
}
