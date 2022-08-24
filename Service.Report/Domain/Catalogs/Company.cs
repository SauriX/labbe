using System;
using static ClosedXML.Excel.XLPredefinedFormat;

namespace Service.Report.Domain.Catalogs
{
    public class Company
    {
        public Company()
        {
        }

        public Company(Guid id, string nombre)
        {
            Id = id;
            NombreEmpresa = nombre;
        }

        public Guid Id { get; set; }
        public string NombreEmpresa { get; set; }
        public byte Convenio { get; set; }
    }
}
