using Service.Catalog.Domain.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface ILocationRepository
    {
        Task<List<Colony>> GetColoniesByZipCode(string zipCode);
        Task<List<City>> GetCities();
    }
}
