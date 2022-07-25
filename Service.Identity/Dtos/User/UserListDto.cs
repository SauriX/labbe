using System;

namespace Service.Identity.Dtos.User
{
    public class UserListDto
    {
        public string Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string TipoUsuario { get; set; }
        public Guid SucursalId { get; set; }
        public bool Activo { get; set; }
    }
}
