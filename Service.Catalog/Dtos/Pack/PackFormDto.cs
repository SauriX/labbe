using Service.Catalog.Domain.Study;
using System;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Pack
{
    public class PackFormDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string NombreLargo { get; set; }
        public int IdArea { get; set; }
        public string Area { get; set; }
        public int IdDepartamento { get; set; }
        public string Departamento  { get; set; }
        public bool Activo { get; set; }
        public bool visible { get; set; }
        public Guid IdUsuario { get; set; }
        public IEnumerable<PackStudyDto> Estudio { get; set; }
    }
}
