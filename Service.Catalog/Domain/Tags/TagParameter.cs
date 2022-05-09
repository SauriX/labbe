
using System;

namespace Service.Catalog.Domain.Tags
{
    public class TagParameter
    {
        public int TagsId { get; set; }
        public virtual Tag Etiqueta { get; set; }
        public int IdParametro { get; set; }
        public virtual Domain.Parameter.Parameter Parametro { get; set; }
        public bool Activo { get; set; }
        public string UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public int? UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }
}
