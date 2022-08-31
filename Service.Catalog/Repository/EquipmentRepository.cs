using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.Equipment;
using Service.Catalog.Repository.IRepository;
using System;
using System.Collections.Generic;
using Service.Catalog.Mapper;
using System.Linq;
using System.Threading.Tasks;
using Service.Catalog.Dtos.Equipment;
using EFCore.BulkExtensions;

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
            //var newEquipo = equipment.ToModel();
            //await _equipmentRepository.Create(newEquipo);
            using var transaction = _context.Database.BeginTransaction();
            try
            {

                var valores = equipment.Valores.ToList();

                equipment.Valores = null;

                _context.CAT_Equipos.Add(equipment);

                await _context.SaveChangesAsync();

                valores.ForEach(valor => { valor.EquipmentId = equipment.Id; valor.EquipmentBranchId = Guid.NewGuid(); });

                var config = new BulkConfig();

                config.SetSynchronizeFilter<EquipmentBranch>(x => x.EquipmentId == equipment.Id);

                await _context.BulkInsertOrUpdateOrDeleteAsync(valores, config);

                transaction.Commit();
            }
            catch (System.Exception)
            {
                transaction.Rollback();
                throw;
            }






            //Equipment equipo = new Equipment();
            //equipo.NombreLargo;
            ////...

            //var equipo_guardado = _context.CAT_Equipos.Add(equipo);
            //for(var valor of equipment.Valores)
            //{
            //    valor.Equipoid = equipo_guardado.id
            //    _context.Relacion_Equipo_Sucursal.Add(valor);
            //}

            //await _context.SaveChangesAsync();
        }

        public async Task<List<Equipos>> GetAll(string search)
        {
            var equipment = _context.CAT_Equipos.Include(x => x.Valores).ThenInclude(x => x.Sucursal).AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                equipment = equipment.Where(x => x.Nombre.ToLower().Contains(search) || x.Clave.ToLower().Contains(search));
            }

            return await equipment.ToListAsync();
        }

        public async Task<Equipos> GetById(int Id)
        {
            var equipment = await _context.CAT_Equipos.Include(x => x.Valores).ThenInclude(x => x.Sucursal)
                 .FirstOrDefaultAsync(x => x.Id == Id);

            return equipment;
        }

        public async Task<bool> IsDuplicate(Equipos equipment)
        {
            var isDuplicate = await _context.CAT_Equipos.AnyAsync((x => x.Id != equipment.Id && x.Nombre == equipment.Nombre));

            return isDuplicate;
        }

        public async Task Update(Equipos equipment)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var values = equipment.Valores.ToList();

                equipment.Valores = null;

                _context.CAT_Equipos.Update(equipment);

                await _context.SaveChangesAsync();

                values.ForEach(valor => { valor.EquipmentBranchId = Guid.NewGuid(); });

                var config = new BulkConfig();

                config.SetSynchronizeFilter<EquipmentBranch>(x => x.EquipmentId == equipment.Id);

                await _context.BulkInsertOrUpdateOrDeleteAsync(values, config);

                transaction.Commit();
            }
            catch (System.Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
