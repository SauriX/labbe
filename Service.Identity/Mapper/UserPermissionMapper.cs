using Service.Identity.Domain.permissions;
using Service.Identity.Dtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Service.Identity.Mapper
{
    public class UserPermissionMapper
    {
        public static List<Permission> toPermission(RegisterUserDTO rolForm, Guid iduser, string token, List<Menu> menus)
        {
            string jwt = token;
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(jwt);
            var claimValue = securityToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;

            List<Permission> permissions = new List<Permission>();
            var modelList = rolForm.permisos;
            foreach (var menu in menus)
            {
                Permission permission = new Permission();
                var list = modelList.Where(x => x.tipo == menu.id);
                foreach (var model in list)
                {
                    switch (model.permiso)
                    {
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
                permission.UsuarioId = iduser;
                permission.UsuarioCreoId = Guid.Parse(claimValue);
                permission.IdPermiso = Guid.NewGuid();
                permission.SubmoduloId = menu.id;
                if (permission.EnvioWapp || permission.EnvioCorreo || permission.Crear || permission.Modificación || permission.Impresión || permission.Descarga)
                {
                    permissions.Add(permission);

                }
                if (menu.subMenus != null)
                {

                    foreach (var submenu in menu.subMenus)
                    {
                        var lists = modelList.Where(x => x.tipo == submenu.id);
                        Permission permissionsub = new Permission();
                        foreach (var model2 in lists)
                        {

                            switch (model2.permiso)
                            {
                                case "Crear":
                                    permissionsub.Crear = model2.asignado;
                                    break;
                                case "Modificación":
                                    permissionsub.Modificación = model2.asignado;
                                    break;
                                case "Impresión":
                                    permissionsub.Impresión = model2.asignado;
                                    break;
                                case "Descarga":
                                    permissionsub.Descarga = model2.asignado;
                                    break;
                                case "EnvioCorreo":
                                    permissionsub.EnvioCorreo = model2.asignado;
                                    break;
                                case "EnvioWapp":
                                    permissionsub.EnvioWapp = model2.asignado;
                                    break;
                                default:
                                    break;
                            }
                        }
                        permissionsub.Activo = rolForm.activo;
                        permissionsub.FechaCreo = DateTime.Now;
                        permission.UsuarioId = iduser;
                        permissionsub.UsuarioCreoId = Guid.Parse(claimValue);
                        permissionsub.IdPermiso = Guid.NewGuid();
                        permissionsub.SubmoduloId = submenu.id;
                        if (permissionsub.EnvioWapp || permissionsub.EnvioCorreo || permissionsub.Crear || permissionsub.Modificación || permissionsub.Impresión || permissionsub.Descarga)
                        {
                            permissions.Add(permissionsub);

                        }
                    }
                }
            }

            return permissions;
        }

        public static List<Permission> toEditPermission(List<UserPermission> rolForm, Guid iduser, string token, List<Menu> menus, string idpermiso)
        {
            string jwt = token;
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(jwt);
            var claimValue = securityToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;

            List<Permission> permissions = new List<Permission>();
            var modelList = rolForm;
            foreach (var menu in menus)
            {
                Permission permission = new Permission();
                var list = modelList;
                foreach (var model in list)
                {
                    switch (model.permiso)
                    {
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
                permission.Activo = true;
                permission.FechaCreo = DateTime.Now;
                permission.RolId = iduser;
                permission.UsuarioCreoId = Guid.Parse(claimValue);
                permission.IdPermiso = new Guid();
                permission.SubmoduloId = menu.id;
                if (permission.EnvioWapp || permission.EnvioCorreo || permission.Crear || permission.Modificación || permission.Impresión || permission.Descarga)
                {
                    permissions.Add(permission);
                }
                if (menu.subMenus != null)
                {
                    foreach (var submenu in menu.subMenus)
                    {
                        var lists = modelList.Where(x => x.tipo == menu.id);
                        foreach (var model in lists)
                        {
                            switch (model.permiso)
                            {
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
                        var id = Guid.Parse(idpermiso);
                        if (string.IsNullOrEmpty(idpermiso))
                        {
                            id = new Guid();
                        }
                        permission.Activo = true;
                        permission.FechaCreo = DateTime.Now;
                        permission.RolId = iduser;
                        permission.UsuarioCreoId = Guid.Parse(claimValue);
                        permission.IdPermiso = id;
                        permission.SubmoduloId = menu.id;
                        if (permission.EnvioWapp || permission.EnvioCorreo || permission.Crear || permission.Modificación || permission.Impresión || permission.Descarga)
                        {
                            permissions.Add(permission);
                        }
                    }
                }
            }

            return permissions;
        }
        public static List<UserPermission> toListPermision(IEnumerable<Permission> permissions, List<Menu> menus, List<UserPermission> completelist)
        {
            List<UserPermission> list = new List<UserPermission>();

            var idp = 1;
            foreach (var permission in permissions)
            {
                var menu1 = menus.Where(x => x.id == permission.SubmoduloId);
                var menucount = menu1.Count();
                if (menucount > 0)
                {
                    var menu = menu1.ToArray()[0];
                    list.Add(new UserPermission
                    {
                        id = idp++,
                        permiso = "Crear",
                        asignado = permission.Crear,
                        menu = menu.descripcion,
                        tipo = menu.id
                    });
                    list.Add(new UserPermission
                    {
                        id = idp++,
                        permiso = "Modificación",
                        asignado = permission.Modificación,
                        menu = menu.descripcion,
                        tipo = menu.id
                    });
                    list.Add(new UserPermission
                    {
                        id = idp++,
                        permiso = "Impresión",
                        asignado = permission.Impresión,
                        menu = menu.descripcion,
                        tipo = menu.id
                    });
                    list.Add(new UserPermission
                    {
                        id = idp++,
                        permiso = "Descarga",
                        asignado = permission.Descarga,
                        menu = menu.descripcion,
                        tipo = menu.id
                    });
                    list.Add(new UserPermission
                    {
                        id = idp++,
                        permiso = "EnvioCorreo",
                        asignado = permission.EnvioCorreo,
                        menu = menu.descripcion,
                        tipo = menu.id
                    });
                    list.Add(new UserPermission
                    {
                        id = idp++,
                        permiso = "EnvioWapp",
                        asignado = permission.EnvioWapp,
                        menu = menu.descripcion,
                        tipo = menu.id
                    });
                }
                else
                {
                    foreach (var item in menus)
                    {
                        if (item.subMenus != null)
                        {
                            var submenu = item.subMenus.Where(x => x.id == permission.SubmoduloId).ToArray()[0];
                            list.Add(new UserPermission
                            {
                                id = idp++,
                                permiso = "Crear",
                                asignado = permission.Crear,
                                menu = submenu.descripcion,
                                tipo = submenu.id
                            });
                            list.Add(new UserPermission
                            {
                                id = idp++,
                                permiso = "Modificación",
                                asignado = permission.Modificación,
                                menu = submenu.descripcion,
                                tipo = submenu.id
                            });
                            list.Add(new UserPermission
                            {
                                id = idp++,
                                permiso = "Impresión",
                                asignado = permission.Impresión,
                                menu = submenu.descripcion,
                                tipo = submenu.id
                            });
                            list.Add(new UserPermission
                            {
                                id = idp++,
                                permiso = "Descarga",
                                asignado = permission.Descarga,
                                menu = submenu.descripcion,
                                tipo = submenu.id
                            });
                            list.Add(new UserPermission
                            {
                                id = idp++,
                                permiso = "EnvioCorreo",
                                asignado = permission.EnvioCorreo,
                                menu = submenu.descripcion,
                                tipo = submenu.id
                            });
                            list.Add(new UserPermission
                            {
                                id = idp++,
                                permiso = "EnvioWapp",
                                asignado = permission.EnvioWapp,
                                menu = submenu.descripcion,
                                tipo = submenu.id
                            });
                        }

                    }

                }
            }
            var count = list.Count;

            if (count < 42)
            {
                UserPermission[] listTemp = new UserPermission[list.Count];
                list.CopyTo(listTemp);
                foreach (var item in completelist)
                {

                    var per = list.Count(x => x.tipo == item.tipo && x.permiso == item.permiso );
                    if (per == 0)
                    {
                        item.id = idp++;
                        list.Add(item);
                    }


                }
            }
            return list;
        }
    }
}
