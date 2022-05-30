using Service.Identity.Dtos.User;
using System;
using System.Collections.Generic;

namespace Service.Identity.Dtos.User
{
    public class UserListDto
    {
        public string Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string TipoUsuario { get; set; }
        public int? SucursalId { get; set; }
        public bool Activo { get; set; }
    }
}
