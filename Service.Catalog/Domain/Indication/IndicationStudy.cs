using System;

namespace Service.Catalog.Domain.Indication
{
    public class IndicationStudy
    {
        public int IndicacionId { get; set; }
        public virtual Indication Indicacion { get; set; }
        public int EstudioId { get; set; }
        public virtual Domain.Study.Study Estudio { get; set; }
        public bool Activo { get; set; }
        public Guid? UsuarioCreoId { get; set; }
        public DateTime? FechaCreo { get; set; }
        public Guid? UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }
}