using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Dtos.Study
{
    public class StudyTagDto
    {
        public int Id { get; set; }
        public int EtiquetaId { get; set; }
        public int EstudioId { get; set; }
        public string ClaveEtiqueta { get; set; }
        public string ClaveInicial { get; set; }
        public string Color { get; set; }
        public decimal Cantidad { get; set; }
        public int Orden { get; set; }
        public string NombreEtiqueta { get; set; }
        public string NombreEstudio { get; set; }
    }
}
