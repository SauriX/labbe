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
            var Suma = model.Estudios.Select(y => y.Estudio).ToList().ToStudyListDto();
            return new IndicationListDto
            {
                Id = model.Id,
                Clave = model.Clave.Trim(),
                Nombre = model.Nombre.Trim(),
                Descripcion = model.Descripcion.Trim(),
                Estudios = (IEnumerable<Dtos.Catalog.CatalogListDto>)Suma,
                Activo = model.Activo

            };
        }
        public static IEnumerable<IndicationListDto> ToIndicationListDto(this List<Indication> model)
        {
            if (model == null) return null;
            return model.Select(x => new IndicationListDto
            {
                Id = x.Id,
                Clave = x.Clave?.Trim(),
                Nombre = x.Nombre?.Trim(),
                Descripcion = x.Descripcion.Trim(),
                Activo = x.Activo,
                Estudios = (IEnumerable<Dtos.Catalog.CatalogListDto>)(x.Estudios?.Select(y => y.Estudio)?.ToList()?.ToStudyListDto())
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
                Activo = model.Activo
            };
        }

        public static Indication ToModel(this IndicationFormDto dto)
        {
            if (dto == null) return null;

            return new Indication
            {
                Clave = dto.Clave.Trim(),
                Nombre = dto.Nombre.Trim(),
                Descripcion = dto.Descripcion.Trim(),
                Activo = dto.Activo,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now,
                Estudios = dto.Estudios.Select(x => new IndicationStudy
                {
                    EstudioId = x.Id,
                    FechaCreo = DateTime.Now,
                    UsuarioCreoId = dto.UsuarioId,
                    FechaMod = DateTime.Now,
                }).ToList(),
            };
        }

        public static Indication ToModel(this IndicationFormDto dto, Indication model)
        {
            if (model == null) return null;

            return new Indication
            {
                Id = dto.Id,
                Clave = model.Clave,
                Nombre = dto.Nombre.Trim(),
                Descripcion = dto.Descripcion.Trim(),
                Activo = dto.Activo,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModificoId = dto.UsuarioId,
                FechaModifico = DateTime.Now,
                Estudios = dto.Estudios.Select(x => new IndicationStudy
                {
                    IndicacionId = model.Id,
                    EstudioId = x.Id,
                    FechaCreo = model.FechaCreo,
                    UsuarioCreoId = model.UsuarioCreoId,
                    FechaMod = DateTime.Now,
                }).ToList(),
            };
        }
    }
}
