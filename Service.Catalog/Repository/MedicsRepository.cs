using Identidad.Api.Infraestructure.Repository.IRepository;
using Identidad.Api.ViewModels.Medicos;
using Identidad.Api.ViewModels.Menu;
using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identidad.Api.Infraestructure.Repository
{
    public class MedicsRepository: IMedicsRepository
    {
        private readonly ApplicationDbContext _context;

        public MedicsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Medics> GetById(int Id)
        {
            return await _context.CAT_Medicos
            .Include(x => x.Clinicas)
            .ThenInclude(x => x.Clinica)
            .FirstOrDefaultAsync(x => x.IdMedico == Id);
                       

        }
        public async Task<List<Medics>> GetAll(string search)
        {
            var doctors = _context.CAT_Medicos.AsQueryable();
            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                doctors = doctors.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await doctors.ToListAsync();

        }

        public async Task Create(Medics doctors)
        {
            _context.CAT_Medicos.Add(doctors);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Medics doctors)
        {
            _context.CAT_Medicos.Update(doctors);

            await _context.SaveChangesAsync();
        }
    }
}
