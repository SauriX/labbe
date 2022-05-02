using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Identity.Context;
using Service.Identity.Dtos;
using Service.Identity.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Service.Identity.Mapper;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Security.Principal;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Mvc;
using Service.Identity.Dictionary;
using ClosedXML.Report;
using Shared.Extensions;
using ClosedXML.Excel;
using Service.Identity.Domain.permissions;
using System.Linq.Dynamic.Core;
using Service.Identity.Domain.User;

namespace Service.Identity.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAll(string search)
        {
            var users = _context.CAT_Usuario.Include(x => x.Rol).AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                users = users.Where(x => x.Clave.ToLower().Contains(search) || (x.Nombre + " " + x.PrimerApellido + " " + x.SegundoApellido).ToLower().Contains(search));
            }

            return await users.ToListAsync();
        }

        public async Task<User> GetById(Guid id)
        {
            var user = await _context.CAT_Usuario.Include(x => x.Rol).Include(x => x.Permisos).ThenInclude(x => x.Menu).FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task<List<UserPermission>> GetPermission(Guid? id = null)
        {
            return await GetPermissions(id);
        }

        public async Task<User> GetByCode(string code)
        {
            var user = await _context.CAT_Usuario.FirstOrDefaultAsync(x => x.Clave == code);

            return user;
        }

        public async Task<bool> IsDuplicate(User role)
        {
            var isDuplicate = await _context.CAT_Usuario.AnyAsync(x => x.Id != role.Id && x.Clave == role.Clave);

            return isDuplicate;
        }

        public async Task Create(User user)
        {
            _context.CAT_Usuario.Add(user);

            await _context.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            _context.CAT_Usuario.Update(user);

            await _context.SaveChangesAsync();
        }

        private async Task<List<UserPermission>> GetPermissions(Guid? id = null)
        {
            var permissions = await
                (from menu in _context.CAT_Menu
                 join lPer in _context.CAT_Usuario_Permiso.Where(x => x.UsuarioId == id) on menu.Id equals lPer.MenuId into ljPer
                 from p in ljPer.DefaultIfEmpty()
                 orderby menu.Orden
                 select new { menu, permission = p ?? new UserPermission() })
                 .Select(x => new UserPermission
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
