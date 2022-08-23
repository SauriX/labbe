using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.Equipment;
using Service.Catalog.Domain.EquipmentMantain;
using Service.Catalog.Dtos.Equipmentmantain;
using Service.Catalog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository
{
    public class EquipmentMantainRepository: IEquipmentMantainRepository
    {
        private readonly ApplicationDbContext _context;
        public EquipmentMantainRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Mantain>> GetAll(MantainSearchDto search) {
            var equipos = _context.CAT_Mantenimiento_Equipo.Include(x => x.Equipo.Equipment).AsQueryable();
            equipos = equipos.Where(x=>x.EquipoId==search.IdEquipo); 
            if (search.Fecha.Length > 0 && search.Fecha[0].Date != DateTime.Now.Date && search.Fecha[1].Date != DateTime.Now.Date) {
                equipos = equipos.Where(x=> x.Fecha_Prog >= search.Fecha[0] && x.Fecha_Prog <= search.Fecha[1]);
            }

            if (string.IsNullOrEmpty(search.Clave)) {
                equipos = equipos.Where(x=>x.clave.Contains(search.Clave));
            }
            return equipos.ToList();
        }
        public async Task<Equipos> GetEquip(int Id) { 
        
        var equip = await _context.CAT_Equipos.Include(x => x.Valores).FirstOrDefaultAsync(x => x.Id==Id);
            return equip;
        }
        public async Task<Mantain> GetById(Guid Id) {
            var mantain = await _context.CAT_Mantenimiento_Equipo.Include(x => x.Equipo.Equipment).FirstOrDefaultAsync(x=>x.Id==Id);
            return mantain;
        }
        public async Task Create(Mantain mantain) {

            await _context.AddAsync(mantain);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Mantain mantain) {
             _context.Update(mantain);
            await _context.SaveChangesAsync();
        }
    }
}
