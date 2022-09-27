namespace Service.Catalog.Domain.Catalog
{
    public class Payment : GenericCatalogDescription
    {
        public Payment()
        {
        }

        public Payment(int id, string clave, string nombre, string description)
        {
            Id = id;
            Clave = clave;
            Nombre = nombre;
            Descripcion = description;
            Activo = true;
        }
    }
}
