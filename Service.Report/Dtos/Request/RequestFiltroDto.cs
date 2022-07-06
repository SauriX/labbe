using System;

namespace Service.Report.Dtos.Request
{
    public class RequestFiltroDto
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public int Visitas { get; set;}
    }
}
