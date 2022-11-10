using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.ResultValidation
{
    public class SearchValidation
    {
        public DateTime[] Fecha  { get; set; }
        public string Search { get; set; }
        public List<int> Departament { get; set; }
       /* area:number[],
        estudio:number[],
        medico:string[],
        tipoSoli:number[],
        compañia:string[],
        sucursal:string[],
        estatus:number[],*/
    }
}
