using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.InvoiceCompany
{
    public class InvoiceFreeFilterDto
    {
        public string Buscar { get; set; }
        public Guid Compania { get; set; }
        public Guid Sucursal { get; set; }
        public List<string> Estatus { get; set; }
        public List<string> Tipo { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
    }
}
