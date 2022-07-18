using Service.Identity.Domain.Permission;
using Service.Identity.Dtos;
using Service.Identity.Dtos.Menu;
using Service.Identity.Dtos.Role;
using Service.Identity.Dtos.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Identity.Mapper
{
    public class PermissionMapper
    {
        public static List<Permission> toPermission(RoleFormDto rolForm, Guid idRol, string token, List<MenuDto> menus)
        {
            string jwt = token;
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(jwt);
            var claimValue = securityToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;

            List<Permission> permissions = new List<Permission>();
            var modelList = rolForm.Permisos;
            foreach (var menu in menus)
            {
                Permission permission = new Permission();
                var list = modelList.Where(x => x.Tipo == menu.Id);
                foreach (var model in list)
                {
                    switch (model.Permiso)
                    {
                        case "Crear":
                            permission.Crear = model.Asignado;
                            break;
                        case "Modificación":
                            permission.Modificación = model.Asignado;
                            break;
                        case "Impresión":
                            permission.Impresión = model.Asignado;
                            break;
                        case "Descarga":
                            permission.Descarga = model.Asignado;
                            break;
                        case "EnvioCorreo":
                            permission.EnvioCorreo = model.Asignado;
                            break;
                        case "EnvioWapp":
                            permission.EnvioWapp = model.Asignado;
                            break;
                        default:
                            break;
                    }
                }
                permission.Activo = rolForm.Activo;
                permission.FechaCreo = DateTime.Now;
                permission.RolId = idRol;
                permission.UsuarioCreoId = Guid.Parse(claimValue);
                //permission.IdPermiso = Guid.NewGuid();
                permission.SubmoduloId = menu.Id;
                if (permission.EnvioWapp || permission.EnvioCorreo || permission.Crear || permission.Modificación || permission.Impresión || permission.Descarga)
                {
                    permissions.Add(permission);

                }
                if (menu.SubMenus != null)
                {

                    foreach (var submenu in menu.SubMenus)
                    {
                        var lists = modelList.Where(x => x.Tipo == submenu.Id);
                        Permission permissionsub = new Permission();
                        foreach (var model2 in lists)
                        {

                            switch (model2.Permiso)
                            {
                                case "Crear":
                                    permissionsub.Crear = model2.Asignado;
                                    break;
                                case "Modificación":
                                    permissionsub.Modificación = model2.Asignado;
                                    break;
                                case "Impresión":
                                    permissionsub.Impresión = model2.Asignado;
                                    break;
                                case "Descarga":
                                    permissionsub.Descarga = model2.Asignado;
                                    break;
                                case "EnvioCorreo":
                                    permissionsub.EnvioCorreo = model2.Asignado;
                                    break;
                                case "EnvioWapp":
                                    permissionsub.EnvioWapp = model2.Asignado;
                                    break;
                                default:
                                    break;
                            }
                        }
                        permissionsub.Activo = rolForm.Activo;
                        permissionsub.FechaCreo = DateTime.Now;
                        permissionsub.RolId = idRol;
                        permissionsub.UsuarioCreoId = Guid.Parse(claimValue);
                        //permissionsub.IdPermiso = Guid.NewGuid();
                        permissionsub.SubmoduloId = submenu.Id;
                        if (permissionsub.EnvioWapp || permissionsub.EnvioCorreo || permissionsub.Crear || permissionsub.Modificación || permissionsub.Impresión || permissionsub.Descarga)
                        {
                            permissions.Add(permissionsub);

                        }
                    }
                }
            }

            return permissions;
        }

        public static List<Permission> toEditPermission(List<UserPermissionDto> rolForm, Guid idRol, string token, List<MenuDto> menus, string idpermiso)
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
                    switch (model.Permiso)
                    {
                        case "Crear":
                            permission.Crear = model.Asignado;
                            break;
                        case "Modificación":
                            permission.Modificación = model.Asignado;
                            break;
                        case "Impresión":
                            permission.Impresión = model.Asignado;
                            break;
                        case "Descarga":
                            permission.Descarga = model.Asignado;
                            break;
                        case "EnvioCorreo":
                            permission.EnvioCorreo = model.Asignado;
                            break;
                        case "EnvioWapp":
                            permission.EnvioWapp = model.Asignado;
                            break;
                        default:
                            break;
                    }
                }
                permission.Activo = true;
                permission.FechaCreo = DateTime.Now;
                permission.RolId = idRol;
                permission.UsuarioCreoId = Guid.Parse(claimValue);
                //permission.IdPermiso = new Guid();
                permission.SubmoduloId = menu.Id;
                if (permission.EnvioWapp || permission.EnvioCorreo || permission.Crear || permission.Modificación || permission.Impresión || permission.Descarga)
                {
                    permissions.Add(permission);
                }
                if (menu.SubMenus != null)
                {
                    foreach (var submenu in menu.SubMenus)
                    {
                        var lists = modelList.Where(x => x.Tipo == menu.Id);
                        foreach (var model in lists)
                        {
                            switch (model.Permiso)
                            {
                                case "Crear":
                                    permission.Crear = model.Asignado;
                                    break;
                                case "Modificación":
                                    permission.Modificación = model.Asignado;
                                    break;
                                case "Impresión":
                                    permission.Impresión = model.Asignado;
                                    break;
                                case "Descarga":
                                    permission.Descarga = model.Asignado;
                                    break;
                                case "EnvioCorreo":
                                    permission.EnvioCorreo = model.Asignado;
                                    break;
                                case "EnvioWapp":
                                    permission.EnvioWapp = model.Asignado;
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
                        permission.RolId = idRol;
                        permission.UsuarioCreoId = Guid.Parse(claimValue);
                        //permission.IdPermiso = id;
                        permission.SubmoduloId = menu.Id;
                        if (permission.EnvioWapp || permission.EnvioCorreo || permission.Crear || permission.Modificación || permission.Impresión || permission.Descarga)
                        {
                            permissions.Add(permission);
                        }
                    }
                }
            }

            return permissions;
        }
        public static List<UserPermissionDto> toListPermision(IEnumerable<Permission> permissions, List<MenuDto> menus, List<UserPermissionDto> completelist)
        {
            List<UserPermissionDto> list = new List<UserPermissionDto>();

            var idp = 1;
            foreach (var permission in permissions)
            {
                var menu1 = menus.Where(x => x.Id == permission.SubmoduloId);
                var menucount = menu1.Count();
                if (menucount > 0)
                {
                    var menu = menu1.ToArray()[0];
                    list.Add(new UserPermissionDto
                    {
                        Id = idp++,
                        Permiso = "Crear",
                        Asignado = permission.Crear,
                        Menu = menu.Descripcion,
                        Tipo = menu.Id
                    });
                    list.Add(new UserPermissionDto
                    {
                        Id = idp++,
                        Permiso = "Modificación",
                        Asignado = permission.Modificación,
                        Menu = menu.Descripcion,
                        Tipo = menu.Id
                    });
                    list.Add(new UserPermissionDto
                    {
                        Id = idp++,
                        Permiso = "Impresión",
                        Asignado = permission.Impresión,
                        Menu = menu.Descripcion,
                        Tipo = menu.Id
                    });
                    list.Add(new UserPermissionDto
                    {
                        Id = idp++,
                        Permiso = "Descarga",
                        Asignado = permission.Descarga,
                        Menu = menu.Descripcion,
                        Tipo = menu.Id
                    });
                    list.Add(new UserPermissionDto
                    {
                        Id = idp++,
                        Permiso = "EnvioCorreo",
                        Asignado = permission.EnvioCorreo,
                        Menu = menu.Descripcion,
                        Tipo = menu.Id
                    });
                    list.Add(new UserPermissionDto
                    {
                        Id = idp++,
                        Permiso = "EnvioWapp",
                        Asignado = permission.EnvioWapp,
                        Menu = menu.Descripcion,
                        Tipo = menu.Id
                    });
                }
                else
                {
                    foreach (var item in menus)
                    {
                        if (item.SubMenus != null)
                        {
                            var submenu = item.SubMenus.Where(x => x.Id == permission.SubmoduloId).ToArray()[0];
                            list.Add(new UserPermissionDto
                            {
                                Id = idp++,
                                Permiso = "Crear",
                                Asignado = permission.Crear,
                                Menu = submenu.Descripcion,
                                Tipo = submenu.Id
                            });
                            list.Add(new UserPermissionDto
                            {
                                Id = idp++,
                                Permiso = "Modificación",
                                Asignado = permission.Modificación,
                                Menu = submenu.Descripcion,
                                Tipo = submenu.Id
                            });
                            list.Add(new UserPermissionDto
                            {
                                Id = idp++,
                                Permiso = "Impresión",
                                Asignado = permission.Impresión,
                                Menu = submenu.Descripcion,
                                Tipo = submenu.Id
                            });
                            list.Add(new UserPermissionDto
                            {
                                Id = idp++,
                                Permiso = "Descarga",
                                Asignado = permission.Descarga,
                                Menu = submenu.Descripcion,
                                Tipo = submenu.Id
                            });
                            list.Add(new UserPermissionDto
                            {
                                Id = idp++,
                                Permiso = "EnvioCorreo",
                                Asignado = permission.EnvioCorreo,
                                Menu = submenu.Descripcion,
                                Tipo = submenu.Id
                            });
                            list.Add(new UserPermissionDto
                            {
                                Id = idp++,
                                Permiso = "EnvioWapp",
                                Asignado = permission.EnvioWapp,
                                Menu = submenu.Descripcion,
                                Tipo = submenu.Id
                            });
                        }

                    }

                }
            }
            var count = list.Count;

            if (count < 42)
            {
                UserPermissionDto[] listTemp = new UserPermissionDto[list.Count];
                list.CopyTo(listTemp);
                foreach (var item in completelist)
                {

                    var per = list.Count(x => x.Tipo == item.Tipo && x.Permiso == item.Permiso);
                    if (per == 0)
                    {
                        item.Id = idp++;
                        list.Add(item);
                    }


                }
            }
            return list;
        }
    }
}
