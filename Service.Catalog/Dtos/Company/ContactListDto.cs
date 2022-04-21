namespace Service.Catalog.Dtos.Company
{
    public class ContactListDto
    {
        public int IdContacto { get; set; }
        public string Nombre { get; set; }
        public long Telefono { get; set; }
        public string Correo { get; set; }
        public bool Activo { get; set; }
    }
}
