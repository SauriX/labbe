﻿using Microsoft.AspNetCore.Http;

namespace Service.Billing.Dto.Series
{
    public class SeriesDto
    {
        public InvoiceSerieDto Factura { get; set; }
        public OwnerInfoDto Emisor { get; set; }
        public ExpeditionPlaceDto Expedicion { get; set; }
    }

    public class InvoiceSerieDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public byte TipoSerie { get; set; }
        public string Contraseña { get; set; }
        public IFormFile ArchivoCer { get; set; }
        public IFormFile ArchivoKey { get; set; }
        public bool Activo { get; set; }
        public string Observaciones { get; set; }
    }

    public class OwnerInfoDto
    {
        public string RFC { get; set; }
        public bool CFDI { get; set; }
        public string Nombre { get; set; }
        public string CodigoPostal { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string Ciudad { get; set; }
        public string Calle { get; set; }
        public string Colonia { get; set; }
        public string NoExterior { get; set; }
        public string NoInterior { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string WebSite { get; set; }
    }

    public class ExpeditionPlaceDto
    {
        public string CodigoPostal { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string Calle { get; set; }
        public string Colonia { get; set; }
        public string NoExterior { get; set; }
        public string NoInterior { get; set; }
        public string Telefono { get; set; }
    }
}