using DocumentFormat.OpenXml.Office2010.Excel;
using Service.Catalog.Domain.Medics;
using static ClosedXML.Excel.XLPredefinedFormat;

namespace Service.Catalog.Domain.Catalog
{
    public class Method : GenericCatalog
    {
        public Method()
        {
        }

        public Method(int id, string clave, string nombre)
        {
            Id = id;
            Clave = clave;
            Nombre = nombre;
            Activo = true;
        }
    }
}
