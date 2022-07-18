using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Repository.IRepository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository
{
    public class RequestRepository : IRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public RequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> GetLastCode(Guid branchId, string date)
        {
            var lastRequest = await _context.CAT_Solicitud
                .OrderBy(x => x.FechaCreo)
                .LastOrDefaultAsync(x => x.SucursalId == branchId && x.Clave.EndsWith(date));

            return lastRequest?.Clave;
        }

        public async Task Create(Request request)
        {
            _context.CAT_Solicitud.Add(request);

            await _context.SaveChangesAsync();
        }

        public Task<Request> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Request request)
        {
            throw new NotImplementedException();
        }
    }
}
