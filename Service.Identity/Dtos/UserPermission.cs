namespace Service.Identity.Dtos
{
    public class UserPermission
    {
        public string id { get; set; }
        public int number { get; set; }
        public string menu { get; set; }
        public string permiso { get; set; }
        public bool asignado {get; set;}
        public int tipo { get; set; }
    }
}
