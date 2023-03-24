using DocumentFormat.OpenXml.Bibliography;
using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository
{
    public class WorkListRepository : IWorkListRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkListRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Request>> GetWorkList(int areaId, List<Guid> branchesId, DateTime date, DateTime startTime, DateTime endTime)
        {
            var requests = await _context.CAT_Solicitud
                .Include(x => x.Expediente)
                .Include(x => x.Estudios.Where(s => s.AreaId == areaId)).ThenInclude(x => x.Estatus)
                .Where(x => branchesId.Contains(x.SucursalId) && x.FechaCreo.Date == date.Date && x.FechaCreo.TimeOfDay >= startTime.TimeOfDay && x.FechaCreo.TimeOfDay <= endTime.TimeOfDay)
                .Where(x => x.Estudios.Select(s => s.AreaId).Contains(areaId))
                .ToListAsync();

            return requests;
        }

        public async Task<List<Request>> GetMassiveWorkList(int? areaId, List<Guid> branchesId, List<DateTime> date)
        {
            var requests = _context.CAT_Solicitud
                .Include(x => x.Expediente)
                .Include(x => x.Sucursal)
                .Include(x => x.Estudios.Where(s => s.AreaId == areaId)).ThenInclude(x => x.Estatus)
                .AsQueryable();

            if (branchesId != null && branchesId.Count > 0)
            {
                requests = requests.Where(x => branchesId.Contains(x.SucursalId));
            }

            if(areaId != 0)
            {
                requests = requests.Where(x => x.Estudios.Select(s => s.AreaId).Contains(areaId));
            }

            return await requests.ToListAsync();
        }
    }
}
