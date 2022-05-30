using Service.Catalog.Dtos.Scopes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Client.IClient
{
    public interface IIdentityClient
    {
        Task<ScopesDto> GetScopes(string module);
    }
}
