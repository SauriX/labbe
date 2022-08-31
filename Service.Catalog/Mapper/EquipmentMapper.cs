using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.Equipment;
using Service.Catalog.Dtos.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Catalog.Mapper
{
    public static class EquipmentMapper
    {
        public static EquipmentListDto ToEquipmentListDto(this Equipos model)
        {
            if (model == null) return null;

            return new EquipmentListDto
            {
                Id = model.Id,
                Nombre = model.Nombre,
                Clave = model.Clave,
                Activo = model.Activo
                // falta numero de serie
            };
        }

        public static IEnumerable<EquipmentListDto> ToEquipmentListDto(this IEnumerable<Equipos> model)
        {
            if (model == null) return null;

            return model.Select(x => new EquipmentListDto
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Clave = x.Clave,
                Activo = x.Activo,
                Categoria = x.Categoria,
                CategoriaText = x.Categoria == "1" ? "Análisis" : x.Categoria == "2" ? "Cómputo" : "Impresión",
                NumerosSeries = string.Join(", ", x.Valores.Select(x => x.Num_Serie)),
                Valores = x.Valores.Select(x => new EquipmentBranch
                {
                    BranchId = x.BranchId,
                    EquipmentId = x.EquipmentId,
                    Num_Serie = x.Num_Serie,
                    FechaCreo = DateTime.Now

                }).ToList()
            });
        }

        public static EquipmentFormDto ToEquipmentFormDto(this Equipos model)
        {
            if (model == null) return null;

                return new EquipmentFormDto
                {
                    Id = model.Id,
                    Clave = model.Clave.Trim(),
                    Nombre = model.Nombre.Trim(),
                    Activo = model.Activo,
                    Categoria = model.Categoria.Trim(),
                    valores = model.Valores.ToEquipmentBranchDto(),
                    CategoriaText = model.Categoria == "1" ? "Análisis" : model.Categoria == "2" ? "Cómputo" : "Impresión",
                };
        }

        private static IEnumerable<EquipmentBrancheDto> ToEquipmentBranchDto(this IEnumerable<EquipmentBranch> model)
        {
            if (model == null) return null;

            return model.Select(x => new EquipmentBrancheDto
            {
                BranchId = x.BranchId,
                EquipmentId = x.EquipmentId,
                Num_serie = x.Num_Serie,
                SucursalText = x.Sucursal.Nombre

            });
        }

        public static Equipos ToModel(this EquipmentFormDto dto)
        {
            if (dto == null) return null;

            return new Equipos
            {
                Clave = dto.Clave.Trim(),
                Nombre = dto.Nombre.Trim(),
                Activo = dto.Activo,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now,
                FechaModifico = DateTime.Now,
                Categoria = dto.Categoria.Trim(),
                Valores = dto.valores.Select(x => new EquipmentBranch
                {
                    BranchId = x.BranchId,
                    EquipmentId = x.EquipmentId,
                    Num_Serie = x.Num_serie,
                    FechaCreo = DateTime.Now


                }).ToList()

            };
        }

        public static Equipos ToModel(this EquipmentFormDto dto, Equipos model)
        {
            if (dto == null || model == null) return null;

            return new Equipos
            {
                Id = model.Id,
                Clave = dto.Clave.Trim(),
                Nombre = dto.Nombre.Trim(),
                Activo = dto.Activo,
                Categoria = dto.Categoria.Trim(),
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModificoId = dto.UsuarioId,
                FechaModifico = DateTime.Now,
                Valores = dto.valores.Select(x => new EquipmentBranch
                {
                    BranchId = x.BranchId,
                    EquipmentId = dto.Id,
                    UsuarioCreoId = model.UsuarioCreoId,
                    FechaCreo = DateTime.Now,
                    Num_Serie = x.Num_serie,

                }).ToList(),

                //falta numero de serie
            };
        }
    }
}
