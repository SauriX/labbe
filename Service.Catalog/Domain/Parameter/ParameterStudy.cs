using System;

namespace Service.Catalog.Domain.Parameter
{
    public class ParameterStudy
    {
        public Guid ParametroId { get; set; }
        public virtual Parameters Parametro { get; set; }
        public int EstudioId { get; set; }
        public virtual Study Estudio { get; set; }
        public bool Activo { get; set; }
        public string UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public int? UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }
}
