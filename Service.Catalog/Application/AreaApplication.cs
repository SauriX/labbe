using ClosedXML.Excel;
using ClosedXML.Report;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dictionary;
using Service.Catalog.Dtos.Catalog;
using Service.Catalog.Mapper;
using Service.Catalog.Repository.IRepository;
using Shared.Dictionary;
using Shared.Error;
using Shared.Extensions;
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

        public async Task<IEnumerable<AreaListDto>> GetActive()
        {
            var areas = await _repository.GetActive();

            return areas.ToAreaListDto();
        }

        public async Task<IEnumerable<AreaListDto>> GetAreaByDepartment(int departmentId)
        {
            var areas = await _repository.GetAreaByDepartment(departmentId);

            return areas.ToAreaListDto();
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

        public async Task<byte[]> ExportList(string search)
        {
            var catalogs = await GetAll(search);

            var path = Assets.AreaList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Reactivos");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Catalogos", catalogs);

            template.Generate();

            var range = template.Workbook.Worksheet("Catálogos").Range("Catalogos");
            var table = template.Workbook.Worksheet("Catálogos").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return template.ToByteArray();
        }

        public async Task<(byte[] file, string code)> ExportForm(int id)
        {
            var catalog = await GetById(id);

            var path = Assets.AreaForm;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Reactivos");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Catalogo", catalog);

            template.Generate();

            template.Format();

            return (template.ToByteArray(), catalog.Clave);
        }
    }
}
