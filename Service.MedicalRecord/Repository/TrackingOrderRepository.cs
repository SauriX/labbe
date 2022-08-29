using Microsoft.EntityFrameworkCore;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Domain.TrackingOrder;
using Service.MedicalRecord.Dtos.TrackingOrder;
using Service.MedicalRecord.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository
{
    public class TrackingOrderRepository : ITrackingOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public TrackingOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<TrackingOrder> GetById(Guid id)
        {
            throw new System.NotImplementedException();
        }
        public Task Create(TrackingOrder reagent)
        {
            throw new System.NotImplementedException();
        }


        public Task Update(TrackingOrder reagent)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Domain.Request.RequestStudy>> FindEstudios(List<int> estudios)
        {
            var listaEstudio = _context.Relacion_Solicitud_Estudio
                .Include(x => x.Solicitud).ThenInclude(x => x.Expediente)
                .Include(x => x.Tapon)
                .AsQueryable();

            listaEstudio = listaEstudio.Where(x => estudios.Contains(x.EstudioId));

            return await listaEstudio.ToListAsync();
        }
    }
}
