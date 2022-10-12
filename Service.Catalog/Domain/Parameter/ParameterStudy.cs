using System;

namespace Service.Catalog.Domain.Parameter
{
    public class ParameterStudy
    {
        public ParameterStudy()
        {
        }

        public ParameterStudy(Guid parametroId, int estudioId)
        {
            ParametroId = parametroId;
            EstudioId = estudioId;
            Activo = true;
        }

        public Guid ParametroId { get; set; }
        public virtual Parameter Parametro { get; set; }
        public int EstudioId { get; set; }
        public virtual Domain.Study.Study Estudio { get; set; }
        public bool Activo { get; set; }
        public string UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public int? UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }
}
