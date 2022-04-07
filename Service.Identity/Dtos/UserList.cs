using System;
using System.Collections.Generic;

namespace Service.Identity.Dtos
{
    public class UserList
    {
        public Guid IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string clave { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public Guid IdRol { get; set; }
        public int IdSucursal { get; set; }
        public bool Activo { get; set; }
        public string TipoUsuario { get; set; }
        public string contraseña { get; set; }
        public string confirmaContraseña { get; set; }
        public List<UserPermission> permisos  { get; set; }
    }
}
