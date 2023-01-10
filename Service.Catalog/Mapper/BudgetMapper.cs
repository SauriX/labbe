using Service.Catalog.Domain.Catalog;
using Service.Catalog.Dtos.Branch;
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
        
        public static IEnumerable<BudgetListDto> ToBranchBudgetListDto(this List<BudgetBranch> model)
        {
            if (model == null) return null;

            return model.Select(x => new BudgetListDto
            {
                Id = x.CostoFijoId,
                Clave = x.CostoFijo.Clave,
                Nombre = x.CostoFijo.Nombre,
                CostoFijo = x.CostoFijo.CostoFijo,
                Activo = x.CostoFijo.Activo,
                FechaAlta = x.CostoFijo.FechaCreo
            });
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
                CostoFijo = model.CostoFijo,
                Activo = model.Activo,
                Fecha = (DateTime)model.FechaCreo,
                Sucursales = model.Sucursales.ToBudgetBranchListDto()
            };
        }

        public static Budget ToModel(this BudgetFormDto dto)
        {
            if (dto == null) return null;

            return new Budget
            {
                Clave = dto.Clave.Trim(),
                Nombre = dto.NombreServicio.Trim(),
                CostoFijo = dto.CostoFijo,
                Activo = dto.Activo,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now,
                Sucursales = dto.Sucursales.Select(x => new BudgetBranch
                {
                    Ciudad = dto.Ciudad,
                    SucursalId = x.SucursalId,
                    Activo = dto.Activo,
                    UsuarioCreoId = dto.UsuarioId,
                    FechaCreo = DateTime.Now
                }).ToList()
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
                Activo = dto.Activo,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModificoId = dto.UsuarioId,
                FechaModifico = DateTime.Now,
                Sucursales = dto.Sucursales.Select(x => new BudgetBranch
                {
                    CostoFijoId = model.Id,
                    Ciudad = dto.Ciudad,
                    SucursalId = x.SucursalId,
                    Activo = dto.Activo,
                    UsuarioCreoId = (Guid)model.UsuarioCreoId,
                    FechaCreo = (DateTime)model.FechaCreo,
                    UsuarioModificoId = dto.UsuarioId,
                    FechaModifico = DateTime.Now
                }).ToList()
            };
        }

        public static List<BudgetBranchListDto> ToBudgetBranchListDto(this IEnumerable<BudgetBranch> model)
        {
            return model.Select(x => new BudgetBranchListDto
            {
                SucursalId = x.SucursalId,
                Ciudad = x.Sucursal.Ciudad,
                CostoFijoId = x.CostoFijoId
            }).ToList();
        }
    }
}
