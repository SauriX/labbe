using Service.Catalog.Domain.Loyalty;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface ILoyaltyRepository
    {
        Task<List<Loyalty>> GetAll(string search);
        Task<Loyalty> GetById(Guid Id);
        Task<List<Loyalty>> GetActive();
        Task<bool> IsDuplicate(Loyalty loyalty);
        Task Create(Loyalty loyalty);
        Task Update(Loyalty loyalty);
    }
}
