using Service.Catalog.Domain.Catalog;
using Service.Catalog.Dtos.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Mapper
{
    public static class CatalogDescriptionMapper
    {
        public static CatalogDescriptionListDto ToCatalogDescriptionListDto<T>(this T model) where T : GenericCatalogDescription
        {
            if (model == null) return null;

            return new CatalogDescriptionListDto
            {
                Id = model.Id,
                Clave = model.Clave,
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                Activo = model.Activo,
            };
        }

        public static IEnumerable<CatalogDescriptionListDto> ToCatalogDescriptionListDto<T>(this List<T> model) where T : GenericCatalogDescription
        {
            if (model == null) return null;

            return model.Select(x => new CatalogDescriptionListDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Nombre = x.Nombre,
                Descripcion = x.Descripcion,
                Activo = x.Activo,
            });
        }

        public static CatalogDescriptionFormDto ToCatalogDescriptionFormDto<T>(this T model) where T : GenericCatalogDescription
        {
            if (model == null) return null;

            return new CatalogDescriptionFormDto
            {
                Id = model.Id,
                Clave = model.Clave,
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                Activo = model.Activo,
            };
        }

        public static T ToModel<T>(this CatalogDescriptionFormDto dto) where T : GenericCatalogDescription, new()
        {
            if (dto == null) return null;

            return new T
            {
                Id = 0,
                Clave = dto.Clave.Trim(),
                Nombre = dto.Nombre.Trim(),
                Descripcion = dto.Descripcion?.Trim(),
                Activo = dto.Activo,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now,
            };
        }

        public static T ToModel<T>(this CatalogDescriptionFormDto dto, T model) where T : GenericCatalogDescription, new()
        {
            if (dto == null || model == null) return null;

            return new T
            {
                Id = model.Id,
                Clave = dto.Clave.Trim(),
                Nombre = dto.Nombre.Trim(),
                Descripcion = dto.Descripcion.Trim(),
                Activo = dto.Activo,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModificoId = dto.UsuarioId,
                FechaModifico = DateTime.Now,
            };
        }
    }
}