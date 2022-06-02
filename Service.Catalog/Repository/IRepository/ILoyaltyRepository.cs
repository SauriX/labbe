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
        //Task<bool> IsDuplicate(string clave, string nombre, Guid id);
        Task<bool> IsDuplicateDate(DateTime fechainicial, DateTime fechafinal, Guid id);
        Task Create(Loyalty loyalty);
        Task Update(Loyalty loyalty);
    }
}
