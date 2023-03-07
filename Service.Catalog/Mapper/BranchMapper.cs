using Service.Catalog.Domain.Branch;
using Service.Catalog.Domain.Series;
using Service.Catalog.Dtos.Branch;
using Shared.Utils;
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
                IdSucursal = x.Id.ToString(),
                Codigo = x.Codigo,
                Clave = x.Clave,
                Nombre = x.Nombre,
                Correo = x.Correo,
                Telefono = x.Telefono,
                //ubicacion = $"{x.Calle} {x.NumeroExterior} {x.Ciudad}",
                Ubicacion = x.Calle.Trim() + " " + x.NumeroExterior?.Trim() + ", " + x.Colonia?.Colonia?.Trim() + ", " + x.Colonia.Ciudad.Ciudad.Trim() + ", " + x.Colonia.Ciudad.Estado.Estado.Trim(),
                Clinico = "test",
                Activo = x.Activo,
                CodigoPostal = x.Codigopostal,
                Ciudad = x.Colonia.Ciudad.Ciudad.Trim()
            });
        }
        public static Branch ToModel(this BranchFormDto dto, string key, IEnumerable<Serie> series = null)
        {
            if (dto == null) return null;

            return new Branch
            {
                Id = Guid.NewGuid(),
                Clave = dto.Clave.Trim(),
                Nombre = dto.Nombre.Trim(),
                Activo = dto.Activo,
                Calle = dto.Calle.Trim(),
                ColoniaId = dto.ColoniaId,
                Correo = dto.Correo,
                FechaCreo = DateTime.Now,
                FechaModifico = DateTime.Now,
                NumeroInterior = dto.NumeroInt?.ToString(),
                NumeroExterior = dto.NumeroExt.ToString(),
                Telefono = dto.Telefono,
                UsuarioCreoId = Guid.NewGuid(),
                UsuarioModificoId = Guid.NewGuid(),
                Ciudad = dto.Ciudad,
                Estado = dto.Estado,
                Codigopostal = dto.CodigoPostal,
                SucursalKey = Crypto.EncryptString(dto.SucursalKey ?? "", key),
                Departamentos = dto.Departamentos.Select(x => new BranchDepartment
                {
                    DepartamentoId = x.DepartamentoId,
                    UsuarioCreoId = dto.UsuarioId,
                    FechaCreo = DateTime.Now
                }).ToList(),
                Series = series.Select(x => new Serie
                {
                    Id = x.Id,
                    Activo = x.Activo,
                    ArchivoCer = x.ArchivoCer,
                    ArchivoKey = x.ArchivoKey,
                    CFDI = x.CFDI,
                    Ciudad = x.Ciudad,
                    Clave = x.Clave,
                    Contraseña = x.Contraseña,
                    Descripcion = x.Descripcion,
                    Nombre = x.Nombre,
                    SucursalKey = x.SucursalKey,
                    TipoSerie = x.TipoSerie,
                    FechaCreo = x.FechaCreo,
                    UsuarioCreoId = x.UsuarioCreoId,
                    SucursalId = Guid.Parse(dto.IdSucursal),
                    UsuarioModificoId = dto.UsuarioId,
                    FechaModifico = DateTime.Now,
                }).ToList(),
                Matriz = dto.Matriz
            };
        }

        public static BranchFormDto ToBranchFormDto(this Branch model, string key)
        {
            if (model == null) return null;
            //List<CatalogListDto> permissions = new List<CatalogListDto>();

            return new BranchFormDto
            {
                IdSucursal = model.Id.ToString(),
                Codigo = model.Codigo,
                Activo = model.Activo,
                Calle = model.Calle,
                Clave = model.Clave,
                Ciudad = model.Ciudad,
                ClinicosId = model.Clinicos,
                CodigoPostal = model.Codigopostal,
                ColoniaId = model.ColoniaId,
                Correo = model.Correo,
                Estado = model.Estado,
                FacturaciónId = model.Clinicos,
                Nombre = model.Nombre,
                NumeroExt = model.NumeroExterior,
                NumeroInt = model.NumeroInterior,
                PresupuestosId = model.Clinicos,
                Telefono = model.Telefono,
                Departamentos = model.Departamentos.ToBranchDepartmentDto(),
                Series = model.Series.ToList().ToSeriesListDto(),
                Matriz = model.Matriz,
                Colonia = model.Colonia.Colonia,
                SucursalKey = Crypto.DecryptString(model.SucursalKey ?? "", key),
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

        public static Branch ToModel(this BranchFormDto dto, Branch model, string key, IEnumerable<Serie> series = null)
        {
            if (dto == null) return null;

            return new Branch
            {
                Id = Guid.Parse(dto.IdSucursal),
                Codigo = model.Codigo,
                Clave = dto.Clave,
                Nombre = dto.Nombre.Trim(),
                Activo = dto.Activo,
                Calle = dto.Calle.Trim(),
                ColoniaId = dto.ColoniaId,
                Correo = dto.Correo,
                FechaCreo = model.FechaCreo,
                FechaModifico = model.FechaModifico,
                NumeroInterior = dto.NumeroInt,
                NumeroExterior = dto.NumeroExt,
                Telefono = dto.Telefono,
                UsuarioCreoId = model.UsuarioCreoId,
                UsuarioModificoId = model.UsuarioModificoId,
                Ciudad = dto.Ciudad,
                Estado = dto.Estado,
                Codigopostal = dto.CodigoPostal,
                Matriz = dto.Matriz,
                Clinicos = model.Clinicos,
                SucursalKey = Crypto.EncryptString(dto.SucursalKey ?? "", key),
                Departamentos = dto.Departamentos.Select(x => new BranchDepartment
                {
                    SucursalId = model.Id,
                    DepartamentoId = x.DepartamentoId,
                    UsuarioCreoId = dto.UsuarioId,
                    FechaCreo = DateTime.Now
                }).ToList(),
                Series = series.Select(x => new Serie
                {
                    Id = x.Id,
                    Activo = x.Activo,
                    ArchivoCer = x.ArchivoCer,
                    ArchivoKey = x.ArchivoKey,
                    CFDI = x.CFDI,
                    Ciudad = x.Ciudad,
                    Clave = x.Clave,
                    Contraseña = x.Contraseña,
                    Descripcion = x.Descripcion,
                    Nombre = x.Nombre,
                    SucursalKey = x.SucursalKey,
                    TipoSerie = x.TipoSerie,
                    FechaCreo = x.FechaCreo,
                    UsuarioCreoId = x.UsuarioCreoId,
                    Relacion = dto.Series.Any(y => y.Relacion != true) ? false : x.Relacion,
                    SucursalId = model.Id,
                    UsuarioModificoId = dto.UsuarioId,
                    FechaModifico = DateTime.Now
                }).ToList()
            };
        }
    }
}
