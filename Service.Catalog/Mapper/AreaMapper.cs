using Service.Catalog.Domain.Catalog;
using Service.Catalog.Dtos.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Catalog.Mapper
{
    public static class AreaMapper
    {
        public static AreaListDto ToAreaListDto(this Area model)
        {
            if (model == null) return null;

            return new AreaListDto
            {
                Id = model.Id,
                Clave = model.Clave,
                Nombre = model.Nombre,
                Departamento = model.Departamento.Nombre,
                Activo = model.Activo,
            };
        }

        public static IEnumerable<AreaListDto> ToAreaListDto(this List<Area> model)
        {
            if (model == null) return null;

            return model.Select(x => new AreaListDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Nombre = x.Nombre,
                Departamento = x.Departamento.Nombre,
                Activo = x.Activo,
            });
        }

        public static AreaFormDto ToAreaFormDto(this Area model)
        {
            if (model == null) return null;

            return new AreaFormDto
            {
                Id = model.Id,
                Clave = model.Clave,
                Nombre = model.Nombre,
                DepartamentoId = model.DepartamentoId,
                Departamento = model.Departamento.Nombre,
                Activo = model.Activo,
            };
        }

        public static Area ToModel(this AreaFormDto dto)
        {
            if (dto == null) return null;

            return new Area
            {
                Id = 0,
                Clave = dto.Clave.Trim(),
                Nombre = dto.Nombre.Trim(),
                DepartamentoId = dto.DepartamentoId,
                Orden = 99,
                Activo = dto.Activo,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now,
            };
        }

        public static Area ToModel(this AreaFormDto dto, Area model)
        {
            if (dto == null || model == null) return null;

            return new Area
            {
                Id = model.Id,
                Clave = dto.Clave.Trim(),
                Nombre = dto.Nombre.Trim(),
                DepartamentoId = dto.DepartamentoId,
                Orden = model.Orden,
                Activo = dto.Activo,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModificoId = dto.UsuarioId,
                FechaModifico = DateTime.Now,
            };
        }
    }
}
