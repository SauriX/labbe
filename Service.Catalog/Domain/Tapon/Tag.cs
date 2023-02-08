namespace Service.Catalog.Domain.Tapon
{
    public class Tag
    {
        public Tag()
        {
        }

        public Tag(int id, string clave, string nombre, string color, string claveInicial)
        {
            Id = id;
            Clave = clave;
            Nombre = nombre;
            Color = color;
            ClaveInicial = claveInicial;
        }

        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
        public string ClaveInicial { get; set; }
    }
}
