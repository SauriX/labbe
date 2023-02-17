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
                FechaAlta = model.FechaCreo
            };
        }

        public static IEnumerable<BudgetListDto> ToBranchBudgetListDto(this List<BudgetBranch> model)
        {
            if (model == null) return null;

            return model.Select(x =>
            {
                Budget costoFijo = x.CostoFijo;
                return new BudgetListDto
                {
                    Id = x.CostoFijoId,
                    Clave = costoFijo.Clave,
                    Nombre = costoFijo.Nombre,
                    Activo = costoFijo.Activo,
                    Sucursal = x.Sucursal.Nombre,
                    FechaAlta = costoFijo.FechaCreo,
                    CostoFijo = x.CostoServicio
                };
            });
        }

        public static IEnumerable<ServiceUpdateDto> ToBudgetByBranchDto(this List<BudgetBranch> model)
        {
            if (model == null) return null;

            var budgets = model.GroupBy(x => new { x.CostoFijoId, x.CostoFijo.Nombre, x.CostoServicio, x.FechaAlta.Month }).Select(x => new ServiceUpdateDto
            {
                CostoFijoId = x.Key.CostoFijoId,
                Identificador = Guid.NewGuid(),
                Nombre = x.Key.Nombre,
                CostoFijo = x.Key.CostoServicio,
                TotalSucursales = x.Key.CostoServicio * x.Count(),
                FechaAlta = x.FirstOrDefault().FechaAlta,
                Sucursales = x.Select(x => new CityBranchServiceDto
                {
                    CostoId = x.Id,
                    Ciudad = x.Ciudad,
                    SucursalId = x.SucursalId
                }).ToList()
            });

            return budgets;
        }

        public static IEnumerable<BudgetListDto> ToBudgetListDto(this List<Budget> model)
        {
            if (model == null) return null;

            return model.Select(x => new BudgetListDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Nombre = x.Nombre,
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
                Activo = dto.Activo,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now,
                Sucursales = dto.Sucursales.Select(x => new BudgetBranch
                {
                    Id = Guid.NewGuid(),
                    Ciudad = x.Ciudad,
                    SucursalId = x.SucursalId,
                    Activo = dto.Activo,
                    UsuarioCreoId = dto.UsuarioId,
                    FechaCreo = DateTime.Now
                }).ToList()
            };
        }

        public static List<BudgetBranch> ToModelList(this List<BudgetBranchFormDto> dto)
        {
            if (dto == null) return null;

            return dto.Select(x => new BudgetBranch
            {
                CostoFijoId = x.ServicioId,
                CostoServicio = x.CostoFijo,
                Activo = true,
                Ciudad = x.Ciudad,
                SucursalId = x.SucursalId,
                UsuarioCreoId = x.UsuarioId,
                FechaCreo = DateTime.Now
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
                Activo = dto.Activo,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModificoId = dto.UsuarioId,
                FechaModifico = DateTime.Now,
                Sucursales = dto.Sucursales.Select(x => new BudgetBranch
                {
                    Id = x.Id,
                    CostoFijoId = model.Id,
                    Ciudad = x.Ciudad,
                    SucursalId = x.SucursalId,
                    CostoServicio = 0m,
                    Activo = dto.Activo,
                    UsuarioCreoId = (Guid)model.UsuarioCreoId,
                    FechaCreo = (DateTime)model.FechaCreo,
                    UsuarioModificoId = dto.UsuarioId,
                    FechaModifico = DateTime.Now
                }).ToList()
            };
        }

        public static List<BudgetBranch> ToModelBudgetBranch(this IEnumerable<ServiceUpdateDto> model, Guid userId, IEnumerable<BudgetBranch> services)
        {
            if (model == null) return null;

            List<BudgetBranch> budgetBranches = new();

            foreach (var budget in model)
            {
                for (int i = 0; i < budget.Sucursales.Count; i++)
                {
                    CityBranchServiceDto branch = budget.Sucursales[i];
                    var service = services.FirstOrDefault(x => x.Id == branch.CostoId);

                    budgetBranches.Add(new BudgetBranch
                    {
                        Id = branch.CostoId != Guid.Empty ? branch.CostoId : Guid.NewGuid(),
                        CostoFijoId = budget.CostoFijoId,
                        SucursalId = branch.SucursalId,
                        CostoServicio = budget.CostoFijo,
                        Ciudad = branch.Ciudad,
                        Activo = true,
                        FechaAlta = budget.FechaAlta,
                        FechaCreo = service != null ? service.FechaCreo : DateTime.Now,
                        UsuarioCreoId = service != null ? service.UsuarioCreoId : userId,
                        FechaModifico = service == null ? null : DateTime.Now,
                        UsuarioModificoId = service == null ? null : userId,
                    });
                }
            }


            return budgetBranches;
        }

        public static List<CityBranchServiceDto> ToCityBranchServiceDto(this IEnumerable<BudgetBranch> model)
        {
            if (model == null) return null;

            return model.Select(x => new CityBranchServiceDto
            {
                Ciudad = x.Ciudad,
                SucursalId = x.SucursalId
            }).ToList();
        }

        public static List<BudgetBranchListDto> ToBudgetBranchListDto(this IEnumerable<BudgetBranch> model)
        {
            return model.Select(x => new BudgetBranchListDto
            {
                Id = x.Id,
                SucursalId = x.SucursalId,
                Ciudad = x.Ciudad,
                CostoFijoId = x.CostoFijoId
            }).ToList();
        }
    }
}
