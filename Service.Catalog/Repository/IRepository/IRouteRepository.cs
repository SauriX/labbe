using Service.Catalog.Domain.Route;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface IRouteRepository
    {
        Task<List<Route>> GetAll(string search);
        Task<List<Route>> GetActive();
        Task<Route> GetById(Guid id);
        Task<bool> IsDuplicate(Route routes);
        Task<bool> IsDestinoIgualAlOrigen(Route routes);
        Task<bool> IsDestinoVacio(Route routes);
        Task Create(Route routes);
        Task Update(Route routes);
        Task<List<Route>> FindRoute(Route route);
    }
}
