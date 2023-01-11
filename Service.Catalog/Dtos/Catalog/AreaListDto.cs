using System.Collections.Generic;

namespace Service.Catalog.Dtos.Catalog
{
    public class AreaListDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public string Departamento { get; set; }
    }

    public class DepartmentAreaListDto
    {
        public IEnumerable<AreaListDto> Areas { get; set; }
        public int DepartamentoId { get; set; }
        public string Departamento { get; set; }
    }
}
