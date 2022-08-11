using ClosedXML.Excel;
using ClosedXML.Report;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dictionary;
using Service.Catalog.Dtos.PriceList;
using Service.Catalog.Mapper;
using Service.Catalog.Repository.IRepository;
using Shared.Dictionary;
using Shared.Error;
using Shared.Extensions;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Service.Catalog.Application
{
    public class PriceListApplication : IPriceListApplication
    {
        private readonly IPriceListRepository _repository;
        private readonly IPromotionRepository _promotionRepository;

        public PriceListApplication(IPriceListRepository repository, IPromotionRepository promotionRepository)
        {
            _repository = repository;
            _promotionRepository = promotionRepository;
        }

        public async Task<IEnumerable<PriceListListDto>> GetAll(string search)
        {
            var prices = await _repository.GetAll(search);

            return prices.ToPriceListListDto();
        }

        public async Task<IEnumerable<PriceListListDto>> GetActive()
        {
            var prices = await _repository.GetActive();

            return prices.ToPriceListListDto();
        }

        public async Task<PriceListFormDto> GetById(string id)
        {
            Helpers.ValidateGuid(id, out Guid guid);

            var price = await _repository.GetById(guid);

            if (price == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return price.ToPriceListFormDto();
        }

        public async Task<PriceListInfoStudyDto> GetPriceStudyById(PriceListInfoFilterDto filterDto)
        {
            if (filterDto.EstudioId == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, "No se seleccionó un estudio");
            }

            var price = await _repository.GetPriceStudyById((int)filterDto.EstudioId, filterDto.SucursalId, filterDto.CompañiaId, filterDto.MedicoId);

            if (price == null || price.Precio <= 0)
            {
                throw new CustomException(HttpStatusCode.NotFound, "Lista de precios no configurada");
            }

            if (price.Estudio.Parameters == null || price.Estudio.Parameters.Count == 0)
            {
                throw new CustomException(HttpStatusCode.NotFound, "El estudio no contiene parámetros");
            }

            var priceDto = price.ToPriceListInfoStudyDto();

            var promo = await _promotionRepository.GetStudyPromo(price.PrecioListaId, filterDto.SucursalId, (int)filterDto.EstudioId);

            if (promo != null)
            {
                priceDto.PromocionId = promo.PromotionId;
                priceDto.Promocion = promo.Promotion.Nombre;
                priceDto.Descuento = promo.DiscountNumeric;
                priceDto.DescuentoPorcentaje = promo.Discountporcent;
            }

            return priceDto;
        }

        public async Task<PriceListInfoPackDto> GetPricePackById(PriceListInfoFilterDto filterDto)
        {
            if (filterDto.PaqueteId == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, "No se seleccionó un paquete");
            }

            var price = await _repository.GetPricePackById((int)filterDto.PaqueteId, filterDto.SucursalId, filterDto.CompañiaId, filterDto.MedicoId);

            if (price == null || price.Precio <= 0)
            {
                throw new CustomException(HttpStatusCode.NotFound, "Lista de precios no configurada");
            }

            if (price.Paquete.studies == null || price.Paquete.studies.Count == 0)
            {
                throw new CustomException(HttpStatusCode.NotFound, "El paquete no contiene estudios");
            }

            if (price.Paquete.studies.Any(x => x.Estudio.Parameters == null || x.Estudio.Parameters.Count == 0))
            {
                throw new CustomException(HttpStatusCode.NotFound, "Alguno de los estudios del paquete no contiene parámetros");
            }

            var priceDto = price.ToPriceListInfoPackDto();

            var studies = await _repository.GetPriceStudyById(price.PrecioListaId, priceDto.Estudios.Select(x => x.EstudioId));

            foreach (var study in priceDto.Estudios)
            {
                var studyPrice = studies.FirstOrDefault(x => x.EstudioId == study.EstudioId)?.Precio;

                if (studyPrice == null || studyPrice <= 0)
                {
                    throw new CustomException(HttpStatusCode.NotFound, $"Estudio ${study.Clave} no tiene precio configurado");
                }

                study.Precio = (decimal)studyPrice;
            }

            var promo = await _promotionRepository.GetPackPromo(price.PrecioListaId, filterDto.SucursalId, (int)filterDto.PaqueteId);

            if (promo != null)
            {
                priceDto.PromocionId = promo.PromotionId;
                priceDto.Promocion = promo.Promotion.Nombre;
                priceDto.Descuento = promo.DiscountNumeric;
                priceDto.DescuentoPorcentaje = promo.Discountporcent;
            }

            return priceDto;
        }

        public async Task<PriceListListDto> Create(PriceListFormDto price)
        {
            if (!string.IsNullOrEmpty(price.Id))
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }

            var newprice = price.ToModel();

            await CheckDuplicate(newprice);

            await _repository.Create(newprice);

            newprice = await _repository.GetById(newprice.Id);

            return newprice.ToPriceListListDto();
        }


        public async Task<PriceListListDto> Update(PriceListFormDto price)
        {
            Helpers.ValidateGuid(price.Id, out Guid guid);

            var existing = await _repository.GetById(guid);
            CheckStudys(price);
            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedprice = price.ToModel(existing);

            await CheckDuplicate(updatedprice);

            await _repository.Update(updatedprice);

            updatedprice = await _repository.GetById(updatedprice.Id);

            return updatedprice.ToPriceListListDto();
        }


        public async Task<(byte[] file, string fileName)> ExportList(string search)
        {
            var prices = await GetAll(search);

            var path = Assets.PriceListList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Lista de Precios");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Precios", prices);

            template.Generate();

            var range = template.Workbook.Worksheet("Precios").Range("Precios");
            var table = template.Workbook.Worksheet("Precios").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return (template.ToByteArray(), "Catálogo de Lista de Precios.xlsx");
        }

        public async Task<(byte[] file, string fileName)> ExportForm(string id)
        {
            var price = await GetById(id);

            var path = Assets.PriceListForm;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Lista de Precios");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Precios", price);
            template.AddVariable("Estudios", price.Estudios.Concat(price.Paquete));
            template.AddVariable("Sucursales", price.Sucursales);
            template.AddVariable("Medicos", price.Medicos);
            template.AddVariable("Compañias", price.Compañia);
            template.Generate();

            template.Format();

            return (template.ToByteArray(), $"Catálogo de lista de Precios ({price.Clave}).xlsx");
        }

        private async Task CheckDuplicate(Domain.Price.PriceList price)
        {
            var isDuplicate = await _repository.IsDuplicate(price);

            var isCSMDuoplicate = await _repository.DuplicateSMC(price);

            if (isDuplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave o nombre"));
            }

            if (isCSMDuoplicate)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La Sucursal , La compañia o el  medico"));
            }
        }
        public async Task<IEnumerable<PriceListCompanyDto>> GetAllCompany(Guid companyId)
        {
            var prices = await _repository.GetAllCompany(companyId);
            //prices.Select(x=>x.ToPriceListListComDto)
            return prices.ToPriceListListComDto();
        }
        public async Task<IEnumerable<PriceListBranchDto>> GetAllBranch(Guid branchId)
        {
            var prices = await _repository.GetAllBranch(branchId);

            return prices.ToPriceListListSucDto();
        }
        public async Task<IEnumerable<PriceListMedicDto>> GetAllMedics(Guid medicsId)
        {
            var prices = await _repository.GetAllMedics(medicsId);

            return prices.ToPriceListListMedDto();
        }


        private static void CheckStudys(PriceListFormDto price)
        {
            var estudios = price.Estudios.AsQueryable();
            foreach (var paquete in price.Paquete)
            {
                foreach (var estudio in paquete.Pack)
                {
                    var existe = estudios.Any(x => x.Id == estudio.Id);
                    if (!existe)
                    {
                        throw new CustomException(HttpStatusCode.Conflict, $"El estudio {estudio.Clave} No tiene un precio asignada");
                    }
                }
            }


        }
    }
}
