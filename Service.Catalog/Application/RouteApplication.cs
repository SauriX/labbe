using ClosedXML.Excel;
using ClosedXML.Report;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dictionary;
using Service.Catalog.Domain.Route;
using Service.Catalog.Dtos.Route;
using Service.Catalog.Mapper;
using Service.Catalog.Repository.IRepository;
using Shared.Dictionary;
using Shared.Error;
using Shared.Extensions;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Service.Catalog.Application
{
    public class RouteApplication : IRouteApplication
    {
        private readonly IRouteRepository _repository;

        public RouteApplication(IRouteRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RouteListDto>> GetAll(string search)
        {
            var routes = await _repository.GetAll(search);

            return routes.ToRouteListDto();
        }

        public async Task<IEnumerable<RouteListDto>> GetActive()
        {
            var routes = await _repository.GetActive();

            return routes.ToRouteListDto();
        }

        public async Task<RouteFormDto> GetById(string id)
        {
            Helpers.ValidateGuid(id, out Guid guid);

            var routes = await _repository.GetById(guid);

            if (routes == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return routes.ToRouteFormDto();
        }

        public async Task<RouteListDto> Create(RouteFormDto routes)
        {
            if (!string.IsNullOrEmpty(routes.Id))
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            var newRoute = routes.ToModel();

            await CheckDuplicate(newRoute);

            await CheckDestinoVacio(newRoute);

            await CheckDestino(newRoute);

            await _repository.Create(newRoute);

            return newRoute.ToRouteListDto();
        }

        public async Task<RouteListDto> Update(RouteFormDto routes)
        {
            Helpers.ValidateGuid(routes.Id, out Guid guid);

            var existing = await _repository.GetById(guid);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedRoute = routes.ToModel(existing);

            await CheckDuplicate(updatedRoute);

            await CheckDestinoVacio(updatedRoute);

            await CheckDestino(updatedRoute);

            await _repository.Update(updatedRoute);

            return updatedRoute.ToRouteListDto();
        }

        public async Task<(byte[] file, string fileName)> ExportList(string search)
        {
            var routes = await GetAll(search);

            var path = Assets.RouteList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Rutas");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Rutas", routes);

            template.Generate();

            var range = template.Workbook.Worksheet("Rutas").Range("Rutas");
            var table = template.Workbook.Worksheet("Rutas").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return (template.ToByteArray(), "Catálogo de Rutas.xlsx");
        }

        public async Task<(byte[] file, string fileName)> ExportForm(string id)
        {
            var routes = await GetById(id);

            var path = Assets.RouteForm;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Rutas");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Rutas", routes);
            template.AddVariable("Estudios", routes.Estudio);

            template.Generate();

            template.Format();

            return (template.ToByteArray(), $"Catálogo de Rutas ({routes.Clave}).xlsx");
        }

        private async Task CheckDuplicate(Route routes)
        {
            var isDuplicate = await _repository.IsDuplicate(routes);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave o nombre"));
            }
        }
        private async Task CheckDestino(Route routes)
        {
            var isDuplicate = await _repository.IsDestinoIgualAlOrigen(routes);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.DuplicatedDestiny("El Destino"));
            }
        }

        private async Task CheckDestinoVacio(Route routes)
        {
            var isDuplicate = await _repository.IsDestinoVacio(routes);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.EmptyDestiny("El Destino"));
            }
        }

        public async Task<IEnumerable<RouteFormDto>> FindRoutes(RouteFormDto route)
        {
            var ruta = route.ToModel();
            var routes = await _repository.FindRoute(ruta);

            return routes.ToRouteFoundDto();
        }
    }
}
