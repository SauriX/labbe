using Service.Catalog.Application.IApplication;
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
    public class AreaApplication : IAreaApplication
    {
        private readonly IAreaRepository _repository;

        public AreaApplication(IAreaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AreaListDto>> GetAll(string search = null)
        {
            var areas = await _repository.GetAll(search);

            return areas.ToAreaListDto();
        }

        public async Task<AreaFormDto> GetById(int id)
        {
            var area = await _repository.GetById(id);

            if (area == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return area.ToAreaFormDto();
        }

        public async Task<AreaListDto> Create(AreaFormDto area)
        {
            if (area.Id != 0)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            var newArea = area.ToModel();

            await _repository.Create(newArea);

            newArea = await _repository.GetById(newArea.Id);

            return newArea.ToAreaListDto();
        }
        public async Task<IEnumerable<CatalogListDto>> GetAreaByDépartament(int id)
        {
            var catalogs = await _repository.GetAreas(id);

            return catalogs.ToAreaDto();
        }
        public async Task<AreaListDto> Update(AreaFormDto area)
        {
            var existing = await _repository.GetById(area.Id);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedAgent = area.ToModel(existing);

            await _repository.Update(updatedAgent);

            updatedAgent = await _repository.GetById(updatedAgent.Id);

            return updatedAgent.ToAreaListDto();
        }
    }
}
