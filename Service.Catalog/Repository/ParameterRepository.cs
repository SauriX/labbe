﻿using EFCore.BulkExtensions;
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
                .Include(x => x.Reactivos)
                .Include(x => x.FormatoImpresion)
                .FirstOrDefaultAsync(x => x.Id == id);

            return parameter;
        }

        public async Task<List<ParameterValue>> GetAllValues(Guid id, string type)
        {
            var values = _context.CAT_Tipo_Valor.Where(x => x.ParametroId == id);

            if (type == "4")
            {
                return await values.Where(x => x.Nombre == "hombre" || x.Nombre == "mujer").ToListAsync();
            }

            return await values.Where(x => x.Nombre == type).ToListAsync();
        }

        public async Task<ParameterValue> GetValueById(Guid id)
        {
            var value = await _context.CAT_Tipo_Valor.FirstOrDefaultAsync(x => x.ParametroId == id);

            return value;
        }

        public async Task<bool> IsDuplicate(Parameter parameter)
        {
            var isDuplicate = await _context.CAT_Parametro.AnyAsync(x => x.Id != parameter.Id && (x.Clave == parameter.Clave || x.Nombre == parameter.Nombre));

            return isDuplicate;
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

        public async Task AddValues(List<ParameterValue> value, string id)
        {
            var config = new BulkConfig();
            config.SetSynchronizeFilter<ParameterValue>(x => x.ParametroId == Guid.Parse(id));
            await _context.BulkInsertOrUpdateOrDeleteAsync(value, config);
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


    }
}
