using System;
using System.ComponentModel.DataAnnotations;

namespace Service.Identity.Domain.Permission
{
    public class RolPermiso
    {
        [Key]
        public Guid IdRelacion_Rol_PermisoE { get; set; }
        public Guid RolId { get; set; }
        public int PermisoEspecialId { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid UsuarioModId { get; set; }
        public DateTime FechaMod { get; set; }
    }
}
