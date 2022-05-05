using Service.Catalog.Domain.Indication;
using Service.Catalog.Dtos.Indication;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Catalog.Mapper
{
    public static class IndicationMapper
    {
        public static IndicationListDto ToIndicationListDto(this Indication model)
        {
            if (model == null) return null;

            return new IndicationListDto
            {
                Id = model.Id,
                Clave = model.Clave,
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                Activo = model.Activo
            };
        }

        public static IEnumerable<IndicationListDto> ToIndicationListDto(this List<Indication> model)
        {
            if (model == null) return null;

            return model.Select(x => new IndicationListDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Nombre = x.Nombre,
                Descripcion = x.Descripcion,
                Activo = x.Activo
            });
        }

        public static IndicationFormDto ToIndicationFormDto(this Indication model)
        {
            if (model == null) return null;

            return new IndicationFormDto
            {
                Id = model.Id,
                Clave = model.Clave.Trim(),
                Nombre = model.Nombre.Trim(),
                Descripcion = model.Descripcion.Trim(),
                Activo = model.Activo,
                Estudios = model.Estudios.ToIndicationStudyDto(),
            };
        }

        private static IEnumerable<IndicationStudyDto> ToIndicationStudyDto(this IEnumerable<IndicationStudy> model)
        {
            if (model == null) return null;

            return model.Select(x => x.Estudio).Select(x => new IndicationStudyDto
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Area = x.Area.Nombre,
            });
        }

        public static Indication ToModel(this IndicationFormDto dto)
        {
            if (dto == null) return null;

            return new Indication
            {
                Clave = dto.Clave.Trim(),
                Nombre = dto.Nombre.Trim(),
                Descripcion = dto.Descripcion?.Trim(),
                Activo = dto.Activo,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now,
            };
        }

        public static Indication ToModel(this IndicationFormDto dto, Indication model)
        {
            if (dto == null || model == null) return null;

            return new Indication
            {
                Id = model.Id,
                Clave = dto.Clave.Trim(),
                Nombre = dto.Nombre.Trim(),
                Descripcion = dto.Descripcion?.Trim(),
                Activo = dto.Activo,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModificoId = dto.UsuarioId,
                FechaModifico = DateTime.Now,
            };
        }
    }
}
