using Service.Catalog.Domain.Company;
using Service.Catalog.Dtos.Company;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Catalog.Mapper
{
    public static class CompanyMapper
    {
        public static CompanyListDto ToCompanyListDto(this Company model)
        {
            if (model == null) return null;

            return new CompanyListDto
            {
                Id = model.Id,
                Clave = model.Clave.Trim(),
                Contrasena = model.Contrasena.Trim(),
                Procedencia = model.Procedencia.Nombre.Trim(),
                ProcedenciaId = model.ProcedenciaId,
                PrecioListaId = model?.PrecioListaId,
                PrecioLista = model?.PrecioLista.Nombre,
                Activo = model.Activo

            };
        }
        public static IEnumerable<CompanyListDto> ToCompanyListDto(this List<Company> model)
        {
            if (model == null) return null;
            return model.Select(x => new CompanyListDto
            {
                Id = x.Id,
                Clave = x.Clave.Trim(),
                Contrasena = x.Contrasena.Trim(),
                NombreComercial = x.NombreComercial.Trim(),
                Procedencia = x.Procedencia?.Nombre?.Trim(),
                ProcedenciaId = x.ProcedenciaId,
                PrecioListaId = x.PrecioListaId,
                PrecioLista = x.PrecioLista?.Nombre,
                Activo = x.Activo,
                Contacts = x.Contacts?.ToList()?.ToContactListDto()
            });
        }
        public static CompanyFormDto ToCompanyFormDto(this Company model)
        {
            if (model == null) return null;
            return new CompanyFormDto
            {
                Id = model.Id,
                Clave = model.Clave.Trim(),
                Contrasena = model.Contrasena?.Trim(),
                EmailEmpresarial = model?.EmailEmpresarial?.Trim(),
                NombreComercial = model.NombreComercial?.Trim(),
                ProcedenciaId = model.ProcedenciaId,
                Procedencia = model.Procedencia.Nombre,
                PrecioListaId = model?.PrecioListaId,
                PrecioLista = model?.PrecioLista.Nombre,
                PromocionesId = model?.PromocionesId,
                Promociones = model?.Promociones.Nombre,
                RFC = model.RFC?.Trim(),
                CodigoPostal = model.CodigoPostal,
                Estado = model.Estado,
                Ciudad = model.Ciudad,
                RazonSocial = model.RazonSocial?.Trim(),
                MetodoDePagoId = model.MetodoDePagoId,
                FormaDePagoId = model.FormaDePagoId,
                LimiteDeCredito = model.LimiteDeCredito?.Trim(),
                DiasCredito = model.DiasCredito,
                CFDIId = model.CFDIId,
                NumeroDeCuenta = model.NumeroDeCuenta?.Trim(),
                BancoId = model.BancoId,
                Activo = model.Activo,
                Contacts = model.Contacts.ToList().ToContactListDto()
            };
        }

        public static Company ToModel(this CompanyFormDto dto)
        {
            if (dto == null) return null;

            return new Company
            {
                Id = dto.Id,
                Clave = dto.Clave.Trim(),
                Contrasena = dto.Contrasena?.Trim(),
                EmailEmpresarial = dto.EmailEmpresarial?.Trim(),
                NombreComercial = dto.NombreComercial?.Trim(),
                ProcedenciaId = dto.ProcedenciaId,
                PrecioListaId = dto.PrecioListaId,
                PromocionesId = dto.PromocionesId,
                RFC = dto.RFC?.Trim(),
                CodigoPostal = dto.CodigoPostal,
                Estado = dto.Estado,
                Ciudad = dto.Ciudad,
                RazonSocial = dto.RazonSocial?.Trim(),
                MetodoDePagoId = dto.MetodoDePagoId,
                FormaDePagoId = dto.FormaDePagoId,
                LimiteDeCredito = dto.LimiteDeCredito?.Trim(),
                DiasCredito = dto.DiasCredito,
                CFDIId = dto.CFDIId,
                NumeroDeCuenta = dto.NumeroDeCuenta?.Trim(),
                BancoId = dto.BancoId,
                Activo = dto.Activo,
                UsuarioCreoId = dto.UsuarioCreoId,
                FechaCreo = DateTime.Now,
                Contacts = dto.Contacts?.Select(x => new Contact
                {
                    Id = x.Id,
                    CompañiaId = dto.Id,
                    Nombre = x.Nombre.Trim(),
                    Telefono = x.Telefono,
                    Correo = x.Correo,
                    Activo = x.Activo,
                    FechaCreo = DateTime.Now,
                    UsuarioCreoId = dto.UsuarioCreoId,
                })?.ToList(),
            };
        }

        public static Company ToModel(this CompanyFormDto dto, Company model)
        {
            if (model == null) return null;

            return new Company
            {
                Id = dto.Id,
                Clave = dto.Clave.Trim(),
                Contrasena = dto.Contrasena?.Trim(),
                EmailEmpresarial = dto.EmailEmpresarial?.Trim(),
                NombreComercial = dto.NombreComercial?.Trim(),
                ProcedenciaId = dto.ProcedenciaId,
                PrecioListaId = dto?.PrecioListaId,
                PromocionesId = dto?.PromocionesId,
                RFC = dto.RFC?.Trim(),
                CodigoPostal = dto.CodigoPostal,
                Estado = dto.Estado,
                Ciudad = dto.Ciudad,
                RazonSocial = dto.RazonSocial?.Trim(),
                MetodoDePagoId = dto.MetodoDePagoId,
                FormaDePagoId = dto.FormaDePagoId,
                LimiteDeCredito = dto.LimiteDeCredito?.Trim(),
                DiasCredito = dto.DiasCredito,
                CFDIId = dto.CFDIId,
                NumeroDeCuenta = dto.NumeroDeCuenta?.Trim(),
                BancoId = dto.BancoId,
                Activo = dto.Activo,
                UsuarioCreoId = dto.UsuarioCreoId,
                FechaCreo = DateTime.Now,
                Contacts = dto.Contacts.Select(x => new Contact
                {
                    Id = x.Id,
                    CompañiaId = dto.Id,
                    Nombre = x.Nombre.Trim(),
                    Telefono = x.Telefono,
                    Correo = x.Correo,
                    Activo = x.Activo,
                    FechaCreo = DateTime.Now,
                    UsuarioCreoId = dto.UsuarioCreoId,
                }).ToList(),
            };
        }
    }
}
