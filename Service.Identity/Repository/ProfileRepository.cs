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
                .SelectMany(x => x.Permisos.Where(y => y.Acceder || x.Permisos.Select(p => p.Menu.MenuPadreId).Contains(y.Menu.Id)))
                .Select(x => x.Menu)
                .ToListAsync();
            var menuspadres = new List<Menu>();
            foreach (var menu in menus)
            {
                if (menu.MenuPadreId != null)
                {
                    var menupadre = _context.CAT_Menu.FirstOrDefault(x => x.Id == menu.MenuPadreId);
                    if (menuspadres.Count() != 0)
                    {
                        var menpad = menuspadres.Where(x => x.Id == menu.MenuPadreId);
                        if (menpad.Count() == 0)
                        {
                            menuspadres.Add(menupadre);
                        }
                    }
                    else
                    {
                        menuspadres.Add(menupadre);
                    }


                }

            }
            return menus.Concat(menuspadres).ToList();
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
