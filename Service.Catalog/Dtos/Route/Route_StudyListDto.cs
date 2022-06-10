using System;

namespace Service.Catalog.Dtos.Route
{
    public class Route_StudyListDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Area { get; set; }
        public string Departamento { get; set; }
        public bool Activo { get; set; }
    }
}
