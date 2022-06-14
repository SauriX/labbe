using FluentValidation;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Company
{
    public class CompanyFormDto
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Contrasena { get; set; }
        public string EmailEmpresarial { get; set; }
        public string NombreComercial { get; set; }
        public int ProcedenciaId { get; set; }
        public string ListaPrecioId { get; set; }
        public string PromocionesId { get; set; }
        public string RFC { get; set; }
        public string CodigoPostal { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
        public string RazonSocial { get; set; }
        public int MetodoDePagoId { get; set; }
        public int? FormaDePagoId { get; set; }
        public string LimiteDeCredito { get; set; }
        public int? DiasCredito { get; set; }
        public int? CFDIId { get; set; }
        public string NumeroDeCuenta { get; set; }
        public int? BancoId { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid UduarioModId { get; set; }
        public DateTime FechaMod { get; set; }
        public IEnumerable<ContactListDto> Contacts { get; set; }
    }

    public class CompanyFormDtoValidator : AbstractValidator<CompanyFormDto>
    {
        public CompanyFormDtoValidator()
        {
            RuleFor(x => x.Clave).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Contrasena).NotEmpty().MaximumLength(100);
            RuleFor(x => x.NombreComercial).NotEmpty().MaximumLength(100);
            RuleFor(x => x.RFC).NotEmpty().MaximumLength(100);
            RuleFor(x => x.RazonSocial).NotEmpty().MaximumLength(500);
            RuleFor(x => x.MetodoDePagoId).NotEmpty();

        }

    }
}
