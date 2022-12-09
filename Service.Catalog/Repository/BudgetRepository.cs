using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Catalog;
using Service.Catalog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly ApplicationDbContext _context;

        public BudgetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Budget>> GetAll(string search)
        {
            var budget = _context.CAT_Presupuestos.Include(x => x.Sucursal).AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                budget = budget.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await budget.ToListAsync();
        }

        public async Task<List<Budget>> GetActive()
        {
            var budgets = await _context.CAT_Presupuestos.Include(x => x.Sucursal).Where(x => x.Activo).ToListAsync();

            return budgets;
        }

        public async Task<List<Budget>> GetBudgetByBranch(Guid branchId)
        {
            var budgets = await _context.CAT_Presupuestos.Include(x => x.Sucursal).Where(x => x.SucursalId == branchId && x.Activo).ToListAsync();

            return budgets;
        }

        public async Task<Budget> GetById(int id)
        {
            var budget = await _context.CAT_Presupuestos.Include(x => x.Sucursal).FirstOrDefaultAsync(x => x.Id == id);

            return budget;
        }

        public async Task<IEnumerable<Budget>> GetBudgets(Guid id)
        {
            var catalog = await _context.CAT_Presupuestos.Where(x => x.SucursalId == id && x.Activo).ToListAsync();

            return catalog;
        }

        public async Task<bool> IsDuplicate(Budget catalog)
        {
            var isDuplicate = await _context.CAT_Presupuestos.AnyAsync(x => x.Id != catalog.Id && x.SucursalId == catalog.SucursalId && (x.Clave == catalog.Clave || x.Nombre == catalog.Nombre));

            return isDuplicate;
        }

        public async Task Create(Budget budget)
        {
            _context.CAT_Presupuestos.Add(budget);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Budget budget)
        {
            _context.CAT_Presupuestos.Add(budget);

            await _context.SaveChangesAsync();
        }
    }
}
