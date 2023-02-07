using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.InvoiceCompany
{
    public class InvoiceCompanyFilterDto
    {
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public List<Guid> Companias { get; set; }
        public List<Guid> Sucursales { get; set; }
        public List<int> Ciudades { get; set; }
        public List<string> TipoFactura { get; set; }
        public string FacturaMetodo { get; set; }
        public string Buscar { get; set; }
    }
}
