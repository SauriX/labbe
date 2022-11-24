using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Billing.Dtos.Facturapi
{
    public class FacturapiClientDto
    {
        public string RazonSocial { get; set; }
        public string RFC { get; set; }
        public string RegimenFiscal { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public FacturapiAddressDto Domicilio { get; set; }
    }
}