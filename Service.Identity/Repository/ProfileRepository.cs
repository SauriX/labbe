using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Identity.Context;
using Service.Identity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Security.Claims;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Service.Identity.Repository.IRepository;
using System.Linq;
using Service.Identity.Domain.Menu;
using Microsoft.EntityFrameworkCore;
using Service.Identity.Domain.User;

namespace Service.Identity.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public ProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetById(Guid id)
        {
            return await _context.CAT_Usuario.FindAsync(id);
        }

        public async Task<User> GetByCode(string code)
        {
            return await _context.CAT_Usuario.FirstOrDefaultAsync(x => x.Clave == code);
        }

        public async Task<List<Menu>> GetMenu(Guid userId)
        {
            return await _context.CAT_Usuario
                .Include(x => x.Permisos).ThenInclude(x => x.Menu)
                .Where(x => x.Id == userId)
                .SelectMany(x => x.Permisos.Where(x => x.Acceder))
                .Select(x => x.Menu)
                .ToListAsync();
        }

        public async Task<UserPermission> GetScopes(Guid userId, string controller)
        {
            return await _context.CAT_Usuario
                .Include(x => x.Permisos).ThenInclude(x => x.Menu)
                .Where(x => x.Id == userId)
                .SelectMany(x => x.Permisos)
                .FirstOrDefaultAsync(x => x.Menu.Controlador.ToLower() == controller.ToLower());
        }
    }
}
