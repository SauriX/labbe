using Service.Catalog.Domain.Catalog;
using Service.Catalog.Dtos.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Mapper
{
    public static class CatalogDescMapper
    {
        public static CatalogDescListDto ToCatalogDescListDto<T>(this T model) where T : GenericCatalogDescription
        {
            if (model == null) return null;

            return new CatalogDescListDto
            {
                Id = model.Id,
                Clave = model.Clave,
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                Activo = model.Activo,
            };
        }

        public static IEnumerable<CatalogDescListDto> ToCatalogDescListDto<T>(this List<T> model) where T : GenericCatalogDescription
        {
            if (model == null) return null;

            return model.Select(x => new CatalogDescListDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Nombre = x.Nombre,
                Descripcion = x.Descripcion,
                Activo = x.Activo,
            });
        }

        public static CatalogDescFormDto ToCatalogFormDto<T>(this T model) where T : GenericCatalogDescription
        {
            if (model == null) return null;

            return new CatalogDescFormDto
            {
                Id = model.Id,
                Clave = model.Clave,
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                Activo = model.Activo,
            };
        }

        public static T ToModel<T>(this CatalogDescFormDto dto) where T : GenericCatalogDescription, new()
        {
            if (dto == null) return null;

            return new T
            {
                Id = 0,
                Clave = dto.Clave,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Activo = dto.Activo,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now,
            };
        }

        public static T ToModel<T>(this CatalogDescFormDto dto, T model) where T : GenericCatalogDescription, new()
        {
            if (dto == null || model == null) return null;

            return new T
            {
                Id = model.Id,
                Clave = dto.Clave,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Activo = dto.Activo,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModificoId = dto.UsuarioId,
                FechaModifico = DateTime.Now,
            };
        }
    }
}