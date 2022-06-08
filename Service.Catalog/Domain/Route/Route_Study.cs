using System;

namespace Service.Catalog.Domain.Route
{
    public class Route_Study
    {
        public int RouteId { get; set; }
        public virtual Route Ruta { get; set; }
        public int EstudioId { get; set; }
        public virtual Study.Study Estudio { get; set; }
        public int Id { get; set; }
        public decimal Precio { get; set; }
        public bool Activo { get; set; }
        public long UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public string UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }
    }
}
