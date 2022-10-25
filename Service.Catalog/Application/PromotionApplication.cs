using ClosedXML.Excel;
using ClosedXML.Report;
using DocumentFormat.OpenXml.Vml.Office;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dictionary;
using Service.Catalog.Domain.Promotion;
using Service.Catalog.Domain.Study;
using Service.Catalog.Dtos.PriceList;
using Service.Catalog.Dtos.Promotion;
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


        public async Task<PromotionFormDto> GetById(int id)
        {
            // Helpers.ValidateGuid(id, out Guid guid);

            var parameter = await _repository.GetById(id);

            if (parameter == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return parameter.ToPromotionFormDto();
        }

        public async Task<IEnumerable<PromotionListDto>> GetActive()
        {
            var promotions = await _repository.GetActive();

            return promotions.ToPromotionListDto();
        }

        public async Task<List<PriceListInfoPromoDto>> GetStudyPromos(List<PriceListInfoFilterDto> filters)
        {
            var promosDto = new List<PriceListInfoPromoDto>();

            foreach (var filter in filters)
            {
                var promo = await _repository.GetStudyPromos(filter.ListaPrecioId, filter.SucursalId, filter.MedicoId, (int)filter.EstudioId);

                var promoDto = promo.Select(x => new PriceListInfoPromoDto
                {
                    EstudioId = x.StudyId,
                    PromocionId = x.PromotionId,
                    Promocion = x.Promotion.Nombre,
                    Descuento = x.DiscountNumeric,
                    DescuentoPorcentaje = x.Discountporcent
                }).ToList();

                promosDto.AddRange(promoDto);
            }

            return promosDto;
        }

        public async Task<List<PriceListInfoPromoDto>> GetPackPromos(List<PriceListInfoFilterDto> filters)
        {
            var promosDto = new List<PriceListInfoPromoDto>();

            foreach (var filter in filters)
            {
                var promos = await _repository.GetPackPromos(filter.ListaPrecioId, filter.SucursalId, filter.MedicoId, (int)filter.PaqueteId);

                var promoDto = promos.Select(x => new PriceListInfoPromoDto
                {
                    PaqueteId = x.PackId,
                    PromocionId = x.PromotionId,
                    Promocion = x.Promotion.Nombre,
                    Descuento = x.DiscountNumeric,
                    DescuentoPorcentaje = x.Discountporcent
                }).ToList();

                promosDto.AddRange(promoDto);
            }

            return promosDto;
        }

        public async Task<PromotionListDto> Create(PromotionFormDto parameter)
        {


            var newParameter = parameter.ToModel();

            await CheckDuplicate(newParameter);
            // await CheckPromotionPackActive(newParameter);
            await _repository.Create(newParameter);

            newParameter = await _repository.GetById(newParameter.Id);

            return newParameter.ToPromotionListDto();
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
            //await CheckPromotionPackActive(updatedParameter);
            await _repository.Update(updatedParameter);

            updatedParameter = await _repository.GetById(updatedParameter.Id);

            return updatedParameter.ToPromotionListDto();
        }



        public async Task<(byte[] file, string fileName)> ExportList(string search)
        {
            var promotions = await GetAll(search);

            var path = Assets.PromotionList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Promociones");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Promotions", promotions);

            template.Generate();

            var range = template.Workbook.Worksheet("Promotions").Range("Promotions");
            var table = template.Workbook.Worksheet("Promotions").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return (template.ToByteArray(), "Catálogo de Promociones.xlsx");
        }

        public async Task<(byte[] file, string fileName)> ExportForm(int id)
        {
            var promotion = await GetById(id);
            var dias = promotion.Dias;
            var sucursales = promotion.Branchs;
            var estudios = promotion.Estudio;


            var path = Assets.PromotionForm;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Promociones");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Promotion", promotion);
            template.AddVariable("Dias", dias);
            template.AddVariable("Estudios", estudios);
            template.AddVariable("Sucursales", sucursales);
            template.Generate();

            template.Format();

            return (template.ToByteArray(), $"Catálogo de Promociones ({promotion.Clave}).xlsx");
        }

        private async Task CheckDuplicate(Promotion parameter)
        {
            var isDuplicate = await _repository.IsDuplicate(parameter);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave o nombre"));
            }
        }

        private async Task CheckPromotionPackActive(Promotion promotion)
        {

            var packs = await _repository.packsIsPriceList(promotion.PrecioListaId);
            var packassigned = promotion.packs.ToList();
            foreach (var pack in packs)
            {

                var (isInPromotion, nombre) = await _repository.PackIsOnPromotrtion(pack.PaqueteId);

                var (isOnvalidPromotion, nombre2) = await _repository.PackIsOnInvalidPromotion(pack.PaqueteId);
                var isAssigned = packassigned.Any(x => x.PackId == pack.PaqueteId);
                if ((!isInPromotion || isOnvalidPromotion) && !isAssigned)
                {
                    throw new CustomException(HttpStatusCode.Conflict, $"El paquete {nombre} No tiene una promoción asignada");
                }

            }
        }


    }
}
