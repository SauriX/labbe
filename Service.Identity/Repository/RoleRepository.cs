using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Service.Identity.Context;
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
using Service.Identity.Domain.Role;
using Microsoft.EntityFrameworkCore;
using EFCore.BulkExtensions;

namespace Service.Identity.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetAll(string search)
        {
            var roles = _context.CAT_Rol.AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                roles = roles.Where(x => x.Nombre.ToLower().Contains(search));
            }

            return await roles.ToListAsync();
        }

        public async Task<List<Role>> GetActive()
        {
            var roles = await _context.CAT_Rol.Where(x => x.Activo).ToListAsync();

            return roles;
        }

        public async Task<Role> GetById(Guid id)
        {
            var role = await _context.CAT_Rol.FirstOrDefaultAsync(x => x.Id == id);

            role.Permisos = await GetPermission(id);

            return role;
        }

        public async Task<List<RolePermission>> GetPermission(Guid? id = null)
        {
            return await GetPermissions(id);
        }

        public async Task Create(Role role)
        {
            _context.CAT_Rol.Add(role);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsDuplicate(Role role)
        {
            var isDuplicate = await _context.CAT_Rol.AnyAsync(x => x.Id != role.Id && x.Nombre == role.Nombre);

            return isDuplicate;
        }

        public async Task Update(Role role)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var permissions = role.Permisos.ToList();

                role.Permisos = null;
                _context.CAT_Rol.Update(role);

                await _context.SaveChangesAsync();

                await _context.BulkInsertOrUpdateAsync(permissions);

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        private async Task<List<RolePermission>> GetPermissions(Guid? id = null)
        {
            var permissions = await
                (from menu in _context.CAT_Menu
                 join lPer in _context.CAT_Rol_Permiso.Where(x => x.RolId == id) on menu.Id equals lPer.MenuId into ljPer
                 from p in ljPer.DefaultIfEmpty()
                 where menu.MenuPadreId != null || (menu.MenuPadreId == null && menu.Controlador != null)
                 orderby menu.Orden
                 select new { menu, permission = p ?? new RolePermission() })
                 .Select(x => new RolePermission
                 {
                     MenuId = x.menu.Id,
                     Menu = x.menu,
                     Acceder = x.permission.Acceder,
                     Crear = x.permission.Crear,
                     Modificar = x.permission.Modificar,
                     Descargar = x.permission.Descargar,
                     Imprimir = x.permission.Imprimir,
                     EnviarCorreo = x.permission.EnviarCorreo,
                     EnviarWapp = x.permission.EnviarWapp,
                 })
                 .ToListAsync();

            return permissions;
        }
    }
}
