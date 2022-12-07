using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.RelaseResult
{
    public class SearchRelase
    {
       public List<DateTime> Fecha { get; set; }
       public string Search { get; set; }
        public int Area { get; set; }
        public List<int> Estudio { get; set; }
        public List<string> Medico { get; set; }
        public List<int> TipoSoli { get; set; }
        public List<string> Compañia { get; set; }
        public List<string> Sucursal { get; set; }
        public List<int> Estatus { get; set; }
    }
}
