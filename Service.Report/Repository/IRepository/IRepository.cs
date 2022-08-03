using Service.Report.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Service.Report.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetOne(Expression<Func<T, bool>> query);
        Task<List<T>> Get(Expression<Func<T, bool>> query);
        Task Create(T model);
        Task Update(T model);
    }
}
