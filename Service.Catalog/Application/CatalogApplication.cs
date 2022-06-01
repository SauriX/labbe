using ClosedXML.Excel;
using ClosedXML.Report;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dictionary;
using Service.Catalog.Domain.Catalog;
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
    public class CatalogApplication<T> : ICatalogApplication<T> where T : GenericCatalog, new()
    {
        private readonly ICatalogRepository<T> _repository;

        public CatalogApplication(ICatalogRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CatalogListDto>> GetAll(string search = null)
        {
            var catalogs = await _repository.GetAll(search);

            return catalogs.ToCatalogListDto();
        }

        public async Task<IEnumerable<CatalogListDto>> GetActive()
        {
            var catalogs = await _repository.GetActive();

            return catalogs.ToCatalogListDto();
        }

        public async Task<CatalogFormDto> GetById(int id)
        {
            var catalog = await _repository.GetById(id);

            if (catalog == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return catalog.ToCatalogFormDto();
        }

        public async Task<CatalogListDto> Create(CatalogFormDto catalog)
        {
            if (catalog.Id != 0)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            var newCatalog = catalog.ToModel<T>();

            var isDuplicate = await _repository.IsDuplicate(newCatalog);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave o nombre"));
            }

            await _repository.Create(newCatalog);

            return newCatalog.ToCatalogListDto();
        }

        public async Task<CatalogListDto> Update(CatalogFormDto catalog)
        {
            var existing = await _repository.GetById(catalog.Id);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedCatalog = catalog.ToModel(existing);

            var isDuplicate = await _repository.IsDuplicate(updatedCatalog);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave o nombre"));
            }

            await _repository.Update(updatedCatalog);

            return updatedCatalog.ToCatalogListDto();
        }

        public async Task<byte[]> ExportList(string search)
        {
            var catalogs = await GetAll(search);

            var path = Assets.CatalogList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Reactivos");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Catalogos", catalogs);

            template.Generate();

            var range = template.Workbook.Worksheet("Catalogos").Range("Catalogos");
            var table = template.Workbook.Worksheet("Catalogos").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return template.ToByteArray();
        }

        public async Task<(byte[] file, string code)> ExportForm(int id)
        {
            var catalog = await GetById(id);

            var path = Assets.CatalogForm;

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
