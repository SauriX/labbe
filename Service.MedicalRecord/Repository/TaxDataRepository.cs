using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Domain.TaxData;
using Service.MedicalRecord.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository
{
    public class TaxDataRepository: ITaxDataRepository
    {
        private readonly ApplicationDbContext _context;
        public TaxDataRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<TaxData>> GetAll(string search)
        {
            var taxData = _context.CAT_Datos_Fiscales.AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                taxData = taxData.Where(x => x.RFC.ToLower().Contains(search) || x.RazonSocial.ToLower().Contains(search));
            }

            return await taxData.ToListAsync();
        }

        public async Task<List<TaxData>> GetActive()
        {
            var taxData = await _context.CAT_Datos_Fiscales.Where(x => x.Activo).ToListAsync();

            return taxData;
        }

        public async Task<TaxData> GetById(Guid id)
        {
            var taxData = await _context.CAT_Datos_Fiscales.FindAsync(id);

            return taxData;
        }


        public async Task Create(TaxData taxData)
        {
            _context.CAT_Datos_Fiscales.Add(taxData);

            await _context.SaveChangesAsync();
        }

        public async Task Update(TaxData taxData)
        {
            _context.CAT_Datos_Fiscales.Update(taxData);

            await _context.SaveChangesAsync();
        }
    }
}
