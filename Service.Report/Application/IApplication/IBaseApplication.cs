using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Report.Application.IApplication
{
    public interface IBaseApplication
    {
        Task<IEnumerable<string>> GetBranchNames(List<Guid> ids);
        Task<IEnumerable<string>> GetDoctorNames(List<Guid> ids);
        Task<IEnumerable<string>> GetCompanyNames(List<Guid> ids);
    }
}
