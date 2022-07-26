using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Dtos.StudyStats
{
    public class StudiesDto
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Estudio { get; set; }
        public string Estatus { get; set; }
        public decimal Precio { get; set; }
    }
}
