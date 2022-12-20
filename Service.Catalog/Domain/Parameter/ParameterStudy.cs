using System;

namespace Service.Catalog.Domain.Parameter
{
    public class ParameterStudy
    {
        public ParameterStudy()
        {
        }

        public ParameterStudy(int id, Guid parametroId, int estudioId, int orden)
        {
            Id = id;
            ParametroId = parametroId;
            EstudioId = estudioId;
            Orden = orden;
            Activo = true;
            FechaCreo = DateTime.Now;
        }

        public int Id { get; set; }
        public Guid ParametroId { get; set; }
        public virtual Parameter Parametro { get; set; }
        public int EstudioId { get; set; }
        public virtual Domain.Study.Study Estudio { get; set; }
        public bool Activo { get; set; }
        public int Orden { get; set; }
        public Guid? UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid? UsuarioModificoId { get; set; }
        public DateTime? FechaModifico { get; set; }
    }
}
