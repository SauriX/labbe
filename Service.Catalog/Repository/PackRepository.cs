using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Packet;
using Service.Catalog.Domain.Study;
using Service.Catalog.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository
{
    public class PackRepository : IPackRepository
    {
        private readonly ApplicationDbContext _context;

        public PackRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Packet> GetById(int Id)
        {
            return await _context.CAT_Paquete
                .Include(x => x.Area)
                .ThenInclude(x => x.Departamento)
                .Include(x => x.Estudios)
                .ThenInclude(x => x.Estudio)
                .ThenInclude(x => x.Area)
                .ThenInclude(x => x.Departamento)
                .FirstOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<List<Packet>> GetAll(string search)
        {
            var Packs = _context.CAT_Paquete
                    .Include(x => x.Area)
                    .ThenInclude(x => x.Departamento)
                    .Include(x => x.Estudios)
                    .ThenInclude(x => x.Estudio)
                    .ThenInclude(x => x.Area)
                    .ThenInclude(x => x.Departamento)
                    .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                search = search.Trim().ToLower();
                Packs = Packs.Where(x => x.Clave.ToLower().Contains(search) || x.Nombre.ToLower().Contains(search));
            }

            return await Packs.ToListAsync();
        }

        public async Task<List<Packet>> GetActive()
        {
            var Packs = _context.CAT_Paquete
                    .Include(x => x.Area)
                    .ThenInclude(x => x.Departamento)
                    .Include(x => x.Estudios)
                    .ThenInclude(x => x.Estudio)
                    .ThenInclude(x => x.Area)
                    .ThenInclude(x => x.Departamento)
                    .Where(x => x.Activo);

            return await Packs.ToListAsync();
        }

        public async Task Create(Packet pack)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var studies = pack.Estudios.ToList();

                pack.Estudios = null;
                _context.CAT_Paquete.Add(pack);

                await _context.SaveChangesAsync();

                studies.ForEach(x => x.PacketId = pack.Id);

                var config = new BulkConfig();
                config.SetSynchronizeFilter<PacketStudy>(x => x.PacketId == pack.Id);

                await _context.BulkInsertOrUpdateOrDeleteAsync(studies, config);

                transaction.Commit();
            }
            catch (System.Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task Update(Packet pack)
        {
            var studies = pack.Estudios.ToList();
            pack.Estudios = null;
            _context.CAT_Paquete.Update(pack);

            var config = new BulkConfig();
            config.SetSynchronizeFilter<PacketStudy>(x => x.PacketId == pack.Id);

            await _context.BulkInsertOrUpdateOrDeleteAsync(studies, config);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsDuplicate(Packet pack)
        {
            var isDuplicate = await _context.CAT_Paquete.AnyAsync(x => x.Id != pack.Id && (x.Clave == pack.Clave || x.Nombre == pack.Nombre));

            return isDuplicate;
        }
    }
}
