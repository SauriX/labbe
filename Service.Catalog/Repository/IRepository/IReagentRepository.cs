using Service.Catalog.Domain.Reagent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface IReagentRepository
    {
        Task<List<Reagent>> GetAll(string search = null);
        Task<Reagent> GetById(int id);
        Task Create(Reagent reagent);
        Task Update(Reagent reagent);
    }
}
