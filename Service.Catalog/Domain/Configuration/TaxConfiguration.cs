using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Domain.Configuration
{
    public class TaxConfiguration
    {
        public Guid Id { get; set; }
        public string RFC { get; set; }
        public string RazonSocial { get; set; }
        public string Nombre { get; set; }
        public string CodigoPostal { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string Calle { get; set; }
        public string Colonia { get; set; }
        public string NoExterior { get; set; }
        public string NoInterior { get; set; }
        public string Telefono { get; set; }
        public string WebSite { get; set; }
        public string Email { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid SucursalId { get; set; }
    }
}
