using Service.Catalog.Domain.Price;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Domain.Company
{
    public class Company
    {
        public Company()
        {
        }

        public Company(Guid id, string clave, string contraseña, string nombreComercial, int procedencia)
        {
            Id = id;
            Clave = clave;
            Contrasena = contraseña;
            NombreComercial = nombreComercial;
            ProcedenciaId = procedencia;
            Activo = true;
            FechaCreo = DateTime.Now;
        }

        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Contrasena { get; set; }
        public string EmailEmpresarial { get; set; }
        public string NombreComercial { get; set; }
        public int ProcedenciaId { get; set; }
        public virtual Provenance.Provenance Procedencia { get; set; }
        public Guid? PrecioListaId { get; set; }
        public virtual Price.PriceList PrecioLista { get; set; }
        public int? PromocionesId { get; set; }
        public virtual Promotion.Promotion Promociones { get; set; }
        public string RFC { get; set; }
        public string CodigoPostal { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
        public string RazonSocial { get; set; }
        public string Colonia { get; set; }
        public int ColoniaId { get; set; }
        public string RegimenFiscal { get; set; }
        public int? MetodoDePagoId { get; set; }
        public int? FormaDePagoId { get; set; }
        public string LimiteDeCredito { get; set; }
        public int? DiasCredito { get; set; }
        public int? CFDIId { get; set; }
        public string NumeroDeCuenta { get; set; }
        public int? BancoId { get; set; }
        public bool Activo { get; set; }
        public Guid? UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid? UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Price_Company> Precio { get; set; }
    }
}