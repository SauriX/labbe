using System;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Route
{
    public class RouteListDto
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string SucursalOrigenId { get; set; }
        public string SucursalDestinoId { get; set; }
        public bool Activo { get; set; }
        public virtual ICollection<Route_StudyListDto> Estudios { get; set; }
    }
}
