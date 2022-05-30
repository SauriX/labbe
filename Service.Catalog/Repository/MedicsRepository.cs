using EFCore.BulkExtensions;
using Identidad.Api.Infraestructure.Repository.IRepository;
using Identidad.Api.Model.Medicos;
using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Medics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository
{
    public class MedicsRepository : IMedicsRepository
    {
        private readonly ApplicationDbContext _context;

        public MedicsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Medics> GetById(Guid Id)
        {
            return await _context.CAT_Medicos
                .Include(x => x.Clinicas)
                .ThenInclude(x => x.Clinica)
                .Include(x => x.Colonia).ThenInclude(x => x.Ciudad).ThenInclude(x => x.Estado)
                .Include(x => x.Especialidad)
                .FirstOrDefaultAsync(x => x.IdMedico == Id);
        }

        public async Task<List<Medics>> GetAll(string search)
        {
            var doctors = _context.CAT_Medicos
                .Include(x => x.Colonia).ThenInclude(x => x.Ciudad).ThenInclude(x => x.Estado)
                .Include(x => x.Especialidad)
                .AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                doctors = doctors.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await doctors.ToListAsync();
        }

        public async Task<Medics> GetByCode(string code)
        {
            return await _context.CAT_Medicos.FirstOrDefaultAsync(x => x.Clave == code);
        }

        public async Task Create(Medics doctors)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var clinics = doctors.Clinicas.ToList();

                doctors.Clinicas = null;
                _context.CAT_Medicos.Add(doctors);

                await _context.SaveChangesAsync();

                clinics.ForEach(x => x.MedicoId = doctors.IdMedico);

                var config = new BulkConfig();
                config.SetSynchronizeFilter<MedicClinic>(x => x.MedicoId == doctors.IdMedico);

                await _context.BulkInsertOrUpdateOrDeleteAsync(clinics, config);

                transaction.Commit();
            }
            catch (System.Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task Update(Medics doctors)
        {
            var clinics = doctors.Clinicas.ToList();

            doctors.Clinicas = null;
            _context.CAT_Medicos.Update(doctors);

            var config = new BulkConfig();
            config.SetSynchronizeFilter<MedicClinic>(x => x.MedicoId == doctors.IdMedico);

            await _context.BulkInsertOrUpdateOrDeleteAsync(clinics, config);

            await _context.SaveChangesAsync();
        }


    }
}
