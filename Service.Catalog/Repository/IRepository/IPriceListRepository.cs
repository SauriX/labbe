using Service.Catalog.Domain.Price;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface IPriceListRepository
    {
        Task<List<PriceList>> GetAll(string search);
        Task<PriceList> GetById(int Id);
        Task<List<PriceList>> GetActive();
        Task<bool> IsDuplicate(PriceList price);
        Task Create(PriceList price);
        Task Update(PriceList price);
    }
}
