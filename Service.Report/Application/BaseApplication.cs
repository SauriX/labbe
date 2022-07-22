using Service.Report.Application.IApplication;
using Service.Report.Domain.Branch;
using Service.Report.Domain.Medic;
using Service.Report.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Report.Application
{
    public class BaseApplication : IBaseApplication
    {
        private readonly IRepository<Branch> _branchRepository;
        private readonly IRepository<Medic> _medicRepository;

        public BaseApplication(IRepository<Branch> branchRepository = null, IRepository<Medic> medicRepository = null)
        {
            _branchRepository = branchRepository;
            _medicRepository = medicRepository;
        }

        public async Task<IEnumerable<string>> GetBranchNames(List<Guid> ids)
        {
            var branches = await _branchRepository.GetByIds(ids);

            return branches.Select(x => x.Sucursal);
        }

        public async Task<IEnumerable<string>> GetDoctorNames(List<Guid> ids)
        {
            var branches = await _medicRepository.GetByIds(ids);

            return branches.Select(x => x.NombreMedico);
        }
    }
}
