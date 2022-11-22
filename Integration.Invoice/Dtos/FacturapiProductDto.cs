using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integration.Invoice.Dtos
{
    public class FacturapiProductDto
    {
        public string Descripcion { get; set; }
        public string ClaveProductoSAT => "85121800"; // http://pys.sat.gob.mx/PyS/catPyS.aspx -> Laboratorios médicos (Servicios de análisis clínicos)
        public decimal Precio { get; set; }
        public bool IncliyeIVA => true;
    }
}