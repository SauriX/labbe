using System;
using System.Collections.Generic;

namespace Service.Catalog.Domain.Company
{
    public class Company
    {
        public int Id { get; set; }
        public string Clave { get; set;}
        public string Contrasena { get; set;}
        public string EmailEmpresarial { get; set;}
        public string NombreComercial { get; set;}
        public int Procedencia { get; set;}
        public int? ListaPrecioId { get; set;}
        public long? PromocionesId { get; set;}
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
        public int? UsuarioModId { get; set;}
        public DateTime? FechaMod { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}