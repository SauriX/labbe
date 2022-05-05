using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Parameter;
using Service.Catalog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository
{
    public class ParameterRepository : IParameterRepository
    {
        private readonly ApplicationDbContext _context;

        public ParameterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Parameter>> GetAll(string search)
        {
            var parameters = _context.CAT_Parametro.Include(x => x.Area).ThenInclude(x => x.Departamento).AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                parameters = parameters.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await parameters.ToListAsync();
        }

        public async Task<List<Parameter>> GetActive()
        {
            var parameters = await _context.CAT_Parametro.Where(x => x.Activo).Include(x => x.Area).ThenInclude(x => x.Departamento).ToListAsync();

            return parameters;
        }

        public async Task<Parameter> GetById(Guid id)
        {
            var parameter = await _context.CAT_Parametro
                .Include(x => x.Estudios).ThenInclude(x => x.Estudio)
                .Include(x => x.Area).ThenInclude(x => x.Departamento)
                .Include(x => x.Reactivo)
                .Include(x => x.FormatoImpresion)
                .FirstOrDefaultAsync(x => x.Id == id);

            return parameter;
        }

        public async Task<List<ParameterValue>> GetAllValues(Guid id)
        {
            var values = await _context.CAT_Tipo_Valor.Where(x => x.ParametroId == id).ToListAsync();

            return values;
        }

        public async Task<ParameterValue> GetValueById(Guid id)
        {
            var value = await _context.CAT_Tipo_Valor.FirstOrDefaultAsync(x => x.ParametroId == id);

            return value;
        }

        public Task<bool> IsDuplicate(Parameter reagent)
        {
            throw new NotImplementedException();
        }

        public async Task Create(Parameter parameter)
        {
            _context.CAT_Parametro.Add(parameter);

            await _context.SaveChangesAsync();
        }

        public async Task AddValue(ParameterValue value)
        {
            _context.CAT_Tipo_Valor.Add(value);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Parameter parameter)
        {
            _context.CAT_Parametro.Update(parameter);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateValue(ParameterValue value)
        {
            _context.CAT_Tipo_Valor.Update(value);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteValue(Guid id)
        {
            _context.CAT_Tipo_Valor.Where(p => p.ParametroId == id).ToList().ForEach(p => _context.CAT_Tipo_Valor.Remove(p));

            await _context.SaveChangesAsync();
        }
    }
}
