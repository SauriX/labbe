using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.PendingRecive
{
    public class PendingSearchDto
    {
        public DateTime? Fecha { get; set; }
        public List<string> Sucursal { get; set; }
        public string Sucursaldest { get; set; }
        public string Busqueda { get; set; }
    }
}
