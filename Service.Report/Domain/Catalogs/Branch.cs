using System;
using static ClosedXML.Excel.XLPredefinedFormat;

namespace Service.Report.Domain.Catalogs
{
    public class Branch
    {
        public Branch()
        {
        }

        public Branch(Guid id, string nombre)
        {
            Id = id;
            Sucursal = nombre;
        }

        public Guid Id { get; set; }
        public string Sucursal { get; set; }
    }
}
