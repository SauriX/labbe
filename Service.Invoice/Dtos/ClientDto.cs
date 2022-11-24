using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Billing.Dtos
{
    public class ClientDto
    {
        public string RazonSocial { get; set; }
        public string RFC { get; set; }
        public string RegimenFiscal { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string CodigoPostal { get; set; }
        public string Calle { get; set; }
        public string NumeroExterior { get; set; }
        public string NumeroInterior { get; set; }
        public string Colonia { get; set; }
        public string Ciudad { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
    }
}