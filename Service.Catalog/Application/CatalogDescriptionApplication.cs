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
    public class CatalogDescriptionApplication<T> : ICatalogDescriptionApplication<T> where T : GenericCatalogDescription, new()
    {
        private readonly ICatalogRepository<T> _repository;

        public CatalogDescriptionApplication(ICatalogRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CatalogDescriptionListDto>> GetAll(string search = null)
        {
            var catalogs = await _repository.GetAll(search);

            return catalogs.ToCatalogDescriptionListDto();
        }

        public async Task<IEnumerable<CatalogDescriptionListDto>> GetActive()
        {
            var catalogs = await _repository.GetActive();

            return catalogs.ToCatalogDescriptionListDto();
        }

        public async Task<CatalogDescriptionFormDto> GetById(int id)
        {
            var catalog = await _repository.GetById(id);

            if (catalog == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return catalog.ToCatalogDescriptionFormDto();
        }

        public async Task<CatalogDescriptionListDto> Create(CatalogDescriptionFormDto catalog)
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

            return newCatalog.ToCatalogDescriptionListDto();
        }

        public async Task<CatalogDescriptionListDto> Update(CatalogDescriptionFormDto catalog)
        {
            var existing = await _repository.GetById(catalog.Id);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedAgent = catalog.ToModel(existing);

            var isDuplicate = await _repository.IsDuplicate(updatedAgent);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave o nombre"));
            }

            await _repository.Update(updatedAgent);

            return updatedAgent.ToCatalogDescriptionListDto();
        }

        public async Task<byte[]> ExportList(string search)
        {
            var catalogs = await GetAll(search);

            var path = Assets.DescriptionList;

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

            var path = Assets.DescriptionForm;

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