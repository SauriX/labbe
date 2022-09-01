using Service.Catalog.Domain.Route;
using Service.Catalog.Dtos.Route;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Application.IApplication
{
    public interface IRouteApplication
    {
        Task<IEnumerable<RouteListDto>> GetAll(string search);
        Task<IEnumerable<RouteListDto>> GetActive();
        Task<RouteFormDto> GetById(string id);
        Task<RouteListDto> Create(RouteFormDto routes);
        Task<RouteListDto> Update(RouteFormDto routes);
        Task<(byte[] file, string fileName)> ExportList(string search);
        Task<(byte[] file, string fileName)> ExportForm(string id);
        Task<IEnumerable<RouteFormDto>> FindRoutes(RouteFormDto routeForm);
    }
}
