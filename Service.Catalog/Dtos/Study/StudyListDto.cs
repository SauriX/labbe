using Service.Catalog.Dtos.Indication;
using Service.Catalog.Dtos.Parameter;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Study
{
    public class StudyListDto
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Titulo { get; set; }
        public string Area { get; set; }
        public int AreaId { get; set; }
        public string Departamento { get; set; }
        public string Formato { get; set; }
        public string Maquilador { get; set; }
        public string Metodo { get; set; }
        public bool Activo { get; set; }
        public IEnumerable<ParameterListDto> Parametros { get; set; }
        public IEnumerable<IndicationListDto> Indicaciones { get; set; }
    }
}
