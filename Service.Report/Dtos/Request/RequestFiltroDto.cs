using System;

namespace Service.Report.Dtos.Request
{
    public class RequestFiltroDto
    {
        public Guid Id { get; set; }
        public string NombrePaciente { get; set; }
        public string Sucursal { get; set; }
        public int Visitas { get; set;}
    }
}
