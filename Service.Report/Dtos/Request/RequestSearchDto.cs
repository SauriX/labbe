using System;

namespace Service.Report.Dtos.Request
{
    public class RequestSearchDto
    {
        public string ciudad { get; set; }
        public string sucursal { get; set; }
        public DateTime? FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }
        public DateTime Fecha { get; set; }
    }
}
