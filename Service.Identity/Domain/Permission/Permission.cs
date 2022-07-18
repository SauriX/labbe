﻿using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace Service.Identity.Domain.Permission
{
    public class Permission
    {
        [Key]
        public Guid Id { get; set; }
        public string Clave { get; set; }
        public bool Acceso { get; set; }
        public bool Crear { get; set; }
        public bool Modificación { get; set; }
        public bool Impresión { get; set; }
        public bool Descarga { get; set; }
        public bool EnvioCorreo { get; set; }
        public bool EnvioWapp { get; set; }
        public Guid RolId { get; set; }
        public Guid UsuarioId { get; set; }
        public int SubmoduloId { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioCreoId { get; set; }
        public DateTime FechaCreo { get; set; }
        public Guid UsuarioModificoId { get; set; }
        public DateTime FechaModifico { get; set; }
    }
}
