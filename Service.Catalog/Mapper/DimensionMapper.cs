using Service.Catalog.Domain.Catalog;
using Service.Catalog.Dtos.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Mapper
{
    public static class DimensionMapper
    {
        public static DimensionListDto ToDimensionListDto(this Dimension model)
        {
            if (model == null) return null;

            return new DimensionListDto
            {
                Id = model.Id,
                Clave = model.Clave,
                Largo = model.Largo,
                Ancho = model.Ancho,
                Activo = model.Activo,
            };
        }

        public static IEnumerable<DimensionListDto> ToDimensionListDto(this List<Dimension> model)
        {
            if (model == null) return null;

            return model.Select(x => new DimensionListDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Largo = x.Largo,
                Ancho = x.Ancho,
                Activo = x.Activo,
            });
        }

        public static DimensionFormDto ToDimensionFormDto(this Dimension model)
        {
            if (model == null) return null;

            return new DimensionFormDto
            {
                Id = model.Id,
                Clave = model.Clave,
                Largo = model.Largo,
                Ancho = model.Ancho,
                Activo = model.Activo,
            };
        }

        public static Dimension ToModel(this DimensionFormDto dto)
        {
            if (dto == null) return null;

            return new Dimension
            {
                Id = 0,
                Clave = dto.Clave.Trim(),
                Largo = dto.Largo,
                Ancho = dto.Ancho,
                Activo = dto.Activo,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now,
            };
        }

        public static Dimension ToModel(this DimensionFormDto dto, Dimension model)
        {
            if (dto == null || model == null) return null;

            return new Dimension
            {
                Id = model.Id,
                Clave = dto.Clave.Trim(),
                Largo = model.Largo,
                Ancho = model.Ancho,
                Activo = dto.Activo,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModificoId = dto.UsuarioId,
                FechaModifico = DateTime.Now,
            };
        }
    }
}
