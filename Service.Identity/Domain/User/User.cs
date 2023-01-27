using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Service.Identity.Domain.User
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string NombreCompleto => Nombre + " " + PrimerApellido + " " + SegundoApellido;
        public Guid RolId { get; set; }
        public virtual Role.Role Rol { get; set; }
        public Guid SucursalId { get; set; }
        public string Contraseña { get; set; }
        public bool Activo { get; set; }
        public bool FlagPassword { get; set; }
        public Guid? UsuarioCreoId { get; set; }
        public DateTime? FechaCreo { get; set; }
        public Guid? UsuarioModificoId { get; set; }
        public DateTime? FechaModifico { get; set; }

        public virtual ICollection<UserPermission> Permisos { get; set; }
        public virtual ICollection<UserBranches> Sucursales { get; set; }
        public virtual ICollection<RequestImage> Imagenes { get; set; }
    }
}
