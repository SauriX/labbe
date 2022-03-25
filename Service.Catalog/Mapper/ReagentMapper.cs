using Service.Catalog.Domain.Reagent;
using Service.Catalog.Dtos.Reagent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Mapper
{
    public static class ReagentMapper
    {
        public static ReagentListDto ToReagentListDto(this Reagent model)
        {
            if (model == null) return null;

            return new ReagentListDto
            {
                Id = model.Id,
                Clave = model.Clave,
                Nombre = model.Nombre,
                ClaveSistema = model.ClaveSistema,
                NombreSistema = model.NombreSistema,
                Activo = model.Activo,
            };
        }

        public static IEnumerable<ReagentListDto> ToReagentListDto(this List<Reagent> model)
        {
            if (model == null) return null;

            return model.Select(x => new ReagentListDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Nombre = x.Nombre,
                ClaveSistema = x.ClaveSistema,
                NombreSistema = x.NombreSistema,
                Activo = x.Activo,
            });
        }

        public static ReagentFormDto ToReagentFormDto(this Reagent model)
        {
            if (model == null) return null;

            return new ReagentFormDto
            {
                Id = model.Id,
                Clave = model.Clave,
                Nombre = model.Nombre,
                ClaveSistema = model.ClaveSistema,
                NombreSistema = model.NombreSistema,
                Activo = model.Activo,
            };
        }

        public static Reagent ToModel(this ReagentFormDto dto)
        {
            if (dto == null) return null;

            return new Reagent
            {
                Id = 0,
                Clave = dto.Clave.Trim(),
                Nombre = dto.Nombre.Trim(),
                ClaveSistema = dto.ClaveSistema.Trim(),
                NombreSistema = dto.NombreSistema.Trim(),
                Activo = dto.Activo,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now,
            };
        }

        public static Reagent ToModel(this ReagentFormDto dto, Reagent model)
        {
            if (dto == null || model == null) return null;

            return new Reagent
            {
                Id = model.Id,
                Clave = dto.Clave.Trim(),
                Nombre = dto.Nombre.Trim(),
                ClaveSistema = dto.ClaveSistema.Trim(),
                NombreSistema = dto.NombreSistema.Trim(),
                Activo = dto.Activo,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModificoId = dto.UsuarioId,
                FechaModifico = DateTime.Now,
            };
        }
    }
}
