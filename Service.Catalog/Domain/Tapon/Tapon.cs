namespace Service.Catalog.Domain.Tapon
{
    public class Tapon
    {
        public Tapon()
        {
        }

        public Tapon(int id, string clave, string nombre, string color)
        {
            Id = id;
            Clave = clave;
            Nombre = nombre;
            Color = color;
        }

        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
    }
}
