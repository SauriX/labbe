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
            token = token.Replace("Bearer ", string.Empty);
            var userRol = Mapper.RolMapper.ToUserRol(rol, token);
            IdentityResult result = await _roleManager.CreateAsync(userRol);
            if (result.Succeeded) {
                var role =await _roleManager.FindByNameAsync(userRol.Name);
                if (role != null) {
                    var permiso = Mapper.PermissionMapper.toPermission(rol,role.Id,token);
                     await _context.CAT_Permisos.AddAsync(permiso);
                    await _context.SaveChangesAsync();
                    return true;                
                }
                return false;
            }
            return false;
        }

        public async Task<bool> Update(RolForm rolForm, string token) {

            if (!string.IsNullOrEmpty(token))
            {
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
                    var rol=await _roleManager.UpdateAsync(role);
                    if (rol.Succeeded) {
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
            var Roles = _roleManager.Roles.ToList();
                var rol = await _roleManager.FindByIdAsync(id);
            if (rol != null) 
            {
                var permisos = _context.CAT_Permisos.AsQueryable();
                var permiso = permisos.Where(x=>x.RolId==rol.Id);
                 var permisoM = Mapper.PermissionMapper.toListPermision(permiso);
                return Mapper.RolMapper.ToRolForm(rol,permisoM);
            }
            return null;
        }

        public async Task<List<UserPermission>> GetPermission() {
            List<UserPermission> list = new List<UserPermission>();
            list.Add(new UserPermission{
                id=1,
                permiso = "Crear",
                asignado= false,
                menu = "Permisos",
                tipo = 1
            });
            list.Add(new UserPermission
            {
                id = 2,
                permiso = "Modificación",
                asignado = false,   
                menu = "Permisos",
                tipo = 1
            });
            list.Add(new UserPermission
            {
                id = 3,
                permiso = "Impresión",
                asignado = false,
                menu = "Permisos",
                tipo = 1
            });
            list.Add(new UserPermission
            {
                id = 4,
                permiso = "Descarga",
                asignado = false,
                menu = "Permisos",
                tipo = 1
            });
            list.Add(new UserPermission
            {
                id = 5,
                permiso = "EnvioCorreo",
                asignado = false,
                menu = "Permisos",
                tipo = 1
            });
            list.Add(new UserPermission
            {
                id = 6,
                permiso = "EnvioWapp",
                asignado = false,
                menu = "Permisos",
                tipo = 1
            });

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
    }
}
