using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Domain.Study
{
    public class StudyTag
    {

        public StudyTag(int id, int estudioId, int etiquetaId, decimal cantidad, int orden, string nombre)
        {
            Id = id;
            EstudioId = estudioId;
            EtiquetaId = etiquetaId;
            Cantidad = cantidad;
            Orden = orden;
            Nombre = nombre;
        }

        public int Id { get; set; }
        public int EstudioId { get; set; }
        public virtual Study Estudio { get; set; }
        public int EtiquetaId { get; set; }
        public virtual Tapon.Tag Etiqueta { get; set; }
        public string Nombre { get; set; }
        public decimal Cantidad { get; set; }
        public int Orden { get; set; }
    }
}
