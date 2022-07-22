using ClosedXML.Excel;
using ClosedXML.Report;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dictionary;
using Service.Catalog.Dtos.Parameter;
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
    public class ParameterApplication : IParameterApplication
    {
        private readonly IParameterRepository _repository;

        public ParameterApplication(IParameterRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ParameterListDto>> GetAll(string search)
        {
            var parameters = await _repository.GetAll(search);

            return parameters.ToParameterListDto();
        }

        public async Task<IEnumerable<ParameterListDto>> GetActive()
        {
            var parameters = await _repository.GetActive();

            return parameters.ToParameterListDto();
        }

        public async Task<ParameterFormDto> GetById(string id)
        {
            Helpers.ValidateGuid(id, out Guid guid);

            var parameter = await _repository.GetById(guid);

            if (parameter == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return parameter.ToParameterFormDto();
        }

        public async Task<IEnumerable<ParameterValueDto>> GetAllValues(string id, string type)
        {
            Helpers.ValidateGuid(id, out Guid guid);

            var values = await _repository.GetAllValues(guid, type);

            return values.ToParameterValueDto();
        }

        public async Task<ParameterValueDto> GetValueById(string id)
        {
            Helpers.ValidateGuid(id, out Guid guid);

            var value = await _repository.GetValueById(guid);

            if (value == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return value.ToParameterValueDto();
        }

        public async Task<ParameterListDto> Create(ParameterFormDto parameter)
        {
            if (!string.IsNullOrEmpty(parameter.Id))
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            var newParameter = parameter.ToModel();

            await CheckDuplicate(newParameter);

            await _repository.Create(newParameter);

            newParameter = await _repository.GetById(newParameter.Id);

            return newParameter.ToParameterListDto();
        }

        public async Task AddValue(ParameterValueDto value)
        {
            if (!string.IsNullOrEmpty(value.Id))
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            var newValue = value.ToModel();

            await _repository.AddValue(newValue);
        }
        public async Task AddValues(List<ParameterValueDto> value, string id)
        {
            if (value == null)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            var newValue = value.ToModel();

            await _repository.AddValues(newValue, id);
        }

        public async Task<ParameterListDto> Update(ParameterFormDto parameter)
        {
            Helpers.ValidateGuid(parameter.Id, out Guid guid);

            var existing = await _repository.GetById(guid);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedParameter = parameter.ToModel(existing);

            await CheckDuplicate(updatedParameter);

            await _repository.Update(updatedParameter);

            updatedParameter = await _repository.GetById(updatedParameter.Id);

            return updatedParameter.ToParameterListDto();
        }

        public async Task UpdateValue(ParameterValueDto value)
        {
            Helpers.ValidateGuid(value.Id, out Guid guid);

            //var existing = await _repository.GetValueById(guid);

            //if (existing == null)
            //{
            //    throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            //}

            var updatedValue = value.ToModel();

            await _repository.UpdateValue(updatedValue);
        }



        public async Task<(byte[] file, string fileName)> ExportList(string search)
        {
            var parameters = await GetAll(search);

            var path = Assets.ParameterList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Parametros");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Parameters", parameters);

            template.Generate();

            var range = template.Workbook.Worksheet("Parameters").Range("Parameters");
            var table = template.Workbook.Worksheet("Parameters").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return (template.ToByteArray(), "Catálogo de Parametros.xlsx");
        }

        public async Task<(byte[] file, string fileName)> ExportForm(string id)
        {
            var parameter = await GetById(id);

            var value = await GetAllValues(parameter.Id, parameter.TipoValor);

            var path = Assets.ParameterForm;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Parametros");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Parameter", parameter);
            template.AddVariable("TiposVAlor", value);
            template.AddVariable("Estudios", parameter.Estudios);
            template.Generate();

            template.Format();

            return (template.ToByteArray(), $"Catálogo de Parametros ({parameter.Clave}).xlsx");
        }

        private async Task CheckDuplicate(Domain.Parameter.Parameter parameter)
        {
            var isDuplicate = await _repository.IsDuplicate(parameter);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave o nombre"));
            }
        }
    }
}
