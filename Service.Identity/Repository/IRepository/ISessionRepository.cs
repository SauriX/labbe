using Service.Identity.Dtos;
using System.Threading.Tasks;

namespace Service.Identity.Repository.IRepository
{
    public interface ISessionRepository
    {
        public Task<LoginResponse> Login(LoginDto user);
    }
}
