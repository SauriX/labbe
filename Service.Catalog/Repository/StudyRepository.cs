using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Indication;
using Service.Catalog.Domain.Parameter;
using Service.Catalog.Domain.Study;
using Service.Catalog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository
{
    public class StudyRepository : IStudyRepository
    {

        private readonly ApplicationDbContext _context;

        public StudyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Study>> GetAll(string search)
        {
            var studyes = _context.CAT_Estudio
                    .Include(x => x.Area)
                    .ThenInclude(x => x.Departamento)
                    .Include(x => x.Maquilador)
                    .Include(x => x.Metodo)
                    .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                search = search.Trim().ToLower();
                studyes = studyes.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await studyes.ToListAsync();
        }

        public async Task<List<Study>> GetStudyList(string search)
        {
            var studyes = _context.CAT_Estudio
                    .Include(x => x.Area)
                    .ThenInclude(x => x.Departamento)
                    .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                search = search.Trim().ToLower();
                studyes = studyes.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await studyes.ToListAsync();
        }

        public async Task<List<Study>> GetActive()
        {
            var studyes = _context.CAT_Estudio
                .Include(x => x.Area)
                .ThenInclude(x => x.Departamento)
                .Include(x => x.Maquilador)
                .Include(x => x.Metodo)
                .Where(x => x.Activo)
                .OrderBy(x => x.Clave).ThenBy(x => x.Nombre);

            return await studyes.ToListAsync();
        }

        public async Task<Study> GetById(int id)
        {
            var reagent = await _context.CAT_Estudio
                .Include(x => x.WorkLists).ThenInclude(x => x.WorkList)
                .Include(x => x.Parameters).ThenInclude(x => x.Parametro).ThenInclude(x => x.Area).ThenInclude(x => x.Departamento)
                .Include(x => x.Parameters).ThenInclude(x => x.Parametro).ThenInclude(x => x.Unidad)
                .Include(x => x.Parameters).ThenInclude(x => x.Parametro).ThenInclude(x => x.TipoValores)
                .Include(x => x.Indications).ThenInclude(x => x.Indicacion)
                .Include(x => x.Reagents).ThenInclude(x => x.Reagent)
                .Include(x => x.Packets).ThenInclude(x => x.Packet)
                .Include(x => x.Area).ThenInclude(x => x.Departamento)
                .Include(x => x.Maquilador)
                .Include(x => x.SampleType)
                .Include(x => x.Metodo)
                .FirstOrDefaultAsync(x => x.Id == id);

            return reagent;
        }

        public async Task<Study> FindAsync(int id)
        {
            var reagent = await _context.CAT_Estudio
                .Include(x => x.Etiquetas).ThenInclude(x=>x.Etiqueta)
                .Include(x => x.SampleType)
                .FirstOrDefaultAsync(x => x.Id == id);

            return reagent;
        }

        public async Task<int> GetIdByCode(string code)
        {
            var study = await _context.CAT_Estudio.FirstOrDefaultAsync(x => x.Clave == code);

            return study?.Id ?? 0;
        }

        public async Task<List<Study>> GetByIds(List<int> ids)
        {
            var studies = await _context.CAT_Estudio
                .Include(x => x.Parameters).ThenInclude(x => x.Parametro).ThenInclude(x => x.Area).ThenInclude(x => x.Departamento)
                .Include(x => x.Parameters).ThenInclude(x => x.Parametro).ThenInclude(x => x.Unidad)
                .Include(x => x.Parameters).ThenInclude(x => x.Parametro).ThenInclude(x => x.TipoValores)
                .Include(x => x.Indications).ThenInclude(x => x.Indicacion)
                .Include(x => x.Etiquetas).ThenInclude(x => x.Etiqueta)
                .Include(x => x.Metodo)
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();

            return studies;
        }

        public async Task Create(Study study)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var reagents = study.Reagents.ToList();
                var workList = study.WorkLists.ToList();
                var parameters = study.Parameters.ToList();
                var indications = study.Indications.ToList();
                study.Reagents = null;
                study.WorkLists = null;
                study.Parameters = null;
                study.Indications = null;
                _context.CAT_Estudio.Add(study);
                await _context.SaveChangesAsync();
                var config = new BulkConfig();
                config.SetSynchronizeFilter<Domain.Study.ReagentStudy>(x => x.EstudioId == study.Id);
                reagents.ForEach(x => x.EstudioId = study.Id);
                await _context.BulkInsertOrUpdateOrDeleteAsync(reagents, config);

                config.SetSynchronizeFilter<Domain.Study.WorkListStudy>(x => x.EstudioId == study.Id);
                workList.ForEach(x => x.EstudioId = study.Id);
                await _context.BulkInsertOrUpdateOrDeleteAsync(workList, config);

                config.SetSynchronizeFilter<ParameterStudy>(x => x.EstudioId == study.Id);
                parameters.ForEach(x => x.EstudioId = study.Id);
                await _context.BulkInsertOrUpdateOrDeleteAsync(parameters, config);

                config.SetSynchronizeFilter<IndicationStudy>(x => x.EstudioId == study.Id);
                indications.ForEach(x => x.EstudioId = study.Id);
                await _context.BulkInsertOrUpdateOrDeleteAsync(indications, config);
                transaction.Commit();


            }
            catch (System.Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        /*public async Task Update(Study study)
        {
            var reagents = study.Reagents.ToList();
           
            var parameters = study.Parameters.ToList();
            var indications = study.Indications.ToList();
            study.Reagents = null;
            study.WorkLists = null;
            study.Parameters = null;
            study.Indications = null;
            _context.CAT_Estudio.Update(study);

            var config = new BulkConfig();

            config.SetSynchronizeFilter<Domain.Study.ReagentStudy>(x => x.EstudioId == study.Id);
            reagents.ForEach(x => x.EstudioId = study.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(reagents, config);



            config.SetSynchronizeFilter<ParameterStudy>(x => x.EstudioId == study.Id);
            parameters.ForEach(x => x.EstudioId = study.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(parameters, config);

            config.SetSynchronizeFilter<IndicationStudy>(x => x.EstudioId == study.Id);
            indications.ForEach(x => x.EstudioId = study.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(indications, config);

            await _context.SaveChangesAsync();
        }*/

        public async Task Update(Study study)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var reagents = study.Reagents.ToList();
                var parameters = study.Parameters.ToList();
                var indications = study.Indications.ToList();

                study.Reagents = null;
                study.WorkLists = null;
                study.Parameters = null;
                study.Indications = null;

                _context.CAT_Estudio.Update(study);

                await _context.SaveChangesAsync();

                var config = new BulkConfig();
                config.SetSynchronizeFilter<ReagentStudy>(x => x.EstudioId == study.Id);
                await _context.BulkInsertOrUpdateOrDeleteAsync(reagents, config);

                config.SetSynchronizeFilter<ParameterStudy>(x => x.EstudioId == study.Id);
                await _context.BulkInsertOrUpdateOrDeleteAsync(parameters, config);

                config.SetSynchronizeFilter<IndicationStudy>(x => x.EstudioId == study.Id);
                await _context.BulkInsertOrUpdateOrDeleteAsync(indications, config);

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<bool> ValidateClaveNamne(string clave, string nombre, int id, int orden)
        {
            return await _context.CAT_Estudio.AnyAsync(x => x.Clave == clave || x.Nombre == nombre || x.Orden == orden && x.Id != id);
        }
    }
}
