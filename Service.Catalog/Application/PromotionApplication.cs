using ClosedXML.Excel;
using ClosedXML.Report;
using DocumentFormat.OpenXml.Vml.Office;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dictionary;
using Service.Catalog.Domain.Promotion;
using Service.Catalog.Domain.Reagent;
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
        private readonly IPriceListRepository _priceListRepository;

        public PromotionApplication(IPromotionRepository repository, IPriceListRepository priceListRepository)
        {
            _repository = repository;
            _priceListRepository = priceListRepository;
        }

        public async Task<IEnumerable<PromotionListDto>> GetAll(string search)
        {
            var promotions = await _repository.GetAll(search);

            return promotions.ToPromotionListDto();
        }

        public async Task<IEnumerable<PromotionListDto>> GetActive()
        {
            var promotions = await _repository.GetActive();

            return promotions.ToPromotionListDto();
        }


        public async Task<PromotionFormDto> GetById(int id)
        {
            var promotion = await _repository.GetById(id);

            if (promotion == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return promotion.ToPromotionFormDto();
        }

        public async Task<List<PriceListInfoPromoDto>> GetStudyPromos(List<PriceListInfoFilterDto> filters)
        {
            var promosDto = new List<PriceListInfoPromoDto>();

            foreach (var filter in filters)
            {
                var promo = await _repository.GetStudyPromos(filter.ListaPrecioId, filter.SucursalId, filter.MedicoId, (int)filter.EstudioId);

                var promoDto = promo.Select(x => new PriceListInfoPromoDto
                {
                    EstudioId = x.EstudioId,
                    PromocionId = x.PromocionId,
                    Promocion = x.Promocion.Nombre,
                    Descuento = x.DescuentoCantidad,
                    DescuentoPorcentaje = x.DescuentoPorcentaje
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
                    PaqueteId = x.PaqueteId,
                    PromocionId = x.PromocionId,
                    Promocion = x.Promocion.Nombre,
                    Descuento = x.DescuentoCantidad,
                    DescuentoPorcentaje = x.DescuentoPorcentaje
                }).ToList();

                promosDto.AddRange(promoDto);
            }

            return promosDto;
        }

        public async Task<IEnumerable<PromotionStudyPackDto>> GetStudies(PromotionFormDto promoDto, bool initial)
        {
            var priceStudyPack = await _priceListRepository.GetStudiesAndPacks(promoDto.ListaPrecioId);
            var promoStudyPack = await _repository.GetStudiesAndPacks(promoDto.Id) ?? new Promotion();

            var promos = priceStudyPack.ToPromotionStudyPackDto(promoDto, promoStudyPack, initial);

            return promos;
        }

        public async Task<PromotionListDto> Create(PromotionFormDto promoDto)
        {
            if (promoDto.Id != 0)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            var newPromo = promoDto.ToModel();

            await CheckDuplicate(newPromo);

            await _repository.Create(newPromo);

            newPromo = await _repository.GetById(newPromo.Id);

            return newPromo.ToPromotionListDto();
        }


        public async Task<PromotionListDto> Update(PromotionFormDto promoDto)
        {
            var existing = await _repository.GetById(promoDto.Id);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedPromo = promoDto.ToModel(existing);

            await CheckDuplicate(updatedPromo);

            await _repository.Update(updatedPromo);

            updatedPromo = await _repository.GetById(updatedPromo.Id);

            return updatedPromo.ToPromotionListDto();
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

            var range = template.Workbook.Worksheet("Promociones").Range("Promotions");
            var table = template.Workbook.Worksheet("Promociones").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return (template.ToByteArray(), "Catálogo de Promociones.xlsx");
        }

        public async Task<(byte[] file, string fileName)> ExportForm(int id)
        {
            var promotion = await GetById(id);
            var dias = promotion.Dias;

            var path = Assets.PromotionForm;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Promociones");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Promotion", promotion);
            template.AddVariable("Dias", dias);
            template.Generate();

            template.Format();

            return (template.ToByteArray(), $"Catálogo de Promociones ({promotion.Clave}).xlsx");
        }

        private async Task CheckDuplicate(Promotion promotion)
        {
            var isDuplicate = await _repository.IsDuplicate(promotion);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave o nombre"));
            }
        }
    }
}
