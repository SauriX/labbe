﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integration.Invoice.Dtos
{
    public class FacturapiProductDto
    {
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public string ClaveProductoSAT => "85121800"; // http://pys.sat.gob.mx/PyS/catPyS.aspx -> Laboratorios médicos (Servicios de análisis clínicos)
        public string ClaveUnidadSAT => "E48"; // http://pys.sat.gob.mx/PyS/catUnidades.aspx -> Unidades específicas de la industria (varias)
        public string ClaveUnidadNombreSAT => "Unidad de Servicio"; // http://pys.sat.gob.mx/PyS/catUnidades.aspx -> Unidad de Servicio
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public int Cantidad { get; set; }
    }
}