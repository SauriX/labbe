﻿using Service.Catalog.Dtos.Scopes;
using System.Threading.Tasks;

namespace Service.Catalog.Client.IClient
{
    public interface IIdentityClient
    {
        Task<ScopesDto> GetScopes(string module);
    }
}
