using Service.Report.Dtos.Scopes;
using System.Threading.Tasks;

namespace Service.Report.Client.IClient
{
    public interface IIdentityClient
    {
        Task<ScopesDto> GetScopes(string module);
    }
}
