using System;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Route
{
    public class RouteListDto
    {
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public Guid? SucursalOrigenId { get; set; }
        public string SucursalOrigen { get; set; }
        public Guid? SucursalDestinoId { get; set; }
        public string SucursalDestino { get; set; }
        public int? MaquiladorId { get; set; }
        //public string Maquilador { get; set; }
        public bool Activo { get; set; }
        public virtual ICollection<Route_StudyListDto> Estudios { get; set; }
    }
}
