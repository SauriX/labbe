namespace Service.Catalog.Domain.Catalog
{
    public class Area : GenericCatalog
    {
        public Area()
        {
        }

        public Area(int id, int departamentoId, string clave, string nombre)
        {
            Id = id;
            DepartamentoId = departamentoId;
            Clave = clave;
            Nombre = nombre;
            Activo = true;
        }

        public int DepartamentoId { get; set; }
        public virtual Department Departamento { get; set; }
    }
}
