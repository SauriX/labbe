using Service.Catalog.Application.IApplication;
using Service.Catalog.Dtos.Constant;
using Service.Catalog.Mapper;
using Service.Catalog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Application
{
    public class LocationApplication : ILocationApplication
    {
        private readonly ILocationRepository _repository;

        public LocationApplication(ILocationRepository repository)
        {
            _repository = repository;
        }

        public async Task<LocationDto> GetColoniesByZipCode(string zipCode)
        {
            var colonies = await _repository.GetColoniesByZipCode(zipCode.Trim());

            return colonies.ToLocationDto(zipCode);
        }
    }
}
