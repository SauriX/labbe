using System;

namespace Service.Catalog.Domain.Tags
{
    public class TagStudy
    {
        public int TagsId { get; set; }
        public virtual Tag Etiqueta { get; set; }
        public int EstudioId { get; set; }
        public virtual Domain.Study.Study Estudio { get; set; }
        public bool Activo { get; set; }
        public string UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public int? UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }
}
