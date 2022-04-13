using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Branch;
using Service.Catalog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository
{
    public class BranchRepository : IBranchRepository
    {
        private readonly ApplicationDbContext _context;

        public BranchRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Branch>> GetAll(string search)
        {
            var branchs = _context.CAT_Sucursal.AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                branchs = branchs.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await branchs.ToListAsync();
        }

        public async Task<Branch> GetById(int id)
        {
            var branch = await _context.CAT_Sucursal.FindAsync(id);

            return branch;
        }

        public async Task Create(Branch branch)
        {
            _context.CAT_Sucursal.Add(branch);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Branch branch)
        {
            _context.CAT_Sucursal.Update(branch);

            await _context.SaveChangesAsync();
        }
    }
}
