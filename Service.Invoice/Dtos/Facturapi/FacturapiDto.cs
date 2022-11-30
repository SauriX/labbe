using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Billing.Dtos.Facturapi
{
    public class FacturapiDto
    {
        public string FacturapiId { get; set; }
        public string Tipo { get; set; }
        public string FormaPago { get; set; }
        public string MetodoPago => "PUE";
        public string UsoCDFI { get; set; }
        public string ClaveExterna { get; set; }
        public FacturapiClientDto Cliente { get; set; }
        public List<FacturapiProductDto> Productos { get; set; }
    }
}