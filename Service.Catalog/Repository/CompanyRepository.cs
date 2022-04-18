﻿using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Company;
using Service.Catalog.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Company> GetById(int Id)
        {
            return await _context.CAT_Compañia
            .Include(x => x.Contacts)
            .ThenInclude(x => x.Compañia)
            .FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<List<Company>> GetAll(string search)
        {
            var Company = _context.CAT_Compañia.AsQueryable();
            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                Company = Company.Where(x => x.Clave.ToLower().Contains(search) || x.NombreComercial.ToLower().Contains(search));
            }

            return await Company.ToListAsync();
        }

        public async Task Create(Company company)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var contacts = company.Contacts.ToList();

                company.Contacts = null;
                _context.CAT_Compañia.Add(company);

                await _context.SaveChangesAsync();

                contacts.ForEach(x => x.CompañiaId = company.Id);
                await _context.BulkInsertOrUpdateOrDeleteAsync(contacts);

                transaction.Commit();
            }
            catch (System.Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task Update(Company company)
        {
            var contact = company.Contacts.ToList();

            company.Contacts = null;
            _context.CAT_Compañia.Update(company);

            await _context.BulkInsertOrUpdateOrDeleteAsync(contact);

            await _context.SaveChangesAsync();
        }
    }
}