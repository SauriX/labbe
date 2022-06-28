using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Identity.Application.IApplication;
using Service.Identity.Domain.Menu;
using Service.Identity.Domain.User;
using Service.Identity.Dtos.Menu;
using Service.Identity.Dtos.Profile;
using Service.Identity.Dtos.Scopes;
using Service.Identity.Mapper;
using Service.Identity.Repository.IRepository;
using Service.Identity.Utils;
using Shared.Dictionary;
using Shared.Error;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Identity.Application
{
    public class ProfileApplication : IProfileApplication
    {
        private readonly IConfiguration _configuration;
        private readonly IProfileRepository _repository;
        private const string ADMIN = "Administrador";
        public ProfileApplication(IConfiguration configuration, IProfileRepository repository)
        {
            _configuration = configuration;
            _repository = repository;
        }

        public async Task<IEnumerable<MenuDto>> GetMenu(Guid userId)
        {
            var menu = await _repository.GetMenu(userId);

            menu = BuildMenu(menu);

            return menu.OrderBy(x => x.Orden).ToMenuDto();
        }

        public async Task<ProfileDto> GetProfile(Guid userId)
        {
            var user = await _repository.GetById(userId);

            if (user == null)
                throw new CustomException(HttpStatusCode.Unauthorized, Responses.Unregistered);

            if (!user.Activo)
                throw new CustomException(HttpStatusCode.Unauthorized, Responses.Disabled);

            return new ProfileDto
            {
                Nombre = user.NombreCompleto,
                RequiereCambio = !user.FlagPassword,
                Sucursal= user.SucursalId.ToString(),
                Admin=user.Rol.Nombre==ADMIN,
            };
        }

        public async Task<ScopesDto> GetScopes(Guid userId, string controller)
        {
            var permission = await _repository.GetScopes(userId, controller);

            return permission.ToScopesDto();
        }

        public async Task<ProfileDto> Login(LoginDto credentials)
        {
            var user = await _repository.GetByCode(credentials.Usuario);

            if (user == null)
            {
                throw new CustomException(HttpStatusCode.Unauthorized, Responses.Unregistered);
            }

            if (!user.Activo)
            {
                throw new CustomException(HttpStatusCode.Unauthorized, Responses.Disabled);
            }

            var pass = credentials.Contraseña;
            var key = _configuration.GetValue<string>("PasswordKey");

            if (Crypto.EncryptString(pass, key) != user.Contraseña)
            {
                throw new CustomException(HttpStatusCode.Unauthorized, Responses.WrongPassword);
            }

            return new ProfileDto
            {
                Nombre = user.NombreCompleto,
                Token = CreateToken(user),
                RequiereCambio = !user.FlagPassword,
                Sucursal = user.SucursalId.ToString(),
                Admin = user.Rol.Nombre == ADMIN,
            };
        }

        private List<Menu> BuildMenu(List<Menu> menu)
        {
            menu.ForEach(x =>
            {
                x.SubMenus = menu.Where(s => s.MenuPadreId == x.Id).ToList();
            });

            return menu.Where(x => x.MenuPadreId == null).ToList();
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Clave),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var _secretKey = _configuration.GetValue<string>("SecretKey");
            var issuer = _configuration.GetValue<string>("Issuer");
            var audience = _configuration.GetValue<string>("Audience");

            var _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = creds,
                Issuer = issuer,
                Audience = audience,
                Expires = DateTime.UtcNow.AddDays(1)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}