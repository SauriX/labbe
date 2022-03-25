using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace Service.Identity.Domain.permissions
{
    public class SpecialPermissions
    {
        [Key]
        public Guid IdPermisoEspecial { get; set; }
        public String Clave { get; set; }
        public String Nombre { get; set; }
        public int SubmoduloId { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }
    }
}
