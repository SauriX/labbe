using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System;
using System.Collections.Generic;

namespace Service.Report.Domain.MedicalRecord
{
    public class ServicesCost
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Sucursal { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaAlta { get; set; }
        public decimal CostoFijo { get; set; }
        public List<string> Sucursales => new List<string>
        {
            Sucursal
        };
    }
}
