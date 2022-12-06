using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.MedicalRecord.Dtos.Invoice
{
    public class ProductDto
    {
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public int Cantidad { get; set; }
    }
}