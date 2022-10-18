using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.WorkList
{
    public class WorkListFilterDto
    {
        public List<int> Areas { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        public List<Guid> Sucursales { get; set; }
    }
}
