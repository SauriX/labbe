﻿using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.Company
{
    public class CompanyFormDto
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Contrasena { get; set; }
        public string EmailEmpresarial { get; set; }
        public string NombreComercial { get; set; }
        public int ProcedenciaId { get; set; }
        public Guid? PrecioListaId { get; set; }
        public int? PromocionesId { get; set; }
        public string Procedencia { get; set; }
        public string PrecioLista { get; set; }
        public string Promociones { get; set; }
        public string RFC { get; set; }
        public string CodigoPostal { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
        public string RazonSocial { get; set; }
        public int? ColoniaId { get; set; }
        public string Colonia { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string RegimenFiscal { get; set; }
        public int? MetodoDePagoId { get; set; }
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
}
