using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Service.Identity.Domain.UsersRol
{
    public class UserRol: IdentityRole<Guid>
    {
        [Key]
        public Guid IdRol { get; set; }
        public string RolUsuario { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }

    }
}
