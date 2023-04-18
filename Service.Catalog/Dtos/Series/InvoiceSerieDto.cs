using Microsoft.AspNetCore.Http;
using System;

namespace Service.Catalog.Dto.Series
{
    public class SeriesDto
    {
        public int? Id { get; set; }
        public InvoiceSerieDto Factura { get; set; }
        public ExpeditionPlaceDto Expedicion { get; set; }
        public Guid UsuarioId { get; set; }
    }

    public class TicketDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public byte TipoSerie => 2;
        public bool Estatus { get; set; }
        public Guid UsuarioId { get; set; }
        public ExpeditionPlaceDto Expedicion { get; set; }
    }

    public class InvoiceSerieDto
    {
        public InvoiceSerieDto()
        {
            Año = DateTime.Now;
        }

        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public byte TipoSerie => 1;
        public string Contraseña { get; set; }
        public bool CFDI { get; set; }
        public IFormFile ArchivoCer { get; set; }
        public IFormFile ArchivoKey { get; set; }
        public string ClaveCer { get; set; }
        public string ClaveKey { get; set; }
        public bool Estatus { get; set; }
        public DateTime Año { get; set; }
        public string Observaciones { get; set; }
        public string SucursalKey { get; set; }
    }

    public class ExpeditionPlaceDto
    {
        public string CodigoPostal { get; set; }
        public string Pais => "México";
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string Calle { get; set; }
        public string Colonia { get; set; }
        public string NoExterior { get; set; }
        public string NoInterior { get; set; }
        public string Telefono { get; set; }
        public string SucursalId { get; set; }
        public string SucursalKey { get; set; }
        public string Correo { get; set; }
    }
}
