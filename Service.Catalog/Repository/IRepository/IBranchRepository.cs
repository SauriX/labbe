using Service.Catalog.Domain.Branch;
using Service.Catalog.Dtos.Study;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface IBranchRepository
    {

        //Task<IEnumerable<StudyListDto>> getservicios(string id);
        Task<List<Branch>> GetAll(string search = null);
        Task<Branch> GetById(string id);
        Task<(bool, string)> IsDuplicate(Branch branch);
        Task Create(Branch reagent);
        Task Update(Branch reagent);
        Task<List<Branch>> GetBranchByCity();
    }
}
