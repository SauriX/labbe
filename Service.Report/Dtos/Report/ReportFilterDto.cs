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
        public DateTime FechaIndividual { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public List<DateTime> Hora { get; set; }
        public bool Grafica { get; set; }
        public string User { get; set; }
        public List<int> Servicios { get; set; }
        public List<string> Ciudad { get; set; }
        public string TipoFecha { get; set; }
    }

    public class ReportModalFilterDto
    {
        public List<string> Servicios { get; set; }
        public List<string> Ciudad { get; set; }
        public List<Guid> SucursalId { get; set; }
        public List<DateTime> Fecha { get; set; }
        public DateTime Mensual { get; set; }
    }
}