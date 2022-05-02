using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.Parameter;
using Service.Catalog.Dtos.Catalog;
using Service.Catalog.Dtos.Reagent;
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
        public int formatoImpresion { get; set; }
        public IEnumerable<StudyListDto> estudios { get; set; }
        public Area areas { get; set; }
        public ReagentFormDto reactivos { get; set; }
        public Format format { get; set; }

    }
}
