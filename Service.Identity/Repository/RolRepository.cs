using Microsoft.AspNetCore.Identity;
using Service.Identity.Context;
using Service.Identity.Domain.UsersRol;
using Service.Identity.Repository.IRepository;
using System.Threading.Tasks;

namespace Service.Identity.Repository
{
    public class RolRepository:IRolRepository
    {
        private RoleManager<UserRol> _roleManager;
        private readonly IndentityContext _context;
        public RolRepository(RoleManager<UserRol> roleManager, IndentityContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }
        public async Task Create(UserRol rol){
            IdentityResult result = await _roleManager.CreateAsync(rol);

        }
    }
}
