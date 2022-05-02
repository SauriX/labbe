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

        public async Task<IEnumerable<ValorTipeForm>> getallvalues(string id,string tipe) {
            var values = await _repository.Getvalues(id,tipe);

            return values.toTipoValorFormList();
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
            var code = await ValidarClaveNombre(parameter);

            if ( code != 0)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave o nombre"));
            }
            var newReagent = parameter.toParameters();

            await _repository.Create(newReagent);
        }

        public async Task Update(ParameterForm parameter)
        {

            var newReagent = parameter.toParameters();
            var existing = await _repository.GetById(parameter.id);
            var code = await ValidarClaveNombre(parameter);
            if (existing.Clave != parameter.clave || existing.Nombre != parameter.nombre) {
                if (code != 0)
                {
                    throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave o nombre"));
                } 
            }
            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedAgent = parameter.toParameters(existing);

            await _repository.Update(updatedAgent);
        }
        public async Task AddValue(ValorTipeForm valorTipeForm) {
            
                var newValor = valorTipeForm.toTipoValor();
                await _repository.addValuNumeric(newValor);
        }
        public async Task<ValorTipeForm> getvalueNum(string id) {
            var valorform = await _repository.getvalueNum(id);
            return valorform.toTipoValorForm();
        }
        public async Task updateValueNumeric(ValorTipeForm tipoValor) {
            var newValor = tipoValor.toTipoValorUpdate();
            await _repository.updateValueNumeric(newValor);
        }
        public async Task<byte[]> ExportList(string search = null)
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

            return template.ToByteArray();
        }

        public async Task<byte[]> ExportForm(string id)
        {
            var parameter = await GetById(id);
            var tipo = parameter.tipoValor;

            var valor = await getallvalues(parameter.id, tipo.ToString());
            var path = Assets.ParameterForm;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Parametros");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Parameter", parameter);
            template.AddVariable("TiposVAlor", valor);
            template.AddVariable("Estudios",parameter.estudios);
            template.Generate();

            template.Format();

            return template.ToByteArray();
        }
        public async Task deletevalue(string id)
        {
            await _repository.deletevalue(id);
        }
            private async Task<int> ValidarClaveNombre(ParameterForm parameter)
        {

            var clave = parameter.clave;
            var name = parameter.nombre;

            var exists = await _repository.ValidateClaveNamne(clave, name);

            if (exists)
            {
                return 1;
            }

            return 0;
        }
    }
}
