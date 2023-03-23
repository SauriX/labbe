using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.InvoiceCompany
{
    public class InvoiceCompanyFilterDto
    {
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public List<Guid> Companias { get; set; }
        public List<Guid> SucursalId { get; set; }
        public List<int> Ciudades { get; set; }
        public List<string> TipoFactura { get; set; }
        public List<int?> Departamentos { get; set; } = new List<int?>();
        public List<Guid> Medicos { get; set; } = new List<Guid>();
        public List<byte> Urgencias { get; set; } = new List<byte>();
        public string FacturaMetodo { get; set; }
        public string Buscar { get; set; }
    }
}
