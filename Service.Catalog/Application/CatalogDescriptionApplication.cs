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
    public class CatalogDescriptionApplication<T> : ICatalogDescriptionApplication<T> where T : GenericCatalogDescription, new()
    {
        private readonly ICatalogRepository<T> _repository;

        public CatalogDescriptionApplication(ICatalogRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CatalogDescriptionListDto>> GetAll(string search = null)
        {
            var catalogs = await _repository.GetAll(search);

            return catalogs.ToCatalogDescriptionListDto();
        }

        public async Task<CatalogDescriptionFormDto> GetById(int id)
        {
            var catalog = await _repository.GetById(id);

            if (catalog == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return catalog.ToCatalogDescriptionFormDto();
        }

        public async Task<CatalogDescriptionListDto> Create(CatalogDescriptionFormDto catalog)
        {
            if (catalog.Id != 0)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            var newCatalog = catalog.ToModel<T>();

            await _repository.Crete(newCatalog);

            return newCatalog.ToCatalogDescriptionListDto();
        }

        public async Task<CatalogDescriptionListDto> Update(CatalogDescriptionFormDto catalog)
        {
            var existing = await _repository.GetById(catalog.Id);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedAgent = catalog.ToModel(existing);

            await _repository.Update(updatedAgent);

            return updatedAgent.ToCatalogDescriptionListDto();
        }
    }
}