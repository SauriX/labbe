using Service.Report.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Service.Report.Repository.IRepository
{
    public interface IRepository<T> where T : Base
    {
        Task<T> GetById(Guid id);
        Task<List<T>> GetByIds(List<Guid> id);
        Task<List<T>> Get(Expression<Func<T, bool>> query);
        Task Create(T model);
        Task Update(T model);
    }
}
