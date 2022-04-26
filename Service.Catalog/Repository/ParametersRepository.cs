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
    public class ParametersRepository: IParameterRepository
    {
        private readonly ApplicationDbContext _context;

        public ParametersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Parameters>> GetAll(string search)
        {
            
            var parameters = _context.CAT_Parametro.Include(x => x.Area).ThenInclude(x=>x.Departamento).AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                parameters = parameters.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await parameters.ToListAsync();
        }
        
        public async Task<Parameters> GetById(string id)
        {
            var parameter = await _context.CAT_Parametro.FindAsync(Guid.Parse(id));

            return parameter;
        }


        public async Task Create(Parameters parameter)
        {
            try
            {
                _context.CAT_Parametro.Add(parameter);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
        }

        public async Task Update(Parameters parameter)
        {
      
                _context.CAT_Parametro.Update(parameter);

                await _context.SaveChangesAsync();
           

        }
        public async Task<bool> ValidateClaveNamne(string clave, string nombre)
        {
            var validate = _context.CAT_Parametro.Where(x => x.Clave == clave || x.Nombre == nombre).Count();

            if (validate == 0)
            {
                return false;
            }
            else
            {

                return true;
            }

        }
    }
}
