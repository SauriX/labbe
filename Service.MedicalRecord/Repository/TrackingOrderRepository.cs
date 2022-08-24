using Service.MedicalRecord.Context;
using Service.MedicalRecord.Domain.TrackingOrder;
using Service.MedicalRecord.Repository.IRepository;
using System;
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
    }
}
