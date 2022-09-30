using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Service.Identity.Domain.Role
{
    public class Role
    {
        public Role()
        {
        }

        public Role(Guid id, string nombre)
        {
            Id = id;
            Nombre = nombre;
            Activo = true;
        }

        [Key]
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public Guid? UsuarioCreoId { get; set; }
        public DateTime? FechaCreo { get; set; }
        public Guid? UsuarioModificoId { get; set; }
        public DateTime? FechaModifico { get; set; }

        public virtual ICollection<RolePermission> Permisos { get; set; }
    }
}
