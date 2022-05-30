using Service.Catalog.Domain.Reagent;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface IReagentRepository
    {
        Task<List<Reagent>> GetAll(string search);
        Task<List<Reagent>> GetActive();
        Task<Reagent> GetById(Guid id);
        Task<bool> IsDuplicate(Reagent reagent);
        Task Create(Reagent reagent);
        Task Update(Reagent reagent);
    }
}
