﻿using Service.Catalog.Domain.Company;
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
                IdCompania = model.Id,
                Clave = model.Clave.Trim(),
                Contrasena = model.Contrasena.Trim(),
                Procedencia = model.Procedencia,
                ListaPrecioId = model.ListaPrecioId,
                Activo = model.Activo

            };
        }
        public static IEnumerable<CompanyListDto> ToCompanyListDto(this List<Company> model)
        {
            if (model == null) return null;
            return model.Select(x => new CompanyListDto
            {
                IdCompania = x.Id,
                Clave = x.Clave.Trim(),
                Contrasena = x.Contrasena.Trim(),
                NombreComercial = x.NombreComercial.Trim(),
                Procedencia = x.Procedencia,
                ListaPrecioId = x.ListaPrecioId,
                Activo = x.Activo,
                //Clinicas = x.Clinicas?.Select(y => y.Clinica)?.ToList()?.ToCatalogListDto()
                Contacts = x.Contacts.ToList().ToContactListDto()
            });
        }
        public static CompanyFormDto ToCompanyFormDto(this Company model)
        {
            if (model == null) return null;
            return new CompanyFormDto
            {
                IdCompania = model.Id,
                Clave = model.Clave.Trim(),
                Contrasena = model.Contrasena.Trim(),
                EmailEmpresarial = model.EmailEmpresarial.Trim(),
                NombreComercial = model.NombreComercial.Trim(),
                Procedencia = model.Procedencia,
                ListaPrecioId = model.ListaPrecioId,
                PromocionesId = model.PromocionesId,
                RFC = model.RFC.Trim(),
                CodigoPostal = model.CodigoPostal,
                EstadoId = model.EstadoId,
                MunicipioId = model.MunicipioId,
                RazonSocial = model.RazonSocial.Trim(),
                MetodoDePagoId = model.MetodoDePagoId,
                FormaDePagoId = model.FormaDePagoId,
                LimiteDeCredito = model.LimiteDeCredito.Trim(),
                DiasCredito = model.DiasCredito,
                CFDIId = model.CFDIId,
                NumeroDeCuenta = model.NumeroDeCuenta.Trim(),
                BancoId = model.BancoId,
                Activo = model.Activo,
                //Clinicas = model.Clinicas.Select(x => x.Clinica).ToList().ToCatalogListDto()
                Contacts = (ICollection<ContactListDto>)model.Contacts.ToList().ToContactListDto()
            };
        }

        public static Company ToModel(this CompanyFormDto dto)
        {
            if (dto == null) return null;

            return new Company
            {
                Id = dto.IdCompania,
                Clave = dto.Clave.Trim(),
                Contrasena = dto.Contrasena.Trim(),
                EmailEmpresarial = dto.EmailEmpresarial.Trim(),
                NombreComercial = dto.NombreComercial.Trim(),
                Procedencia = dto.Procedencia,
                ListaPrecioId = dto.ListaPrecioId,
                PromocionesId = dto.PromocionesId,
                RFC = dto.RFC.Trim(),
                CodigoPostal = dto.CodigoPostal,
                EstadoId = dto.EstadoId,
                MunicipioId = dto.MunicipioId,
                RazonSocial = dto.RazonSocial.Trim(),
                MetodoDePagoId = dto.MetodoDePagoId,
                FormaDePagoId = dto.FormaDePagoId,
                LimiteDeCredito = dto.LimiteDeCredito.Trim(),
                DiasCredito = dto.DiasCredito,
                CFDIId = dto.CFDIId,
                NumeroDeCuenta = dto.NumeroDeCuenta.Trim(),
                BancoId = dto.BancoId,
                Activo = dto.Activo,
                UsuarioCreoId = dto.UsuarioCreoId,
                FechaCreo = DateTime.Now,
                Contacts = dto.Contacts.Select(x => new Contact
                {
                    Id = x.IdContacto,
                    Nombre = x.Nombre.Trim(),
                    Telefono = x.IdContacto,
                    Correo = x.Correo,
                    Activo = x.Activo,
                    FechaCreo = DateTime.Now,
                    UsuarioCreoId = dto.UsuarioCreoId,
                }).ToList(),
            };
        }

        public static Company ToModel(this CompanyFormDto dto, Company model)
        {
            if (model == null) return null;

            return new Company
            {
                Id = dto.IdCompania,
                Clave = dto.Clave.Trim(),
                Contrasena = dto.Contrasena.Trim(),
                EmailEmpresarial = dto.EmailEmpresarial.Trim(),
                NombreComercial = dto.NombreComercial.Trim(),
                Procedencia = dto.Procedencia,
                ListaPrecioId = dto.ListaPrecioId,
                PromocionesId = dto.PromocionesId,
                RFC = dto.RFC.Trim(),
                CodigoPostal = dto.CodigoPostal,
                EstadoId = dto.EstadoId,
                MunicipioId = dto.MunicipioId,
                RazonSocial = dto.RazonSocial.Trim(),
                MetodoDePagoId = dto.MetodoDePagoId,
                FormaDePagoId = dto.FormaDePagoId,
                LimiteDeCredito = dto.LimiteDeCredito.Trim(),
                DiasCredito = dto.DiasCredito,
                CFDIId = dto.CFDIId,
                NumeroDeCuenta = dto.NumeroDeCuenta.Trim(),
                BancoId = dto.BancoId,
                Activo = dto.Activo,
                UsuarioCreoId = dto.UsuarioCreoId,
                FechaCreo = DateTime.Now,
                Contacts = dto.Contacts.Select(x => new Contact
                {
                    Id = x.IdContacto,
                    Nombre = x.Nombre.Trim(),
                    Telefono = x.IdContacto,
                    Correo = x.Correo,
                    Activo = x.Activo,
                    FechaCreo = DateTime.Now,
                    UsuarioCreoId = dto.UsuarioCreoId,
                }).ToList(),
            };
        }
    }
}
