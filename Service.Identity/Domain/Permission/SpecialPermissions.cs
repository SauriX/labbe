using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace Service.Identity.Domain.Permission
{
    public class SpecialPermissions
    {
        [Key]
        public Guid IdPermisoEspecial { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public int SubmoduloId { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }
    }
}
