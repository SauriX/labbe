using Service.Catalog.Domain.Catalog;
using Service.Catalog.Dtos.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Catalog.Mapper
{
    public static class BudgetMapper
    {
        public static BudgetListDto ToBudgetListDto(this Budget model)
        {
            if (model == null) return null;

            return new BudgetListDto
            {
                Id = model.Id,
                Clave = model.Clave,
                Nombre = model.Nombre,
                Activo = model.Activo,
            };
        }

        public static IEnumerable<BudgetListDto> ToBudgetListDto(this List<Budget> model)
        {
            if (model == null) return null;

            return model.Select(x => new BudgetListDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Nombre = x.Nombre,
                CostoFijo = x.CostoFijo,
                Sucursal = x.Sucursal.Nombre,
                Activo = x.Activo,
                FechaAlta = x.FechaCreo
            });
        }

        public static BudgetFormDto ToBudgetFormDto(this Budget model)
        {
            if (model == null) return null;

            return new BudgetFormDto
            {
                Id = model.Id,
                Clave = model.Clave,
                NombreServicio = model.Nombre,
                SucursalId = model.SucursalId,
                Sucursal = model.Sucursal.Nombre,
                CostoFijo = model.CostoFijo,
                Activo = model.Activo,
            };
        }

        public static Budget ToModel(this BudgetFormDto dto)
        {
            if (dto == null) return null;

            return new Budget
            {
                Id = 0,
                Clave = dto.Clave.Trim(),
                Nombre = dto.NombreServicio.Trim(),
                CostoFijo = dto.CostoFijo,
                SucursalId = dto.SucursalId,
                Activo = dto.Activo,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now,
            };
        }

        public static Budget ToModel(this BudgetFormDto dto, Budget model)
        {
            if (dto == null || model == null) return null;

            return new Budget
            {
                Id = model.Id,
                Clave = dto.Clave.Trim(),
                Nombre = dto.NombreServicio.Trim(),
                CostoFijo = dto.CostoFijo,
                SucursalId = dto.SucursalId,
                Activo = dto.Activo,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModificoId = dto.UsuarioId,
                FechaModifico = DateTime.Now,
            };
        }
    }
}
