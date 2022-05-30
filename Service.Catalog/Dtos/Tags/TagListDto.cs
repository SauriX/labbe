using Service.Catalog.Domain.Tags;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Tags
{
    public class TagListDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public int Cantidad { get; set; }
        public bool Activo { get; set; }
        public virtual ICollection<TagStudy> Estudios { get; set; }
        public virtual ICollection<TagParameter> Parametros { get; set; }
    }
}
