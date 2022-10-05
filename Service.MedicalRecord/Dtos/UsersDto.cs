using System;
using System.Collections.Generic;

namespace Service.MedicalRecord.Dtos
{
    public class UsersDto
    {
        public string Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public Guid RolId { get; set; }
        public string Rol { get; set; }
        public Guid SucursalId { get; set; }
        public string Contraseña { get; set; }
        public string ConfirmaContraseña { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioId { get; set; }
        public List<UserPermissionDto> Permisos { get; set; }
    }
}
