using Microsoft.EntityFrameworkCore;
using Service.Report.Context;
using Service.Report.Domain;
using Service.Report.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Service.Report.Repository
{
    public class Repository<T> : IRepository<T> where T : Base
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _entity;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entity = context.Set<T>();
        }

        public async Task<T> GetById(Guid id)
        {
            var catalog = await _entity.FindAsync(id);

            return catalog;
        }

        public async Task<List<T>> GetByIds(List<Guid> ids)
        {
            var catalog = await _entity.Where(x => ids.Contains(x.Id)).ToListAsync();

            return catalog;
        }

        public async Task<List<T>> Get(Expression<Func<T, bool>> query)
        {
            var catalog = await _entity.Where(query).ToListAsync();

            return catalog;
        }

        public async Task Create(T catalog)
        {
            _entity.Add(catalog);

            await _context.SaveChangesAsync();
        }

        public async Task Update(T catalog)
        {
            _entity.Update(catalog);

            await _context.SaveChangesAsync();
        }
    }
}
