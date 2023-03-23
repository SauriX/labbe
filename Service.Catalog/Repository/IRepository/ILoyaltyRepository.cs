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
        Task<Loyalty> GetByDate(DateTime fecha);
        Task<Loyalty> GetByPriceListDate(DateTime date, Guid priceList);
        Task<List<Loyalty>> GetActive();
        Task<bool> IsPorcentaje(Loyalty loyalty);
        Task<bool> IsDuplicateDate(DateTime fechainicial, DateTime fechafinal, Guid id);
        Task Create(Loyalty loyalty);
        Task Update(Loyalty loyalty);
    }
}
