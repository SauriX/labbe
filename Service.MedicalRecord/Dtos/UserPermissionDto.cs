namespace Service.MedicalRecord.Dtos
{
    public class UserPermissionDto
    {
        public UserPermissionDto() { }

        public UserPermissionDto(int id, short menuId, string menu, string permiso, bool asignado, byte tipo)
        {
            Id = id;
            MenuId = menuId;
            Menu = menu;
            Permiso = permiso;
            Asignado = asignado;
            Tipo = tipo;
        }

        public int Id { get; set; }
        public short MenuId { get; set; }
        public string Menu { get; set; }
        public string Permiso { get; set; }
        public bool Asignado { get; set; }
        public int Tipo { get; set; }
    }
}
