using System;
using System.Collections.Generic;

namespace Service.Identity.Dtos.Role
{
    public class RoleFormDto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioId { get; set; }
        public List<RolePermissionDto> Permisos { get; set; }
    }
}
