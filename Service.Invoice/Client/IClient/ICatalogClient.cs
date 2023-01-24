using Service.Billing.Domain.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Billing.Client.IClient
{
    public interface ICatalogClient
    {
        Task<BranchInfo> GetBranchByName(string name);
    }
}
