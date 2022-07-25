using ClosedXML.Excel;
using ClosedXML.Report;
using Service.Identity.Application.IApplication;
using Service.Identity.Dictionary;
using Service.Identity.Domain.Role;
using Service.Identity.Dtos.Role;
using Service.Identity.Mapper;
using Service.Identity.Repository.IRepository;
using Shared.Dictionary;
using Shared.Error;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Service.Identity.Application
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IRoleRepository _repository;

        public RoleApplication(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RoleListDto>> GetAll(string search)
        {
            var roles = await _repository.GetAll(search);

            return roles.ToRoleListDto();
        }

        public async Task<IEnumerable<RoleListDto>> GetActive()
        {
            var roles = await _repository.GetActive();

            return roles.ToRoleListDto();
        }

        public async Task<RoleFormDto> GetById(string id)
        {
            var validGuid = Guid.TryParse(id, out Guid guid);

            if (!validGuid)
            {
                throw new CustomException(HttpStatusCode.BadRequest, Responses.NotFound);
            }

            var role = await _repository.GetById(guid);

            if (role == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return role.ToRoleFormDto();
        }

        public async Task<IEnumerable<RolePermissionDto>> GetPermission(string id = null)
        {
            var valid = Guid.TryParse(id, out Guid guid);

            List<RolePermission> permissions;

            if (valid)
            {
                permissions = await _repository.GetPermission(guid);
            }
            else
            {
                permissions = await _repository.GetPermission();
            }

            return permissions.ToRolePermissionDto();
        }

        public async Task<RoleListDto> Create(RoleFormDto role)
        {
            if (!string.IsNullOrWhiteSpace(role.Id))
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            var newRole = role.ToModel();

            var isDuplicate = await _repository.IsDuplicate(newRole);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("El nombre"));
            }

            await _repository.Create(newRole);

            newRole = await _repository.GetById(newRole.Id);

            return newRole.ToRoleListDto();
        }

        public async Task<RoleListDto> Update(RoleFormDto role)
        {
            var validGuid = Guid.TryParse(role.Id, out Guid guid);

            if (!validGuid)
            {
                throw new CustomException(HttpStatusCode.BadRequest, Responses.NotFound);
            }

            var existing = await _repository.GetById(guid);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedRole = role.ToModel(existing);

            var isDuplicate = await _repository.IsDuplicate(updatedRole);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("El nombre"));
            }

            await _repository.Update(updatedRole);

            updatedRole = await _repository.GetById(guid);

            return updatedRole.ToRoleListDto();
        }

        public async Task<(byte[] file, string fileName)> ExportList(string search = null)
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

            return (template.ToByteArray(), "Catálogo de Roles.xlsx");
        }

        public async Task<(byte[] file, string fileName)> ExportForm(string id)
        {
            var rol = await GetById(id);

            var path = Assets.RoleForm;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Roles");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Rol", rol);
            template.AddVariable("Permisos", rol.Permisos);
            template.Generate();

            return (template.ToByteArray(), $"Catálogo de Roles ({rol.Nombre}).xlsx");
        }
    }
}



//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Service.Identity.Context;
//using Service.Identity.Domain.RolesRol;
//using Service.Identity.Dtos;
//using Service.Identity.Repository.IRepository;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Threading.Tasks;
//using ClosedXML.Report;
//using Shared.Extensions;
//using ClosedXML.Excel;
//using Service.Identity.Dictionary;
//using Service.Identity.Domain.permissions;

//namespace Service.Identity.Repository
//{
//    public class RoleRepository : IRoleRepository
//    {
//        private RoleManager<RoleRol> _roleManager;
//        private readonly ApplicationDbContext _context;
//        public RoleRepository(RoleManager<RoleRol> roleManager, ApplicationDbContext context)
//        {
//            _roleManager = roleManager;
//            _context = context;
//        }
//        public async Task<bool> Create(RoleFormDto rol, string token)
//        {
//            var Menus = await getMenus();
//            token = token.Replace("Bearer ", string.Empty);
//            var roleRol = Mapper.RolMapper.ToRoleRol(rol, token);
//            IdentityResult result = await _roleManager.CreateAsync(roleRol);
//            if (result.Succeeded)
//            {
//                var role = await _roleManager.FindByNameAsync(roleRol.Name);
//                if (role != null)
//                {
//                    var permisos = Mapper.PermissionMapper.toPermission(rol, role.Id, token, Menus);
//                    foreach (Permission permiso in permisos)
//                    {
//                        await _context.CAT_Permisos.AddAsync(permiso);
//                        await _context.SaveChangesAsync();
//                    }

//                    return true;
//                }
//                return false;
//            }
//            return false;
//        }

//        public async Task<bool> Update(RoleFormDto rolForm, string token)
//        {

//            if (!string.IsNullOrEmpty(token))
//            {
//                var Menus = await getMenus();
//                token = token.Replace("Bearer ", string.Empty);
//                string jwt = token;
//                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
//                var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(jwt);
//                var claimValue = securityToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
//                var idMod = claimValue;
//                if (_roleManager.Roles.Any(r => r.Id == Guid.Parse(rolForm.Id)))
//                {
//                    var role = _roleManager.Roles.First(r => r.Id == Guid.Parse(rolForm.Id));

//                    role.Name = rolForm.Nombre;
//                    role.Activo = rolForm.Activo;
//                    role.FechaMod = DateTime.Now;
//                    role.UsuarioModId = Guid.Parse(idMod);
//                    var rol = await _roleManager.UpdateAsync(role);
//                    if (rol.Succeeded)
//                    {
//                        var permisions = _context.CAT_Permisos.ToList();
//                        var permisios = permisions.Where(x => x.RolId == Guid.Parse(rolForm.Id));
//                        foreach (var permiso in permisios)
//                        {


//                            _context.CAT_Permisos.Remove(permiso);
//                            _context.SaveChanges();


//                        }
//                        var nuevosPermisos = Mapper.PermissionMapper.toPermission(rolForm, role.Id, token, Menus);
//                        foreach (Permission permiso in nuevosPermisos)
//                        {
//                            await _context.CAT_Permisos.AddAsync(permiso);
//                            await _context.SaveChangesAsync();
//                        }


//                        return true;
//                    }
//                }
//            }
//            return false;
//        }

//        public async Task<List<RoleListDto>> GetAll(string search)
//        {
//            var roles = _context.Roles.AsQueryable();

//            if (!string.IsNullOrWhiteSpace(search) && search != "all")
//            {
//                search = search.Trim().ToLower();
//                roles = roles.Where(x => x.RolUsuario.ToLower().Contains(search));
//            }
//            else
//            {
//                roles = roles.Where(x => x.Activo);
//            }
//            var listRol = Mapper.RolMapper.ToRolListDto(roles);
//            return (List<RoleListDto>)listRol;
//        }

//        public async Task<RoleFormDto> GetById(string id)
//        {
//            var Menus = await getMenus();
//            var permisoslist = await GetPermission();
//            var Roles = _roleManager.Roles.ToList();
//            var rol = await _roleManager.FindByIdAsync(id);
//            if (rol != null)
//            {
//                var permisos = _context.CAT_Permisos.AsQueryable();
//                var permiso = permisos.Where(x => x.RolId == rol.Id);
//                var permisoM = Mapper.PermissionMapper.toListPermision(permiso, Menus, permisoslist);

//                return Mapper.RolMapper.ToRolForm(rol, permisoM);
//            }
//            return null;
//        }

//        public async Task<List<RolePermissionDto>> GetPermission()
//        {
//            List<RolePermissionDto> list = new List<RolePermissionDto>();
//            var Menus = await getMenus();
//            var idP = 1;
//            foreach (MenuDto menu in Menus)
//            {
//                list.Add(new RolePermissionDto
//                {
//                    Id = idP++,
//                    Permiso = "Crear",
//                    Asignado = false,
//                    Menu = menu.Descripcion,
//                    Tipo = menu.Id
//                });
//                list.Add(new RolePermissionDto
//                {
//                    Id = idP++,
//                    Permiso = "Modificación",
//                    Asignado = false,
//                    Menu = menu.Descripcion,
//                    Tipo = menu.Id
//                });
//                list.Add(new RolePermissionDto
//                {
//                    Id = idP++,
//                    Permiso = "Impresión",
//                    Asignado = false,
//                    Menu = menu.Descripcion,
//                    Tipo = menu.Id
//                });
//                list.Add(new RolePermissionDto
//                {
//                    Id = idP++,
//                    Permiso = "Descarga",
//                    Asignado = false,
//                    Menu = menu.Descripcion,
//                    Tipo = menu.Id
//                });
//                list.Add(new RolePermissionDto
//                {
//                    Id = idP++,
//                    Permiso = "EnvioCorreo",
//                    Asignado = false,
//                    Menu = menu.Descripcion,
//                    Tipo = menu.Id
//                });
//                list.Add(new RolePermissionDto
//                {
//                    Id = idP++,
//                    Permiso = "EnvioWapp",
//                    Asignado = false,
//                    Menu = menu.Descripcion,
//                    Tipo = menu.Id
//                });
//                if (menu.SubMenus != null)
//                {
//                    foreach (MenuDto subMenu in menu.SubMenus.ToList<MenuDto>())
//                    {
//                        list.Add(new RolePermissionDto
//                        {
//                            Id = idP++,
//                            Permiso = "Crear",
//                            Asignado = false,
//                            Menu = subMenu.Descripcion,
//                            Tipo = subMenu.Id
//                        });
//                        list.Add(new RolePermissionDto
//                        {
//                            Id = idP++,
//                            Permiso = "Modificación",
//                            Asignado = false,
//                            Menu = subMenu.Descripcion,
//                            Tipo = subMenu.Id
//                        });
//                        list.Add(new RolePermissionDto
//                        {
//                            Id = idP++,
//                            Permiso = "Impresión",
//                            Asignado = false,
//                            Menu = subMenu.Descripcion,
//                            Tipo = subMenu.Id
//                        });
//                        list.Add(new RolePermissionDto
//                        {
//                            Id = idP++,
//                            Permiso = "Descarga",
//                            Asignado = false,
//                            Menu = subMenu.Descripcion,
//                            Tipo = subMenu.Id
//                        });
//                        list.Add(new RolePermissionDto
//                        {
//                            Id = idP++,
//                            Permiso = "EnvioCorreo",
//                            Asignado = false,
//                            Menu = subMenu.Descripcion,
//                            Tipo = subMenu.Id
//                        });
//                        list.Add(new RolePermissionDto
//                        {
//                            Id = idP++,
//                            Permiso = "EnvioWapp",
//                            Asignado = false,
//                            Menu = subMenu.Descripcion,
//                            Tipo = subMenu.Id
//                        });
//                    }
//                }
//            }
//            return list;
//        }
//        public async Task<byte[]> ExportList(string search = null)
//        {

//        }

//        public async Task<byte[]> ExportForm(string id)
//        {

//        }

//        public async Task<List<MenuDto>> getMenus()
//        {
//            List<MenuDto> menu = new List<MenuDto>();
//            List<MenuDto> subMenu = new List<MenuDto>();
//            subMenu.Add(new MenuDto { Id = 3, Ruta = "roles", Icono = "role", Descripcion = "Usuarios" });
//            subMenu.Add(new MenuDto { Id = 4, Ruta = "roles", Icono = "role", Descripcion = "Roles" });
//            subMenu.Add(new MenuDto { Id = 7, Ruta = "sucursales", Icono = "laboratorio", Descripcion = "Sucursales" });
//            menu.Add(new MenuDto
//            {
//                Id = 1,
//                Ruta = "",
//                Icono = "home",
//                Descripcion = "Inicio",
//            });
//            menu.Add(new MenuDto
//            {
//                Id = 2,
//                Icono = "admin",
//                Descripcion = "Administración",
//                SubMenus = subMenu,
//            });
//            menu.Add(new MenuDto
//            {
//                Id = 5,
//                Ruta = "medics",
//                Descripcion = "Medicos",
//                Icono = "medico",
//            });
//            menu.Add(new MenuDto
//            {
//                Id = 6,
//                Ruta = "indication",
//                Descripcion = "Indicaciones",
//                Icono = "role",
//            });
//            return menu;
//        }

//    }
//}
