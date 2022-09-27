namespace Service.Catalog.Domain.Catalog
{
    public class UseOfCFDI​ : GenericCatalogDescription
    {
        public UseOfCFDI​()
        {
        }

        public UseOfCFDI​(int id, string clave, string nombre, string description)
        {
            Id = id;
            Clave = clave;
            Nombre = nombre;
            Descripcion = description;
            Activo = true;
        }
    }
}
