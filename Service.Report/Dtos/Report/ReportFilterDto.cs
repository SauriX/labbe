using System;
using System.Collections.Generic;

namespace Service.Report.Dtos
{
    public class ReportFilterDto
    {
        public List<Guid> SucursalId { get; set; }
        public List<Guid> MedicoId { get; set; }
        public List<Guid> CompañiaId { get; set; }
        public List<byte> MetodoEnvio { get; set; }
        public List<byte> Urgencia { get; set; }
        public List<byte> TipoCompañia { get; set; }
        public List<DateTime> Fecha { get; set; }
        public bool Grafica { get; set; }

    }
}