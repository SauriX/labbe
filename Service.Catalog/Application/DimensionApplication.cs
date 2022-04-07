using Service.Catalog.Application.IApplication;
using Service.Catalog.Domain.Catalog;
using Service.Catalog.Dtos.Catalog;
using Service.Catalog.Mapper;
using Service.Catalog.Repository.IRepository;
using Shared.Dictionary;
using Shared.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Service.Catalog.Application
{
    public class DimensionApplication : IDimensionApplication
    {
        private readonly IDimensionRepository _repository;

        public DimensionApplication(IDimensionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DimensionListDto>> GetAll(string search = null)
        {
            var areas = await _repository.GetAll(search);

            return areas.ToDimensionListDto();
        }

        public async Task<DimensionFormDto> GetById(int id)
        {
            var area = await _repository.GetById(id);

            if (area == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return area.ToDimensionFormDto();
        }

        public async Task<DimensionListDto> Create(DimensionFormDto area)
        {
            if (area.Id != 0)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            var newDimension = area.ToModel();

            await _repository.Create(newDimension);

            return newDimension.ToDimensionListDto();
        }

        public async Task<DimensionListDto> Update(DimensionFormDto area)
        {
            var existing = await _repository.GetById(area.Id);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedAgent = area.ToModel(existing);

            await _repository.Update(updatedAgent);

            return updatedAgent.ToDimensionListDto();
        }
    }
}
