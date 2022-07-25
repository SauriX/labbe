using Service.Catalog.Domain.Constant;
using Service.Catalog.Dtos.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface ILocationApplication
    {
        Task<LocationDto> GetColoniesByZipCode(string zipCode);
        Task<List<City>> Getcity();
    }
}
