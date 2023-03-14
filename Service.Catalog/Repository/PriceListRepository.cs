using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Branch;
using Service.Catalog.Domain.Price;
using Service.Catalog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository
{
    public class PriceListRepository : IPriceListRepository
    {
        private readonly ApplicationDbContext _context;

        public PriceListRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PriceList>> GetAll(string search)
        {
            var prices = _context.CAT_ListaPrecio.AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                prices = prices.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await prices.ToListAsync();
        }

        public async Task<PriceList_Study> GetPriceStudyById(int studyId, Guid branchId, Guid companyId)
        {
            var prices = _context.Relacion_ListaP_Estudio
                .Include(x => x.Estudio.Parameters).ThenInclude(x => x.Parametro.Area.Departamento)
                .Include(x => x.Estudio.Indications).ThenInclude(x => x.Indicacion)
                .Include(x => x.Estudio).ThenInclude(x => x.Etiquetas).ThenInclude(x => x.Etiqueta)
                .Include(x => x.Estudio).ThenInclude(x => x.Maquilador)
                .Include(x => x.PrecioLista)
                .Where(x => x.EstudioId == studyId);

            var companyPrice = await
                (from p in prices
                 join cp in _context.CAT_ListaP_Compañia.Where(x => x.CompañiaId == companyId) on p.PrecioListaId equals cp.PrecioListaId
                 join dp in _context.CAT_ListaP_Sucursal.Where(x => x.SucursalId == branchId) on p.PrecioListaId equals dp.PrecioListaId
                 where cp.Activo && dp.Activo
                 select p).FirstOrDefaultAsync();

            return companyPrice;
        }

        public async Task<List<PriceList_Study>> GetPriceStudyById(Guid priceList, IEnumerable<int> studyId)
        {
            var prices = await _context.Relacion_ListaP_Estudio
                .Include(x => x.Estudio.Parameters).ThenInclude(x => x.Parametro.Area.Departamento)
                .Include(x => x.Estudio.Indications).ThenInclude(x => x.Indicacion)
                .Include(x => x.Estudio.Etiquetas).ThenInclude(x => x.Etiqueta)
                .Where(x => x.PrecioListaId == priceList && studyId.Contains(x.EstudioId))
                .ToListAsync();

            return prices;
        }

        public async Task<PriceList_Packet> GetPricePackById(int packId, Guid branchId, Guid companyId)
        {
            var prices = _context.Relacion_ListaP_Paquete
                .Include(x => x.Paquete.Estudios).ThenInclude(x => x.Estudio.Etiquetas).ThenInclude(x => x.Etiqueta)
                .Include(x => x.Paquete.Estudios).ThenInclude(x => x.Estudio.Parameters).ThenInclude(x => x.Parametro.Area.Departamento)
                .Include(x => x.Paquete.Estudios).ThenInclude(x => x.Estudio.Indications).ThenInclude(x => x.Indicacion)
                .Include(x => x.PrecioLista)
                .Where(x => x.PaqueteId == packId);

            var companyPrice = await
                (from p in prices
                 join cp in _context.CAT_ListaP_Compañia.Where(x => x.CompañiaId == companyId) on p.PrecioListaId equals cp.PrecioListaId
                 join dp in _context.CAT_ListaP_Sucursal.Where(x => x.SucursalId == branchId) on p.PrecioListaId equals dp.PrecioListaId
                 where cp.Activo && dp.Activo
                 select p).FirstOrDefaultAsync();

            return companyPrice;
        }

        public async Task<PriceList> GetById(Guid Id)
        {
            var indication = await _context.CAT_ListaPrecio
                .Include(x => x.Estudios).ThenInclude(x => x.Estudio).ThenInclude(x => x.Area).ThenInclude(x => x.Departamento)
                //.Include(x => x.Estudios).ThenInclude(x => x.Estudio).ThenInclude(x => x.Tapon)
                .Include(x => x.Sucursales).ThenInclude(x => x.Sucursal)
                .Include(x => x.Compañia).ThenInclude(x => x.Compañia)
                .Include(x => x.Medicos).ThenInclude(x => x.Medico)
                .Include(x => x.Paquetes).ThenInclude(x => x.Paquete.Estudios).ThenInclude(x => x.Estudio.Area.Departamento)
                .Include(x => x.Paquetes).ThenInclude(x => x.Paquete.Area.Departamento)
                .FirstOrDefaultAsync(x => x.Id == Id);

            return indication;
        }
        public async Task<List<PriceList_Study>> GetStudiesById(Guid Id)
        {

            return await _context.Relacion_ListaP_Estudio
                .Include(x => x.Estudio).ThenInclude(x => x.Area).ThenInclude(x => x.Departamento)
                .Include(x => x.Estudio).ThenInclude(x => x.Etiquetas).ThenInclude(x => x.Etiqueta)
                .Where(x => x.PrecioListaId == Id)
                .OrderBy(x => x.PrecioListaId)
                .ToListAsync();

        }
        public async Task<List<PriceList>> GetActive()
        {
            var prices = await _context.CAT_ListaPrecio.Include(x => x.Estudios).ThenInclude(x => x.Estudio).ThenInclude(x => x.Area).ThenInclude(x => x.Departamento)
                    .Include(x => x.Estudios).ThenInclude(x => x.Estudio).ThenInclude(x => x.Etiquetas).ThenInclude(x => x.Etiqueta)
                    .Include(x => x.Sucursales).ThenInclude(x => x.Sucursal)
                    .Include(x => x.Compañia).ThenInclude(x => x.Compañia)
                    .Include(x => x.Medicos).ThenInclude(x => x.Medico).Where(x => x.Activo).ToListAsync();

            return prices;
        }

        public async Task<List<PriceList>> GetOptions()
        {
            var prices = await _context.CAT_ListaPrecio.Where(x => x.Activo).OrderBy(x => x.Nombre).ToListAsync();

            return prices;
        }

        public async Task<List<Branch>> GetBranchesByPriceListId(Guid id)
        {
            var branches = await _context.CAT_ListaP_Sucursal
                .Include(x => x.Sucursal)
                .Where(x => x.PrecioListaId == id && x.Activo)
                .Select(x => x.Sucursal)
                .OrderBy(x => x.Nombre)
                .ToListAsync();

            return branches;
        }

        public async Task<PriceList> GetStudiesAndPacks(Guid priceListId)
        {
            var priceList = await _context.CAT_ListaPrecio
                .Include(x => x.Estudios.Where(x => x.Precio > 0)).ThenInclude(x => x.Estudio.Area)
                .Include(x => x.Paquetes.Where(x => x.Precio > 0)).ThenInclude(x => x.Paquete)
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.Id == priceListId);

            return priceList;
        }

        public async Task<bool> IsDuplicate(PriceList price)
        {
            var isDuplicate = await _context.CAT_ListaPrecio.AnyAsync(x => x.Id != price.Id && (x.Clave == price.Clave || x.Nombre == price.Nombre));

            return isDuplicate;
        }

        public async Task Create(PriceList price)
        {
            _context.CAT_ListaPrecio.Add(price);

            await _context.SaveChangesAsync();
        }

        public async Task Update(PriceList price)
        {
            var branches = price.Sucursales.ToList();
            var packs = price.Paquetes.ToList();
            var studies = price.Estudios.ToList();
            var medic = price.Medicos.ToList();
            var company = price.Compañia.ToList();
            price.Sucursales = null;
            price.Paquetes = null;
            price.Estudios = null;
            price.Medicos = null;
            price.Compañia = null;
            _context.CAT_ListaPrecio.Update(price);
            var config = new BulkConfig();
            config.SetSynchronizeFilter<Price_Branch>(x => x.PrecioListaId == price.Id);
            branches.ForEach(x => x.PrecioListaId = price.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(branches, config);

            config.SetSynchronizeFilter<PriceList_Packet>(x => x.PrecioListaId == price.Id);
            packs.ForEach(x => x.PrecioListaId = price.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(packs, config);

            config.SetSynchronizeFilter<PriceList_Study>(x => x.PrecioListaId == price.Id);
            studies.ForEach(x => x.PrecioListaId = price.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(studies, config);

            config.SetSynchronizeFilter<Price_Medics>(x => x.PrecioListaId == price.Id);
            medic.ForEach(x => x.PrecioListaId = price.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(medic, config);
            await _context.SaveChangesAsync();

            config.SetSynchronizeFilter<Price_Company>(x => x.PrecioListaId == price.Id);
            company.ForEach(x => x.PrecioListaId = price.Id);
            await _context.BulkInsertOrUpdateOrDeleteAsync(company, config);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Price_Company>> GetAllCompany(Guid companyId)
        {
            var asignado = await
                (from company in _context.CAT_Compañia
                 join priceList in _context.CAT_ListaP_Compañia.Include(x => x.PrecioLista) on company.Id equals priceList.CompañiaId into ljPriceList
                 from pList in ljPriceList.DefaultIfEmpty()
                 select new { company, pList })
                 .Select(x => new Price_Company
                 {
                     Compañia = x.company,
                     PrecioLista = x.pList == null ? null : x.pList.PrecioLista,
                 })
                .ToListAsync();

            return asignado;
        }
        public async Task<List<Price_Branch>> GetAllBranch(Guid branchId)
        {
            var asignado = await
                (from branch in _context.CAT_Sucursal
                 join priceList in _context.CAT_ListaP_Sucursal.Include(x => x.PrecioLista) on branch.Id equals priceList.SucursalId into ljPriceList
                 from pList in ljPriceList.DefaultIfEmpty()
                 select new { branch, pList })
                 .Select(x => new Price_Branch
                 {
                     Sucursal = x.branch,
                     PrecioLista = x.pList == null ? null : x.pList.PrecioLista,
                     SucursalId = x.branch.Id
                 })
                .ToListAsync();

            return asignado;
        }

        public async Task<List<Price_Medics>> GetAllMedics(Guid medicsId)
        {
            var asignado = await
                (from medics in _context.CAT_Medicos
                 join priceList in _context.CAT_ListaP_Medicos.Include(x => x.PrecioLista) on medics.IdMedico equals priceList.MedicoId into ljPriceList
                 from pList in ljPriceList.DefaultIfEmpty()
                 select new { medics, pList })
                 .Select(x => new Price_Medics
                 {
                     MedicoId = x.medics.IdMedico,
                     Medico = x.medics,
                     PrecioLista = x.pList == null ? null : x.pList.PrecioLista,
                 })
                .ToListAsync();

            return asignado;
        }

        public async Task<bool> DuplicateSMC(PriceList price)
        {
            var company = false;
            var branch = false;
            // var precios = _context.CAT_ListaPrecio.Include(x=>x.Sucursales).Include(x=>x.Compañia).AsQueryable();

            // var coincidencias = await precios.AnyAsync(x => x.Sucursales.SequenceEqual(price.Sucursales) && x.Compañia.SequenceEqual(price.Compañia));
            foreach (var compañia in price.Compañia)
            {
                var compañias = await _context.CAT_ListaP_Compañia.AnyAsync(x => x.CompañiaId == compañia.CompañiaId && x.PrecioListaId != price.Id);
                if (compañias)
                {
                    company = true;
                    break;
                }
            }
            foreach (var sucursal in price.Sucursales)
            {
                var sucursales = await _context.CAT_ListaP_Sucursal.AnyAsync(x => x.SucursalId == sucursal.SucursalId && x.PrecioListaId != price.Id);
                if (sucursales)
                {
                    branch = true;
                    break;
                }
            }
            if (!branch && !company)
            {
                return false;
            }
            return true;
        }
    }
}
