namespace Service.Identity.Dtos
{
    public class UserPermission
    {
        public int id { get; set; }
        public string menu { get; set; }
        public string permiso { get; set; }
        public bool asignado {get; set;}
        public int tipo { get; set; }
    }
}
