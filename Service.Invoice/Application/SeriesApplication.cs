using Service.Billing.Application.IApplication;
using Service.Billing.Dtos.Series;
using Service.Billing.Mapper;
using Service.Billing.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Billing.Application
{
    public class SeriesApplication : ISeriesApplication
    {
        private readonly ISeriesRepository _repository;

        public SeriesApplication(ISeriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SeriesListDto>> GetByBranch(Guid branchId, byte type)
        {
            var series = await _repository.GetByBranch(branchId, type);

            return series.ToSeriesListDto();
        }
    }
}
