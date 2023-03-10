using System;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Route
{
    public class RouteListDto
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public Guid? OrigenId { get; set; }
        public string Origen { get; set; }
        public Guid? DestinoId { get; set; }
        public string Destino { get; set; }
        public int? MaquiladorId { get; set; }
        //public string Maquilador { get; set; }
        public bool Activo { get; set; }
        public virtual ICollection<Route_StudyListDto> Estudios { get; set; }
    }
}
