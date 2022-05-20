using Service.Catalog.Application.IApplication;
using Service.Catalog.Dtos.Promotion;
using Service.Catalog.Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Catalog.Mapper;
using Shared.Error;
using System.Net;
using Shared.Dictionary;
using ClosedXML.Excel;
using System;
using Service.Catalog.Dictionary;
using ClosedXML.Report;
using Shared.Extensions;
using Service.Catalog.Domain.Promotion;

namespace Service.Catalog.Application
{
    public class PromotionApplication : IPromotionApplication
    {
        private readonly IPromotionRepository _repository;

        public PromotionApplication(IPromotionRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<PromotionListDto>> GetAll(string search)
        {
            var promotions = await _repository.GetAll(search);

            return promotions.ToPromotionListDto();
        }


        public async Task<PromotionFormDto> GetById(int  id)
        {
           // Helpers.ValidateGuid(id, out Guid guid);

            var parameter = await _repository.GetById(id);

            if (parameter == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return parameter.ToPromotionFormDto();
        }




        public async Task<PromotionFormDto> Create(PromotionFormDto parameter)
        {


            var newParameter = parameter.ToModel();

            await CheckDuplicate(newParameter);

            await _repository.Create(newParameter);

            newParameter = await _repository.GetById(newParameter.Id);

            return newParameter.ToPromotionFormDto();
        }


        public async Task<PromotionListDto> Update(PromotionFormDto parameter)
        {
          //  Helpers.ValidateGuid(parameter.Id, out Guid guid);

            var existing = await _repository.GetById(parameter.Id);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedParameter = parameter.ToModel(existing);

            await CheckDuplicate(updatedParameter);

            await _repository.Update(updatedParameter);

            updatedParameter = await _repository.GetById(updatedParameter.Id);

            return updatedParameter.ToPromotionListDto();
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

        public async Task<(byte[] file, string fileName)> ExportForm(int id)
        {
            var parameter = await GetById(id);

 

            var path = Assets.ParameterForm;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Parametros");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Parameter", parameter);
            template.Generate();

            template.Format();

            return (template.ToByteArray(), $"Catálogo de Parametros ({parameter.Clave}).xlsx");
        }

        private async Task CheckDuplicate(Promotion parameter)
        {
            var isDuplicate = await _repository.IsDuplicate(parameter);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave o nombre"));
            }
        }
    }
}
