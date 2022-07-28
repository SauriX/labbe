using System;

namespace Service.Report.Dtos.ContactStats
{
    public class ContactStatsChartDto
    {
        public Guid Id { get; set; }
        public string Fecha { get; set; }
        public int Solicitudes { get; set; }
        public int CantidadTelefono { get; set; }
        public int CantidadCorreo { get; set; }
        public int Total => CantidadTelefono + CantidadCorreo;
    }
}
