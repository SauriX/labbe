using System.Collections.Generic;

namespace Service.Catalog.Dtos.Pack
{
    public class PackListDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string NombreLargo { get; set; }
        public bool Activo { get; set; }
        public IEnumerable<PackStudyDto> Pack { get; set; }
        public string Departamento { get; set; }
        public string Area { get; set; }
    }
}
