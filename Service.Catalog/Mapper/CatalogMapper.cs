using Service.Catalog.Domain.Catalog;
using Service.Catalog.Dtos.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Mapper
{
    public static class CatalogMapper
    {
        public static CatalogListDto ToCatalogListDto<T>(this T model) where T : GenericCatalog
        {
            if (model == null) return null;

            return new CatalogListDto
            {
                Id = model.Id,
                Clave = model.Clave,
                Nombre = model.Nombre,
                Activo = model.Activo,
            };
        }

        public static IEnumerable<CatalogListDto> ToCatalogListDto<T>(this List<T> model) where T : GenericCatalog
        {
            if (model == null) return null;

            return model.Select(x => new CatalogListDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Nombre = x.Nombre,
                Activo = x.Activo,
            });
        }

        public static CatalogFormDto ToCatalogFormDto<T>(this T model) where T : GenericCatalog
        {
            if (model == null) return null;

            return new CatalogFormDto
            {
                Id = model.Id,
                Clave = model.Clave,
                Nombre = model.Nombre,
                Activo = model.Activo,
            };
        }

        public static T ToModel<T>(this CatalogFormDto dto) where T : GenericCatalog, new()
        {
            if (dto == null) return null;

            return new T
            {
                Id = 0,
                Clave = dto.Clave,
                Nombre = dto.Nombre,
                Activo = dto.Activo,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now,
            };
        }

        public static T ToModel<T>(this CatalogFormDto dto, T model) where T : GenericCatalog, new()
        {
            if (dto == null || model == null) return null;

            return new T
            {
                Id = model.Id,
                Clave = dto.Clave,
                Nombre = dto.Nombre,
                Activo = dto.Activo,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModificoId = dto.UsuarioId,
                FechaModifico = DateTime.Now,
            };
        }
    }
}
