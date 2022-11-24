﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integration.Invoice.Dtos
{
    public class FacturapiDto
    {
        public string Tipo { get; set; }
        public string FormaPago { get; set; }
        public string MetodoPago => "PUE";
        public string UsoCDFI { get; set; }
        public string ClaveExterna { get; set; }
        public FacturapiClientDto Cliente { get; set; }
        public List<FacturapiProductDto> Productos { get; set; }
    }
}