﻿using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _entity;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entity = context.Set<T>();
        }

        public async Task<T> GetOne(Expression<Func<T, bool>> query)
        {
            var catalog = await _entity.Where(query).FirstOrDefaultAsync();

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
