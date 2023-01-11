using EFCore.BulkExtensions;
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
            var budget = _context.CAT_Presupuestos.Include(x => x.Sucursales).AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                budget = budget.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await budget.ToListAsync();
        }

        public async Task<List<Budget>> GetActive()
        {
            var budgets = await _context.CAT_Presupuestos.Include(x => x.Sucursales).Where(x => x.Activo).ToListAsync();

            return budgets;
        }

        public async Task<List<BudgetBranch>> GetBudgetByBranch(Guid branchId)
        {
            var budgets = await _context.Relacion_Presupuesto_Sucursal
                .Include(x => x.Sucursal)
                .Include(x => x.CostoFijo)
                .Where(x => x.SucursalId == branchId)
                .ToListAsync();

            return budgets;
        }

        public async Task<List<BudgetBranch>> GetBudgetsByBranch(List<Guid> branchId)
        {
            var budgets = await _context.Relacion_Presupuesto_Sucursal
                .Include(x => x.Sucursal)
                .Include(x => x.CostoFijo)
                .Where(x => branchId.Contains(x.SucursalId))
                .ToListAsync();

            return budgets;
        }

        public async Task<Budget> GetById(int id)
        {
            var budget = await _context.CAT_Presupuestos.Include(x => x.Sucursales).FirstOrDefaultAsync(x => x.Id == id);

            return budget;
        }

        public async Task<IEnumerable<Budget>> GetBudgets(List<int> ids)
        {
            var budgets = await _context.CAT_Presupuestos.Include(x => x.Sucursales).Where(x => ids.Contains(x.Id)).ToListAsync();

            return budgets;
        }

        public async Task<bool> IsDuplicate(Budget catalog)
        {
            var isDuplicate = await _context.CAT_Presupuestos.AnyAsync(x => x.Id != catalog.Id && (x.Clave == catalog.Clave || x.Nombre == catalog.Nombre));

            return isDuplicate;
        }

        public async Task Create(Budget budget)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var branches = budget.Sucursales.ToList();

                budget.Sucursales = null;

                _context.CAT_Presupuestos.Add(budget);

                await _context.SaveChangesAsync();

                branches.ForEach(x => x.CostoFijoId = budget.Id);

                var config = new BulkConfig();
                config.SetSynchronizeFilter<BudgetBranch>(x => x.CostoFijoId == budget.Id);

                await _context.BulkInsertOrUpdateAsync(branches, config);

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task Update(Budget budget)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var branches = budget.Sucursales.ToList();

                budget.Sucursales = null;

                _context.CAT_Presupuestos.Update(budget);

                await _context.SaveChangesAsync();

                branches.ForEach(x => x.CostoFijoId = budget.Id);

                var config = new BulkConfig();
                config.SetSynchronizeFilter<BudgetBranch>(x => x.CostoFijoId == budget.Id);

                await _context.BulkInsertOrUpdateOrDeleteAsync(branches, config);

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
