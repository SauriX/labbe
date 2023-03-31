using Service.MedicalRecord.Dtos.Catalogs;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.Request
{
    public class RequestStudyParamsDto
    {
        public int Id { get; set; }
        public List<ParameterListDto> Parametros { get; set; }
        public List<IndicationListDto> Indicaciones { get; set; }
        public List<StudyTagDto> Etiquetas { get; set; }
        public string Metodo { get; set; }
        public int Orden { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
    }
}
