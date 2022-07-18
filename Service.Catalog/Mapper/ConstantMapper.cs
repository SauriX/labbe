using Service.Catalog.Domain.Constant;
using Service.Catalog.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Catalog.Mapper
{
    public static class ConstantMapper
    {
        public static LocationDto ToLocationDto(this IEnumerable<Colony> model, string zipCode)
        {
            if (model == null || !model.Any()) return null;

            var city = model.FirstOrDefault().Ciudad;

            var stateName = city.Estado.Estado;
            var cityName = city.Ciudad;

            return new LocationDto
            {
                Estado = stateName,
                Ciudad = cityName,
                CodigoPostal = zipCode,
                Colonias = model.Select(x => new CatalogDto
                {
                    Id = x.Id,
                    Nombre = x.Colonia
                })
            };
        }
    }
}
