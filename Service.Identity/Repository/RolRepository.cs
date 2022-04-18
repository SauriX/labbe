using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Service.Identity.Context;
using Service.Identity.Domain.UsersRol;
using Service.Identity.Dtos;
using Service.Identity.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Report;
using Shared.Extensions;
using ClosedXML.Excel;
using Service.Identity.Dictionary;
using Service.Identity.Domain.permissions;

namespace Service.Identity.Repository
{
    public class RolRepository : IRolRepository 
    {
        private RoleManager<UserRol> _roleManager;
        private readonly IndentityContext _context;
        public RolRepository(RoleManager<UserRol> roleManager, IndentityContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }
        public async Task<bool> Create(RolForm rol, string token) {
            var Menus = await getMenus();
            token = token.Replace("Bearer ", string.Empty);
            var userRol = Mapper.RolMapper.ToUserRol(rol, token);
            IdentityResult result = await _roleManager.CreateAsync(userRol);
            if (result.Succeeded) {
                var role = await _roleManager.FindByNameAsync(userRol.Name);
                if (role != null) {
                    var permisos = Mapper.PermissionMapper.toPermission(rol, role.Id, token,Menus);
                    foreach (Permission permiso in permisos) {
                        await _context.CAT_Permisos.AddAsync(permiso);
                        await _context.SaveChangesAsync();
                    }
                    
                    return true;
                }
                return false;
            }
            return false;
        }

        public async Task<bool> Update(RolForm rolForm, string token) {

            if (!string.IsNullOrEmpty(token))
            {
                var Menus = await getMenus();
                token = token.Replace("Bearer ", string.Empty);
                string jwt = token;
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(jwt);
                var claimValue = securityToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
                var idMod = claimValue;
                if (_roleManager.Roles.Any(r => r.Id == Guid.Parse(rolForm.Id)))
                {
                    var role = _roleManager.Roles.First(r => r.Id == Guid.Parse(rolForm.Id));

                    role.Name = rolForm.nombre;
                    role.Activo = rolForm.activo;
                    role.FechaMod = DateTime.Now;
                    role.UsuarioModId = Guid.Parse(idMod);
                    var rol = await _roleManager.UpdateAsync(role);
                    if (rol.Succeeded) {
                        var permisions = _context.CAT_Permisos.ToList();
                        var permisios = permisions.Where(x => x.RolId == Guid.Parse(rolForm.Id));
                        foreach (var permiso in permisios)
                        {
               

                                _context.CAT_Permisos.Remove(permiso);
                                _context.SaveChanges();

                    
                        }
                            var nuevosPermisos = Mapper.PermissionMapper.toPermission(rolForm, role.Id, token, Menus);
                            foreach (Permission permiso in nuevosPermisos)
                            {
                                await _context.CAT_Permisos.AddAsync(permiso);
                                await _context.SaveChangesAsync();
                            }
                        

                        return true;
                    }
                }
            }
            return false;
        }

        public async Task<List<RolInfo>> GetAll(string search)
        {
            var roles = _context.Roles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                search = search.Trim().ToLower();
                roles = roles.Where(x => x.RolUsuario.ToLower().Contains(search));
            }
            else
            {
                roles = roles.Where(x => x.Activo);
            }
            var listRol = Mapper.RolMapper.ToRolListDto(roles);
            return (List<RolInfo>)listRol;
        }

        public async Task<RolForm> GetById(string id)
        {
            var Menus = await getMenus();
            var permisoslist = await GetPermission();
            var Roles = _roleManager.Roles.ToList();
            var rol = await _roleManager.FindByIdAsync(id);
            if (rol != null)
            {
                var permisos = _context.CAT_Permisos.AsQueryable();
                var permiso = permisos.Where(x => x.RolId == rol.Id);
                var permisoM = Mapper.PermissionMapper.toListPermision(permiso,Menus,permisoslist);
                
                return Mapper.RolMapper.ToRolForm(rol, permisoM);
            }
            return null;
        }

        public async Task<List<UserPermission>> GetPermission() {
            List<UserPermission> list = new List<UserPermission>();
            var Menus = await getMenus();
            var idP = 1;
            foreach (Menu menu in Menus) {
                list.Add(new UserPermission {
                    id = idP++,
                    permiso = "Crear",
                    asignado = false,
                    menu = menu.descripcion,
                    tipo = menu.id
                });
                list.Add(new UserPermission
                {
                    id = idP++,
                    permiso = "Modificación",
                    asignado = false,
                    menu = menu.descripcion,
                    tipo = menu.id
                });
                list.Add(new UserPermission
                {
                    id = idP++,
                    permiso = "Impresión",
                    asignado = false,
                    menu = menu.descripcion,
                    tipo = menu.id
                });
                list.Add(new UserPermission
                {
                    id = idP++,
                    permiso = "Descarga",
                    asignado = false,
                    menu = menu.descripcion,
                    tipo = menu.id
                });
                list.Add(new UserPermission
                {
                    id = idP++,
                    permiso = "EnvioCorreo",
                    asignado = false,
                    menu = menu.descripcion,
                    tipo = menu.id
                });
                list.Add(new UserPermission
                {
                    id = idP++,
                    permiso = "EnvioWapp",
                    asignado = false,
                    menu = menu.descripcion,
                    tipo = menu.id
                });
                if (menu.subMenus != null) {
                    foreach (Menu subMenu in menu.subMenus.ToList<Menu>()) {
                        list.Add(new UserPermission
                        {
                            id = idP++,
                            permiso = "Crear",
                            asignado = false,
                            menu = subMenu.descripcion,
                            tipo = subMenu.id
                        });
                        list.Add(new UserPermission
                        {
                            id = idP++,
                            permiso = "Modificación",
                            asignado = false,
                            menu = subMenu.descripcion,
                            tipo = subMenu.id
                        });
                        list.Add(new UserPermission
                        {
                            id = idP++,
                            permiso = "Impresión",
                            asignado = false,
                            menu = subMenu.descripcion,
                            tipo = subMenu.id
                        });
                        list.Add(new UserPermission
                        {
                            id = idP++,
                            permiso = "Descarga",
                            asignado = false,
                            menu = subMenu.descripcion,
                            tipo = subMenu.id
                        });
                        list.Add(new UserPermission
                        {
                            id = idP++,
                            permiso = "EnvioCorreo",
                            asignado = false,
                            menu = subMenu.descripcion,
                            tipo = subMenu.id
                        });
                        list.Add(new UserPermission
                        {
                            id = idP++,
                            permiso = "EnvioWapp",
                            asignado = false,
                            menu = subMenu.descripcion,
                            tipo = subMenu.id
                        });
                    }
                }
            }
            return list;
        }
        public async Task<byte[]> ExportList(string search = null)
        {
            var rol = await GetAll(search);

            var path = Assets.RoleList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Roles");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Roles", rol);

            template.Generate();

            var range = template.Workbook.Worksheet("Roles").Range("Roles");
            var table = template.Workbook.Worksheet("Roles").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return template.ToByteArray();
        }

        public async Task<byte[]> ExportForm(string id)
        {
            var rol = await GetById(id);

            var path = Assets.RoleForm;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Roles");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Rol", rol);
            template.AddVariable("Permisos", rol.permisos);
            template.Generate();

            return template.ToByteArray();
        }

        public async Task<List<Menu>>getMenus(){
            List<Menu> menu = new List<Menu>();
            List<Menu> subMenu = new List<Menu>();
            subMenu.Add(new Menu { id = 3, ruta = "users", icono = "user", descripcion = "Usuarios" });
            subMenu.Add(new Menu { id = 4, ruta = "roles", icono = "role", descripcion = "Roles" });
            subMenu.Add(new Menu { id = 7, ruta = "sucursales", icono = "laboratorio", descripcion = "Sucursales" });
            menu.Add(new Menu   {
                id= 1,
                ruta= "",
                icono= "home",
                descripcion= "Inicio",
            });
            menu.Add(new Menu   {
                id= 2,
                icono= "admin",
                descripcion= "Administración",
                subMenus=subMenu,
            });
            menu.Add(new Menu   {
                id= 5,
                ruta= "medics",
                descripcion= "Medicos",
                icono= "medico",
            });
            menu.Add(new Menu  {
                id= 6,
                ruta= "indication",
                descripcion= "Indicaciones",
                icono= "role",
            });
            return menu;
        }

    }
}
