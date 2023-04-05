using System;

namespace Service.Catalog.Domain.Route
{
    public class Route_Study
    {
        public Guid RouteId { get; set; }
        public virtual Route Ruta { get; set; }
        public int EstudioId { get; set; }
        public virtual Study.Study Estudio { get; set; }
        //public bool Activo { get; set; }
        public string UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public string UsuarioModId { get; set; }
        public DateTime? FechaMod { get; set; }
    }
}
