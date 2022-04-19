using FluentValidation;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Company
{
    public class CompanyFormDto
    {
        public int IdCompania { get; set; }
        public string Clave { get; set; }
        public string Contrasena { get; set; }
        public string EmailEmpresarial { get; set; }
        public string NombreComercial { get; set; }
        public int Procedencia { get; set; }
        public int? ListaPrecioId { get; set; }
        public long? PromocionesId { get; set; }
        public string RFC { get; set; }
        public int? CodigoPostal { get; set; }
        public int? EstadoId { get; set; }
        public int? MunicipioId { get; set; }
        public string RazonSocial { get; set; }
        public int MetodoDePagoId { get; set; }
        public int? FormaDePagoId { get; set; }
        public string LimiteDeCredito { get; set; }
        public int? DiasCredito { get; set; }
        public int? CFDIId { get; set; }
        public string NumeroDeCuenta { get; set; }
        public int? BancoId { get; set; }
        public bool Activo { get; set; }
        public int UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public int UduarioModId { get; set; }
        public DateTime FechaMod { get; set; }
        public virtual ICollection<ContactListDto> Contacts { get; set; }
    }

    public class CompanyFormDtoValidator : AbstractValidator<CompanyFormDto>
    {
        public CompanyFormDtoValidator()
        {
            RuleFor(x => x.IdCompania).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Clave).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Contrasena).NotEmpty().MaximumLength(100);
            RuleFor(x => x.NombreComercial).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Procedencia).NotEmpty();
            RuleFor(x => x.RFC).NotEmpty().MaximumLength(100);
            RuleFor(x => x.MunicipioId).NotEmpty();
            RuleFor(x => x.RazonSocial).NotEmpty().MaximumLength(500);
            RuleFor(x => x.MetodoDePagoId).NotEmpty();

        }

    }
}
