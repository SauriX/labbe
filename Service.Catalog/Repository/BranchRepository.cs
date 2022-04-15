using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain;
using Service.Catalog.Domain.Branch;
using Service.Catalog.Dtos.Study;
using Service.Catalog.Mapper;
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

        public async Task<Branch> GetById(string id)
        {
            var branch = await _context.CAT_Sucursal.FindAsync(Guid.Parse(id));

            return branch;
        }

        public async Task Create(Branch branch)
        {
           await _context.CAT_Sucursal.AddAsync(branch);

            var cont =  await _context.SaveChangesAsync();
           
        }

        public async Task Update(Branch branch)
        {
            _context.CAT_Sucursal.Update(branch);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Domain.Constant.Colony>> GetColoniesByZipCode(short id)
        {
            var colonies = await _context.CAT_Colonia
                .Include(x => x.Ciudad).ThenInclude(x => x.Estado)
                .Where(x => x.CiudadId==id).ToListAsync();

            return colonies;
        }

        public async Task <IEnumerable<StudyListDto>> getservicios(string id) {
            List<Study> studys = new List<Study>();
            var servicios = _context.Relacion_Estudio_Sucursal.Where(x=>x.BranchId==Guid.Parse(id));
            foreach (var study in servicios) {
                var stud = _context.CAT_Estudio.Where(x=>x.Id==study.EstudioId);
                foreach (var estu in stud) {
                    
                    studys.Add(estu);
                }
                
            }
            var estudios = studys.ToStudyListDtos();
            return estudios;
        }
    }
}
