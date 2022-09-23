using System;

namespace Service.Catalog.Domain.Parameter
{
    public class ParameterReagent
    {
        public Guid ParametroId { get; set; }
        public virtual Parameter Parametro { get; set; }
        public Guid ReactivoId { get; set; }
        public virtual Reagent.Reagent Reactivo { get; set; }
        public string UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public string? UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }
}
