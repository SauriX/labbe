using ClosedXML.Excel;
using ClosedXML.Report;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dictionary;
using Service.Catalog.Domain.Loyalty;
using Service.Catalog.Dtos.Loyalty;
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
    public class LoyaltyApplication : ILoyaltyApplication
    {
        private readonly ILoyaltyRepository _repository;

        public LoyaltyApplication(ILoyaltyRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<LoyaltyListDto>> GetAll(string search)
        {
            var loyaltys = await _repository.GetAll(search);

            return loyaltys.ToLoyaltyListDto();
        }

        public async Task<IEnumerable<LoyaltyListDto>> GetActive()
        {
            var loyaltys = await _repository.GetActive();

            return loyaltys.ToLoyaltyListDto();
        }

        public async Task<LoyaltyFormDto> GetById(Guid Id)
        {
            var loyalty = await _repository.GetById(Id);

            if (loyalty == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return loyalty.ToLoyaltyFormDto();
        }

        public async Task<LoyaltyListDto> Create(LoyaltyFormDto loyalty)
        {
            Helpers.ValidateGuid(loyalty.Id.ToString(), out Guid guid);

            var newloyalty = loyalty.ToModel();

            await CheckDuplicate(newloyalty);

            await _repository.Create(newloyalty);

            return newloyalty.ToLoyaltyListDto();
        }


        public async Task<LoyaltyListDto> Update(LoyaltyFormDto loyalty)
        {
            var existing = await _repository.GetById(loyalty.Id);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedLoyalty = loyalty.ToModel(existing);

            await CheckDuplicate(updatedLoyalty);

            await _repository.Update(updatedLoyalty);

            return updatedLoyalty.ToLoyaltyListDto();
        }

        public async Task<(byte[] file, string fileName)> ExportList(string search)
        {
            var loyaltys = await GetAll(search);

            var path = Assets.LoyaltyList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Indicaciones");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Lealtades", loyaltys);

            template.Generate();

            var range = template.Workbook.Worksheet("Lealtades").Range("Lealtades");
            var table = template.Workbook.Worksheet("Lealtades").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return (template.ToByteArray(), "Catálogo de Lealtades.xlsx");
        }

        public async Task<(byte[] file, string fileName)> ExportForm(Guid id)
        {
            var loyalty = await GetById(id);

            var path = Assets.LoyaltyForm;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Lealtades");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Lealtades", loyalty);

            template.Generate();

            template.Format();

            return (template.ToByteArray(), $"Catálogo de Lealtades (${loyalty.Clave}).xlsx");
        }

        private async Task CheckDuplicate(Loyalty loyalty)
        {
            var isDuplicate = await _repository.IsDuplicate(loyalty);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave o nombre"));
            }
        }
    }
}
