using Service.Identity.Domain.permissions;
using Service.Identity.Dtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Service.Identity.Mapper
{
    public class PermissionMapper
    {
        public static Permission toPermission(RolForm rolForm,Guid idRol,string token) {
            string jwt = token;
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(jwt);
            var claimValue = securityToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
            Permission permission = new Permission();
            var modelList = rolForm.permisos;
            foreach (var model in modelList) {
                switch (model.permiso) {
                    case "Crear":
                        permission.Crear = model.asignado;
                        break;
                    case "Modificación":
                        permission.Modificación = model.asignado;
                        break;
                    case "Impresión":
                        permission.Impresión = model.asignado;
                        break;
                    case "Descarga":
                        permission.Descarga = model.asignado;
                        break;
                    case "EnvioCorreo":
                        permission.EnvioCorreo = model.asignado;
                        break;
                    case "EnvioWapp":
                        permission.EnvioWapp = model.asignado;
                        break;
                    default:
                        break;
                }            
            }
            permission.Activo = rolForm.activo;
            permission.FechaCreo = DateTime.Now;
            permission.RolId = idRol;
            permission.UsuarioCreoId = Guid.Parse(claimValue);
            permission.IdPermiso = new Guid();
            return permission;
        }

        public static List<UserPermission> toListPermision(IEnumerable<Permission> permissions) { 
            List<UserPermission> list = new List<UserPermission>();
            foreach (var permission in permissions)
            {
                list.Add(new UserPermission
                {
                    id = 1,
                    permiso = "Crear",
                    asignado = permission.Crear,
                    menu = "Permisos",
                    tipo = 1
                });
                list.Add(new UserPermission
                {
                    id = 2,
                    permiso = "Modificación",
                    asignado = permission.Modificación,
                    menu = "Permisos",
                    tipo = 1
                });
                list.Add(new UserPermission
                {
                    id = 3,
                    permiso = "Impresión",
                    asignado = permission.Impresión,
                    menu = "Permisos",
                    tipo = 1
                });
                list.Add(new UserPermission
                {
                    id = 4,
                    permiso = "Descarga",
                    asignado = permission.Descarga,
                    menu = "Permisos",
                    tipo = 1
                });
                list.Add(new UserPermission
                {
                    id = 5,
                    permiso = "EnvioCorreo",
                    asignado = permission.EnvioCorreo,
                    menu = "Permisos",
                    tipo = 1
                });
                list.Add(new UserPermission
                {
                    id = 6,
                    permiso = "EnvioWapp",
                    asignado = permission.EnvioWapp,
                    menu = "Permisos",
                    tipo = 1
                });
            }
            return list;
        }
    }
}
