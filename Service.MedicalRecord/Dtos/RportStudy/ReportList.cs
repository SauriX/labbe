using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.RportStudy
{
    public class ReportList
    {
        public string Sucursal { get; set; }
        public string Tipo {get; set;}
        public string SucursalRaw { get; set; } 
        public List<ReportRequestListDto> Requests { get; set; }
    }
}
