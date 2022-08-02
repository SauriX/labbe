using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.Sampling;
using Service.MedicalRecord.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository
{
    public class SamplingRepository:ISamplingRepository
    {
        private readonly ApplicationDbContext _context;

        public SamplingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Request>> GetAll(SamplingSearchDto search)
        {
            var citasLab = _context.CAT_Solicitud.AsQueryable();

            if (!string.IsNullOrEmpty(search.Procedencia) || search.Fecha.Length > 0)
            {
                citasLab = citasLab.Where(x => x.FechaCreo.Date <= search.Fecha[1].Date && x.FechaCreo.Date >= search.Fecha[0].Date);
            }


            return await citasLab.ToListAsync();
        }
    }
}
