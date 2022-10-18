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
                .Include(x => x.Estudios.Where(s => s.AreaId == areaId))
                .Where(x => branchesId.Contains(x.SucursalId) && x.FechaCreo.Date == date.Date && x.FechaCreo.TimeOfDay >= startTime.TimeOfDay && x.FechaCreo.TimeOfDay <= endTime.TimeOfDay)
                .Where(x => x.Estudios.Select(s => s.AreaId).Contains(areaId))
                .ToListAsync();

            return requests;
        }
    }
}
