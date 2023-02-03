using System;

namespace Service.MedicalRecord.Dtos.InvoiceCatalog
{
    public class InvoiceCatalogSearch
    {
        public DateTime[] Fecha { get; set; } 
        public string[] Sucursal { get; set; }
        public string Buscar { get; set; }
        public string[] Tipo { get; set; }
        public string[] Ciudad { get; set; }
    }
}
