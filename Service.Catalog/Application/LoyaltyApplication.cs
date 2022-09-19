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
using SharedResponses = Shared.Dictionary.Responses;

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

        public async Task<LoyaltyListDto> GetByDate(DateTime fecha)
        {
            var loyalty = await _repository.GetByDate(fecha);

            if (loyalty == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return loyalty.ToLoyaltyDto();
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

            var newloyalty = loyalty.ToModelCreate();


            await CheckTipoDescuento(newloyalty);
            var fecha = await CheckDuplicateDate(loyalty, loyalty.Id);

            //if (code != 0)
            //{
            //    throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave o nombre"));
            //}
            if (fecha != 0)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.DuplicatedDate("La Fecha Inicial o La Fecha Final"));
            }

            if (loyalty.Id != Guid.Empty)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            await _repository.Create(newloyalty);

            newloyalty = await _repository.GetById(loyalty.Id);

            return newloyalty.ToLoyaltyListDto();
        }


        public async Task<LoyaltyListDto> Update(LoyaltyFormDto loyalty)
        {
            var existing = await _repository.GetById(loyalty.Id);

            if (existing.FechaInicial != loyalty.FechaInicial || existing.FechaFinal != loyalty.FechaFinal)
            {
                var code = await CheckDuplicateDate(loyalty, loyalty.Id);
                if (code != 0)
                {
                    throw new CustomException(HttpStatusCode.Conflict, Responses.DuplicatedDate("La Fecha Inicial o la Fecha Final"));
                }
            }
            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedLoyalty = loyalty.ToModelUpdate(existing);
            await CheckTipoDescuento(updatedLoyalty);

            await _repository.Update(updatedLoyalty);

            updatedLoyalty = await _repository.GetById(loyalty.Id); 

            return updatedLoyalty.ToLoyaltyListDto();
        }

        public async Task<(byte[] file, string fileName)> ExportList(string search)
        {
            var loyaltys = await GetAll(search);

            var path = Assets.LoyaltyList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Lealtades");
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

        private async Task CheckTipoDescuento(Loyalty loyalty)
        {
            var isDuplicate = await _repository.IsPorcentaje(loyalty);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.TipoDescuento("El Descuento/Cantidad"));
            }
        }

        private async Task<int> CheckDuplicateDate(LoyaltyFormDto loyalty, Guid id)
        {
            DateTime fechainicial = loyalty.FechaInicial;
            DateTime fechafinal = loyalty.FechaFinal;

            var exists = await _repository.IsDuplicateDate(fechainicial, fechafinal, id);

            if (exists)
            {
                return 1;
            }

            return 0;
        }

        public async Task<LoyaltyListDto> CreateReschedule(LoyaltyFormDto loyalty)
        {
            Helpers.ValidateGuid(loyalty.Id.ToString(), out Guid guid);

            _ = await GetExistingLoyalty(loyalty.Id);

            Loyalty newloyalty = loyalty.ToModelCreate();

            await _repository.Create(newloyalty);

            newloyalty = await _repository.GetById(loyalty.Id);

            return newloyalty.ToLoyaltyListDto();
        }

        private async Task<Loyalty> GetExistingLoyalty(Guid loyaltyId)
        {
            var loyalty = await _repository.GetById(loyaltyId);

            if (loyalty == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            return loyalty;
        }

    }
}
