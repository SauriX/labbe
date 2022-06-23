using Service.Catalog.Domain.Constant;
using Service.Catalog.Dtos.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface ILocationApplication
    {
        Task<LocationDto> GetColoniesByZipCode(string zipCode);
        Task<List<City>> Getcity();
    }
}
