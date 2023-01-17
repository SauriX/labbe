﻿using Service.Catalog.Domain.Catalog;
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
                Sucursal = x.Sucursal.Nombre,
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

        public static IEnumerable<BudgetListDto> ToBudgetSelectInputDto(this List<Budget> model)
        {
            if (model == null) return null;

            return model.Select(x => x.Nombre).Distinct().Select(x => new BudgetListDto
            {
                Nombre = x,
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
                    Ciudad = x.Ciudad,
                    SucursalId = x.SucursalId,
                    Activo = dto.Activo,
                    UsuarioCreoId = dto.UsuarioId,
                    FechaCreo = DateTime.Now
                }).ToList()
            };
        }

        public static List<Budget> ToModelList(this List<BudgetFormDto> dto)
        {
            if (dto == null) return null;

            return dto.Select(x => new Budget
            {
                Clave = x.Clave.Trim(),
                Nombre = x.NombreServicio.Trim(),
                CostoFijo = x.CostoFijo,
                Activo = x.Activo,
                UsuarioCreoId = x.UsuarioId,
                FechaCreo = DateTime.Now,
                Sucursales = x.Sucursales.Select(y => new BudgetBranch
                {
                    Ciudad = y.Ciudad,
                    SucursalId = y.SucursalId,
                    Activo = x.Activo,
                    UsuarioCreoId = x.UsuarioId,
                    FechaCreo = DateTime.Now
                }).ToList()
            }).ToList();
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
                    Ciudad = x.Ciudad,
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
                Ciudad = x.Ciudad,
                CostoFijoId = x.CostoFijoId
            }).ToList();
        }
    }
}
