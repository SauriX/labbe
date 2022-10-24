using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos.MassSearch
{
    public class MassSearchFilterDto
    {
        public List<DateTime> Fechas { get; set; }
        public int Area { get; set; }
        public string Busqueda { get; set; }
        public List<int> Estudios { get; set; }
        public List<Guid> Sucursales { get; set; }
    }
}
