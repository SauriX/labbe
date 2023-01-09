using Microsoft.EntityFrameworkCore;
using Service.Identity.Context;
using Service.Identity.Domain.Menu;
using Service.Identity.Domain.User;
using Service.Identity.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return await _context.CAT_Usuario.Include(x => x.Rol).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetByCode(string code)
        {
            return await _context.CAT_Usuario.Include(x => x.Rol).FirstOrDefaultAsync(x => x.Clave == code);
        }

        public async Task<List<Menu>> GetMenu(Guid userId)
        {
            var menus = await _context.CAT_Usuario
                .Include(x => x.Permisos).ThenInclude(x => x.Menu)
                .Where(x => x.Id == userId)
                .SelectMany(x => x.Permisos.Where(y => y.Acceder))
                .Select(x => x.Menu).Where(x => x.Id != 29)
                .OrderBy(x => x.Orden)
                .ToListAsync();

            var finalMenu = new List<Menu>();

            foreach (var menu in menus)
            {
                finalMenu.Add(menu);
                if (menu.MenuPadreId != null && !finalMenu.Any(x => x.Id == menu.MenuPadreId))
                {
                    var menupadre = await _context.CAT_Menu.FirstOrDefaultAsync(x => x.Id == menu.MenuPadreId);

                    finalMenu.Add(menupadre);
                }
            }

            return finalMenu;
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
