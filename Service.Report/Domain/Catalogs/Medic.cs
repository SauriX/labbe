using System;
using static ClosedXML.Excel.XLPredefinedFormat;

namespace Service.Report.Domain.Catalogs
{
    public class Medic
    {
        public Medic()
        {
        }

        public Medic(Guid id, string clave, string nombre)
        {
            Id = id;
            ClaveMedico = clave;
            NombreMedico = nombre;
        }

        public Guid Id { get; set; }
        public string ClaveMedico { get; set; }
        public string NombreMedico { get; set; }
    }
}
