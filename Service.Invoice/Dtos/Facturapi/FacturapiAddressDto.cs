﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Billing.Dtos.Facturapi
{
    public class FacturapiAddressDto
    {
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