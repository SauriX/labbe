namespace Service.Catalog.Domain.Catalog
{
    public class Units : GenericCatalogDescription
    {
        public Units()
        {
        }

        public Units(int id, string clave)
        {
            Id = id;
            Clave = clave;
            Nombre = clave;
            Descripcion = clave;
            Activo = true;
        }
    }
}
