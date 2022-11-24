using Service.Billing.Dtos.Scopes;
using System.Threading.Tasks;

namespace Service.Billing.Client.IClient
{
    public interface IIdentityClient
    {
        Task<ScopesDto> GetScopes(string module);
    }
}
