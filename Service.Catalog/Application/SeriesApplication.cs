using Service.Catalog.Application.IApplication;
using Service.Catalog.Dtos.Series;
using Service.Catalog.Mapper;
using Service.Catalog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application
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
