using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Branch;
using Service.Catalog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Service.Catalog.Dictionary.DuplicateCodes;

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
            var branchs = _context.CAT_Sucursal
                .Include(x => x.Colonia).ThenInclude(x => x.Ciudad).ThenInclude(x => x.Estado)
                .AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                branchs = branchs.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await branchs.ToListAsync();
        }

        public async Task<Branch> GetById(string id)
        {
            var branch = await _context.CAT_Sucursal
                .Include(x => x.Colonia).ThenInclude(x => x.Ciudad).ThenInclude(x => x.Estado)
                .Include(x => x.Departamentos).ThenInclude(x => x.Departamento)
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

            return branch;
        }

        public async Task<string> GetLastFolio(string ciudad)
        {
            var folio = await _context.CAT_Sucursal
                .Where(x => x.Ciudad == ciudad)
                .OrderByDescending(x => x.Clinicos)
                .Select(x => x.Clinicos)
                .FirstOrDefaultAsync();

            return folio;
        }

        public async Task<string> GetCodeRange(Guid id)
        {
            var branch = await _context.CAT_Sucursal.FindAsync(id);

            return branch?.Clinicos;
        }

        public async Task<(bool, string)> IsDuplicate(Branch branch)
        {
            var isDuplicate = false;
            var code = "";
            var isDuplicateName = await _context.CAT_Sucursal.AnyAsync(x => x.Id != branch.Id && x.Nombre == branch.Nombre);
            var isDuplicateClave = await _context.CAT_Sucursal.AnyAsync(x => x.Id != branch.Id && x.Clave == branch.Clave);
            var isDuplicateEmail = await _context.CAT_Sucursal.AnyAsync(x => x.Id != branch.Id && x.Correo == branch.Correo);
            if (isDuplicateName)
            {
                isDuplicate = isDuplicateName;
                code = DuplicateCodesEnum.Nombre.ToString();
            }
            if (isDuplicateClave)
            {
                isDuplicate = isDuplicateClave;
                code = DuplicateCodesEnum.Clave.ToString();
            }
            if (isDuplicateEmail)
            {
                isDuplicate = isDuplicateEmail;
                code = DuplicateCodesEnum.Email.ToString();
            }

            return (isDuplicate, code);
        }

        public async Task Create(Branch branch)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var departments = branch.Departamentos.ToList();

                branch.Departamentos = null;
                _context.CAT_Sucursal.Add(branch);

                await _context.SaveChangesAsync();

                departments.ForEach(x => x.SucursalId = branch.Id);

                var config = new BulkConfig();
                config.SetSynchronizeFilter<BranchDepartment>(x => x.SucursalId == branch.Id);

                await _context.BulkInsertOrUpdateOrDeleteAsync(departments, config);

                transaction.Commit();
            }
            catch (System.Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task Update(Branch branch)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var departments = branch.Departamentos.ToList();

                branch.Departamentos = null;

                _context.CAT_Sucursal.Update(branch);

                await _context.SaveChangesAsync();

                var config = new BulkConfig();
                config.SetSynchronizeFilter<BranchDepartment>(x => x.SucursalId == branch.Id);

                await _context.BulkInsertOrUpdateOrDeleteAsync(departments, config);

                transaction.Commit();
            }
            catch (System.Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<List<Domain.Constant.Colony>> GetColoniesByZipCode(short id)
        {
            var colonies = await _context.CAT_Colonia
                .Include(x => x.Ciudad).ThenInclude(x => x.Estado)
                .Where(x => x.CiudadId == id).ToListAsync();

            return colonies;
        }

        public async Task<List<Branch>> GetBranchByCity()
        {
            var colonies = await _context.CAT_Sucursal
                .Include(x => x.Colonia).ThenInclude(x => x.Ciudad).ThenInclude(x => x.Estado)
                .ToListAsync();

            return colonies;
        }

        public async Task<List<BranchFolioConfig>> GetConfigByState(byte stateId)
        {
            var config = await _context.CAT_Sucursal_Folio
                .Where(x => x.EstadoId == stateId)
                .OrderByDescending(x => x.ConsecutivoCiudad)
                .ToListAsync();

            return config;
        }

        public async Task<BranchFolioConfig> GetLastConfig()
        {
            var config = await _context.CAT_Sucursal_Folio.OrderByDescending(x => x.ConsecutivoEstado).FirstOrDefaultAsync();

            return config;
        }

        public async Task CreateConfig(BranchFolioConfig config)
        {
            _context.CAT_Sucursal_Folio.Add(config);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasMatriz(Branch branch)
        {
            var active = await _context.CAT_Sucursal.AnyAsync(x => x.Ciudad == branch.Ciudad && x.Matriz && x.Id != branch.Id);

            return active;
        }
    }
}
