using ClosedXML.Excel;
using ClosedXML.Report;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dictionary;
using Service.Catalog.Domain.Reagent;
using Service.Catalog.Dtos.Reagent;
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
    public class ReagentApplication : IReagentApplication
    {
        private readonly IReagentRepository _repository;

        public ReagentApplication(IReagentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ReagentListDto>> GetAll(string search)
        {
            var reagents = await _repository.GetAll(search);

            return reagents.ToReagentListDto();
        }

        public async Task<IEnumerable<ReagentListDto>> GetActive()
        {
            var reagents = await _repository.GetActive();

            return reagents.ToReagentListDto();
        }

        public async Task<ReagentFormDto> GetById(string id)
        {
            Helpers.ValidateGuid(id, out Guid guid);

            var reagent = await _repository.GetById(guid);

            if (reagent == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return reagent.ToReagentFormDto();
        }

        public async Task<ReagentListDto> Create(ReagentFormDto reagent)
        {
            if (!string.IsNullOrEmpty(reagent.Id))
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            var newReagent = reagent.ToModel();

            await CheckDuplicate(newReagent);

            await _repository.Create(newReagent);

            return newReagent.ToReagentListDto();
        }

        public async Task<ReagentListDto> Update(ReagentFormDto reagent)
        {
            Helpers.ValidateGuid(reagent.Id, out Guid guid);

            var existing = await _repository.GetById(guid);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedReagent = reagent.ToModel(existing);

            await CheckDuplicate(updatedReagent);

            await _repository.Update(updatedReagent);

            return updatedReagent.ToReagentListDto();
        }

        public async Task<(byte[] file, string fileName)> ExportList(string search)
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

            return (template.ToByteArray(), "Catálogo de Reactivos.xlsx");
        }

        public async Task<(byte[] file, string fileName)> ExportForm(string id)
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

            return (template.ToByteArray(), $"Catálogo de Reactivos ({reagent.Clave}).xlsx");
        }

        private async Task CheckDuplicate(Reagent reagent)
        {
            var isDuplicate = await _repository.IsDuplicate(reagent);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave o nombre"));
            }
        }
    }
}
