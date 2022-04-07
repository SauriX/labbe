using System;

namespace Service.Identity.Dtos
{
    public class RegisterUserDTO
    {
        public string idUsuario { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public int IdSucursal { get; set; }
        public string usertype { get; set; }
        public string Contraseña { get; set; }
        public string confirmaContraseña { get; set; }
        public bool activo { get; set; }
    }
}
