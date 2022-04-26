using Service.Catalog.Dtos.Study;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Parameters
{
    public class ParameterForm
    {
        public string id { get; set; }
        public string clave { get; set; }
        public string nombre { get; set; }
        public string nombreCorto { get; set; }
        public string unidades { get; set; }
        public int tipoValor { get; set; }
        public string formula { get; set; }
        public string formato { get; set; }
        public string valorInicial  { get; set; }
        public int departamento { get; set; }
        public int area { get; set; }
        public int reactivo { get; set; }
        public string unidadSi { get; set; }
        public string fcs { get; set; }
        public bool activo { get; set; }
        public string formatoImpresion { get; set; }
        public ICollection<StudyListDto> estudios { get; set; }

 
    }
}
