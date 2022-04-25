using ClosedXML.Excel;
using ClosedXML.Report;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dictionary;
using Service.Catalog.Dtos.Parameters;
using Service.Catalog.Mapper;
using Service.Catalog.Repository.IRepository;
using Shared.Dictionary;
using Shared.Error;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Service.Catalog.Application
{
    public class ParameterApplication: IParameterApplication
    {
        private readonly IParameterRepository _repository;

        public ParameterApplication(IParameterRepository repository) 
        {
            _repository = repository;
        }
        public async Task<IEnumerable<ParameterList>> GetAll(string search = null)
        {
            var parameters = await _repository.GetAll(search);

            return parameters.ToParameterListDto();
        }

        public async Task<ParameterForm> GetById(string id)
        {
            var parameter = await _repository.GetById(id);

            if (parameter == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return parameter.ToParameterFormDto();
        }

        public async Task Create(ParameterForm parameter)
        {

            var newReagent = parameter.toParameters();

            await _repository.Create(newReagent);
        }

        public async Task Update(ParameterForm parameter)
        {
            var existing = await _repository.GetById(parameter.id);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedAgent = parameter.toParameters(existing);

            await _repository.Update(updatedAgent);
        }

        public async Task<byte[]> ExportList(string search = null)
        {
            var reagents = await GetAll(search);

            var path = Assets.ReagentList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Reactivos");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Reactivos", reagents);

            template.Generate();

            var range = template.Workbook.Worksheet("Reactivos").Range("Reactivos");
            var table = template.Workbook.Worksheet("Reactivos").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return template.ToByteArray();
        }

        public async Task<byte[]> ExportForm(string id)
        {
            var reagent = await GetById(id);

            var path = Assets.ReagentForm;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Reactivos");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Reactivo", reagent);

            template.Generate();

            template.Format();

            return template.ToByteArray();
        }
    }
}
