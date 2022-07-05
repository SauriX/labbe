using Service.Catalog.Domain;
using Service.Catalog.Domain.Branch;
using Service.Catalog.Dtos.Branch;
using Service.Catalog.Dtos.Catalog;
using Service.Catalog.Dtos.Study;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Catalog.Mapper
{
    public static class BranchMapper
    {
        public static IEnumerable<BranchInfoDto> ToBranchListDto(this List<Branch> model)
        {
            if (model == null) return null;
            return model.Select(x => new BranchInfoDto
            {
                idSucursal = x.Id.ToString(),
                clave = x.Clave,
                nombre = x.Nombre,
                correo = x.Correo,
                telefono = x.Telefono,
                //ubicacion = $"{x.Calle} {x.NumeroExterior} {x.Ciudad}",
                ubicacion = x.Calle.Trim() + " " + x.NumeroExterior.Trim() + ", " + x.Colonia?.Colonia?.Trim() + ", " + x.Colonia.Ciudad.Ciudad.Trim() + ", " + x.Colonia.Ciudad.Estado.Estado.Trim(),
                clinico = "test",
                activo = x.Activo,
                codigoPostal = x.Codigopostal
            });
        }
        public static Branch ToModel(this BranchFormDto dto)
        {
            if (dto == null) return null;

            return new Branch
            {
                Clave = dto.clave.Trim(),
                Nombre = dto.nombre.Trim(),
                Activo = dto.activo,
                Calle = dto.calle.Trim(),
              
                ColoniaId = dto.coloniaId,
                Correo = dto.correo,
                FechaCreo = DateTime.Now,
                FacturaciónId = Guid.NewGuid(),
                FechaModifico = DateTime.Now,
                Id = Guid.NewGuid(),
                NumeroInterior = dto.numeroInt?.ToString(),
                NumeroExterior = dto.numeroExt.ToString(),
                PresupuestosId = Guid.NewGuid(),
                Telefono = dto.telefono,
                UsuarioCreoId = Guid.NewGuid(),
                UsuarioModificoId = Guid.NewGuid(),
                Ciudad = dto.ciudad,
                Estado = dto.estado,
                Codigopostal = dto.codigoPostal,
                Departamentos = dto.departamentos.Select(x => new BranchDepartment
                {
                    DepartamentoId = x.DepartamentoId,
                    UsuarioCreoId = dto.UsuarioId,
                    FechaCreo = DateTime.Now
                }).ToList(),
                Matriz = dto.Matriz
            };
        }

        public static BranchFormDto ToBranchFormDto(this Branch model)
        {
            if (model == null) return null;
            //List<CatalogListDto> permissions = new List<CatalogListDto>();

            return new BranchFormDto
            {
                idSucursal = model.Id.ToString(),
                activo = model.Activo,
                calle = model.Calle,
                clave = model.Clave,
                ciudad = model.Ciudad,
                clinicosId = model.Clinicos ,
                codigoPostal = model.Codigopostal,
                coloniaId = model.ColoniaId,
                correo = model.Correo,
                estado = model.Estado,
                facturaciónId = model.Clinicos,
                nombre = model.Nombre,
                numeroExt = model.NumeroExterior,
                numeroInt = model.NumeroInterior,
                presupuestosId = model.Clinicos,
                telefono = model.Telefono,
                departamentos = model.Departamentos.ToBranchDepartmentDto(),
                Matriz = model.Matriz,
                colonia = model.Colonia.Colonia
               
            };
        }

        private static IEnumerable<BranchDepartmentDto> ToBranchDepartmentDto(this IEnumerable<BranchDepartment> model)
        {
            if (model == null) return null;

            return model.Select(x => new BranchDepartmentDto
            {
                SucursalId = x.SucursalId.ToString(),
                DepartamentoId = x.DepartamentoId,
                Departamento = x.Departamento.Nombre,
            });
        }

        private static IEnumerable<T> IEnumerable<T>()
        {
            throw new NotImplementedException();
        }

        public static Branch ToModel(this BranchFormDto dto, Branch model)
        {
            if (dto == null) return null;

            return new Branch
            {
                Clave = dto.clave,
                Nombre = dto.nombre.Trim(),
                Activo = dto.activo,
                Calle = dto.calle.Trim(),
                
                ColoniaId = dto.coloniaId,
                Correo = dto.correo,
                FechaCreo = model.FechaCreo,
                FacturaciónId = model.FacturaciónId,
                FechaModifico = model.FechaModifico,
                Id = Guid.Parse(dto.idSucursal),
                NumeroInterior = dto.numeroInt,
                NumeroExterior = dto.numeroExt,
                PresupuestosId = model.PresupuestosId,
                Telefono = dto.telefono,
                UsuarioCreoId = model.UsuarioCreoId,
                UsuarioModificoId = model.UsuarioModificoId,
                Ciudad = dto.ciudad,
                Estado = dto.estado,
                Codigopostal = dto.codigoPostal,
                Matriz = dto.Matriz,
                Departamentos = dto.departamentos.Select(x => new BranchDepartment
                {
                    SucursalId = model.Id,
                    DepartamentoId = x.DepartamentoId,
                    UsuarioCreoId = dto.UsuarioId,
                    FechaCreo = DateTime.Now
                }).ToList(),
            };
        }
    }
}
