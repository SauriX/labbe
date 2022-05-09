using Service.Catalog.Domain.Catalog;
using System;

namespace Service.Catalog.Dtos.Study
{
    public class StudyListDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Titulo { get; set; }
        public string Area { get; set; }
        public string Departamento { get; set; }
        public string Formato { get; set; }
        public string Maquilador { get; set; }
        public string Metodo { get; set; }
        public bool Activo { get; set; }
    }
}
