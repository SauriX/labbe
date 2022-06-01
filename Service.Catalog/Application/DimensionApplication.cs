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
    public class DimensionApplication : IDimensionApplication
    {
        private readonly IDimensionRepository _repository;

        public DimensionApplication(IDimensionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DimensionListDto>> GetAll(string search = null)
        {
            var dimensions = await _repository.GetAll(search);

            return dimensions.ToDimensionListDto();
        }

        public async Task<IEnumerable<DimensionListDto>> GetActive()
        {
            var dimensions = await _repository.GetActive();

            return dimensions.ToDimensionListDto();
        }

        public async Task<DimensionFormDto> GetById(int id)
        {
            var dimension = await _repository.GetById(id);

            if (dimension == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return dimension.ToDimensionFormDto();
        }

        public async Task<DimensionListDto> Create(DimensionFormDto dimension)
        {
            if (dimension.Id != 0)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            var newDimension = dimension.ToModel();

            await _repository.Create(newDimension);

            return newDimension.ToDimensionListDto();
        }

        public async Task<DimensionListDto> Update(DimensionFormDto dimension)
        {
            var existing = await _repository.GetById(dimension.Id);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedAgent = dimension.ToModel(existing);

            await _repository.Update(updatedAgent);

            return updatedAgent.ToDimensionListDto();
        }

        public async Task<byte[]> ExportList(string search)
        {
            var catalogs = await GetAll(search);

            var path = Assets.DimensionList;

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

            var path = Assets.DimensionForm;

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
