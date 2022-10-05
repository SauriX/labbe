using Service.MedicalRecord.Dtos;
using Service.MedicalRecord.Dtos.Scopes;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Client.IClient
{
    public interface IIdentityClient
    {
        Task<ScopesDto> GetScopes(string module);
        Task<UsersDto> GetByid(string id);
    }
}
