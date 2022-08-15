using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.Equipment;
using Service.Catalog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly ApplicationDbContext _context;
        public EquipmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Create(Equipos equipment)
        {
            _context.CAT_Equipos.Add(equipment);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Equipos>> GetAll(string search)
        {
            var equipment = _context.CAT_Equipos.AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                equipment = equipment.Where(x => x.Nombre.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await equipment.ToListAsync();
        }

        public async Task<Equipos> GetById(int Id)
        {
            var indication = await _context.CAT_Equipos
                 .FirstOrDefaultAsync(x => x.Id == Id);

            return indication;
        }

        public async Task<bool> IsDuplicate(Equipos equipment)
        {
            var isDuplicate = await _context.CAT_Equipos.AnyAsync(x => x.Id != equipment.Id || x.NombreLargo == equipment.NombreLargo);

            return isDuplicate;
        }

        public async Task Update(Equipos equipment)
        {
            _context.CAT_Equipos.Update(equipment);

            await _context.SaveChangesAsync();
        }
    }
}
