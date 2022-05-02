namespace Service.Catalog.Dtos.Maquilador
{
    public class MaquiladorListDto
    {
        public int Id { set; get; }
        public string Clave { set; get; }
        public string Nombre { set; get; }
        public string Correo { set; get; }
        public string Telefono { set; get; }
        public string Direccion { get; set; }
        public bool Activo { get; set; }
    }
}
