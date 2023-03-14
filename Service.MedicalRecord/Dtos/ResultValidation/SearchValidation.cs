using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.ResultValidation
{
    public class SearchValidation
    {
        public DateTime[]? Fecha  { get; set; }
        public string Search { get; set; }
        public int Area { get; set; }
        public List<string> Ciudad { get; set; }
      //  public int? Departament { get; set; }
        public List<int> Estudio { get; set; }
        public List<string> Medico { get; set; }
        public List<int> TipoSoli { get; set; }
        public List<string> Compañia { get; set; }
        public List<string> Sucursal { get; set; }
        public List<int> Estatus { get; set; }
    }
}
