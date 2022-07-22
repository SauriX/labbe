using Service.Identity.Dtos.Menu;
using Service.Identity.Dtos.Profile;
using Service.Identity.Dtos.Scopes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Identity.Application.IApplication
{
    public interface IProfileApplication
    {
        Task<ProfileDto> Login(LoginDto credentials);
        Task<ProfileDto> GetProfile(Guid userId);
        Task<IEnumerable<MenuDto>> GetMenu(Guid userId);
        Task<ScopesDto> GetScopes(Guid userId, string controller);
    }
}
