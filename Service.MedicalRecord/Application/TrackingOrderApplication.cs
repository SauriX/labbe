using Service.MedicalRecord.Dtos.TrackingOrder;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Repository.IRepository;
using System;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public class TrackingOrderApplication : ITrackingOrderApplication
    {
        private readonly ITrackingOrderRepository _repository; 

        public TrackingOrderApplication(ITrackingOrderRepository repository)
        {
            _repository = repository;
        }
        public Task<TrackingOrderDto> Create(TrackingOrderFormDto order)
        {
            throw new System.NotImplementedException();
        }

        public async Task<TrackingOrderDto> GetById(Guid Id)
        {
                var order = await _repository.GetById(Id);
                return order.ToTrackingOrderFormDto();
        }

        public Task<TrackingOrderDto> GetTrackingOrder(TrackingOrderFormDto order)
        {
            throw new NotImplementedException();
        }

        public Task<TrackingOrderDto> Update(TrackingOrderFormDto order)
        {
            throw new System.NotImplementedException();
        }

    }
}
