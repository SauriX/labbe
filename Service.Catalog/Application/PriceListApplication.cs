﻿using ClosedXML.Excel;
using ClosedXML.Report;
using EventBus.Messages.Common;
using MassTransit;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Client.IClient;
using Service.Catalog.Dictionary;
using Service.Catalog.Dtos.Notifications;
using Service.Catalog.Dtos.Common;
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
using VT = Shared.Dictionary.Catalogs.ValueType;
using Service.Catalog.Domain.Study;

namespace Service.Catalog.Application
{
    public class PriceListApplication : IPriceListApplication
    {
        private readonly IPriceListRepository _repository;
        private readonly IPromotionRepository _promotionRepository;
        private readonly IStudyRepository _studyRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly INotificationsRepository _notificationsRepository;
        private object notification;
        const int PATOLOGIA = 3;
        const int IMAGENOLOGIA = 2;

        public PriceListApplication(IPriceListRepository repository, IPromotionRepository promotionRepository, IStudyRepository studyRepository, IPublishEndpoint publishEndpoint, INotificationsRepository notifications)
        {
            _repository = repository;
            _promotionRepository = promotionRepository;
            _studyRepository = studyRepository;
            _publishEndpoint = publishEndpoint;
            _notificationsRepository = notifications;
        }

        public async Task<IEnumerable<PriceListListDto>> GetAll(string search)
        {
            var prices = await _repository.GetAll(search);

            return prices.ToPriceListListDto(); ;
        }

        public async Task<IEnumerable<PriceListListDto>> GetActive()
        {
            var prices = await _repository.GetActive();

            return prices.ToPriceListListActiveDto();
        }

        public async Task<IEnumerable<OptionsDto>> GetOptions()
        {
            var prices = await _repository.GetOptions();

            return prices.ToOptionsDto();
        }

        public async Task<IEnumerable<OptionsDto>> GetBranchesOptionsByPriceListId(Guid id)
        {
            var branches = await _repository.GetBranchesByPriceListId(id);

            return branches.ToOptionsDto();
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

        public async Task<List<PriceListStudyDto>> GetStudiesById(PriceListStudiesPaginateDto filter)
        {

            //Helpers.ValidateGuid(Id, out Guid guid);

            var studies = await _repository.GetStudiesById(filter.id);

            if (studies == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            return studies.ToPriceListStudyDto(filter);
        }

        public async Task<PriceListInfoStudyDto> GetPriceStudyById(PriceListInfoFilterDto filterDto)
        {
            if (filterDto.EstudioId == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, "No se seleccionó un estudio");
            }

            var price = await _repository.GetPriceStudyById((int)filterDto.EstudioId, filterDto.SucursalId, filterDto.CompañiaId);

            if (price == null || price.Precio <= 0)
            {
                throw new CustomException(HttpStatusCode.NotFound, "Lista de precios no configurada");
            }

            if ((price.Estudio.Parameters == null || price.Estudio.Parameters.Count == 0) && !price.Estudio.DepartamentoId.In(PATOLOGIA, IMAGENOLOGIA))
            {
                throw new CustomException(HttpStatusCode.NotFound, "El estudio no contiene parámetros");
            }

            price.Estudio.Parameters = price.Estudio.Parameters
                .Where(x => !x.Parametro.TipoValor.In(VT.Observacion, VT.Etiqueta, VT.SinValor, VT.Texto, VT.Parrafo))
                .ToList();

            var studyRoute = await _repository.GetStudyRoute(price.EstudioId);

            var priceDto = price.ToPriceListInfoStudyDto(studyRoute);
            priceDto.Identificador = Helpers.GenerateRandomHex(6);

            var promos = await _promotionRepository.GetStudyPromos(price.PrecioListaId, filterDto.SucursalId, filterDto.MedicoId, (int)filterDto.EstudioId);

            if (promos != null && promos.Count > 0)
            {
                foreach (var promo in promos)
                {
                    priceDto.Promociones.Add(new PriceListInfoPromoDto
                    {
                        PromocionId = promo.PromocionId,
                        Promocion = promo.Promocion.Nombre,
                        Descuento = promo.DescuentoCantidad,
                        DescuentoPorcentaje = promo.DescuentoPorcentaje
                    });
                }
            }

            return priceDto;
        }

        public async Task<List<PriceListInfoStudyDto>> GetPriceStudyByCodes(PriceListInfoFilterDto filterDto)
        {
            var studies = new List<PriceListInfoStudyDto>();

            foreach (var item in filterDto.Estudios)
            {
                var studyId = await _studyRepository.GetIdByCode(item);

                if (studyId == 0)
                {
                    throw new CustomException(HttpStatusCode.BadRequest, "No se encontró el estudio " + item);
                }

                filterDto.EstudioId = studyId;
                var info = await GetPriceStudyById(filterDto);

                studies.Add(info);
            }

            return studies;
        }

        public async Task<PriceListInfoPackDto> GetPricePackById(PriceListInfoFilterDto filterDto)
        {
            if (filterDto.PaqueteId == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, "No se seleccionó un paquete");
            }

            var price = await _repository.GetPricePackById((int)filterDto.PaqueteId, filterDto.SucursalId, filterDto.CompañiaId);

            if (price == null || price.PrecioFinal <= 0)
            {
                throw new CustomException(HttpStatusCode.NotFound, "Lista de precios no configurada");
            }

            if (price.Paquete.Estudios == null || price.Paquete.Estudios.Count == 0)
            {
                throw new CustomException(HttpStatusCode.NotFound, "El paquete no contiene estudios");
            }

            if (price.Paquete.Estudios.Any(x => x.Estudio.Parameters == null || x.Estudio.Parameters.Count == 0))
            {
                throw new CustomException(HttpStatusCode.NotFound, "Alguno de los estudios del paquete no contiene parámetros");
            }

            var priceDto = price.ToPriceListInfoPackDto();
            priceDto.Identificador = Helpers.GenerateRandomHex(6);

            var studies = await _repository.GetPriceStudyById(price.PrecioListaId, priceDto.Estudios.Select(x => x.EstudioId));

            var studyRoutes = await _repository.GetStudyRoute(studies.Select(x => x.EstudioId));

            foreach (var study in priceDto.Estudios)
            {
                var studyPrice = studies.FirstOrDefault(x => x.EstudioId == study.EstudioId)?.Precio ?? 0;

                var studyRoute = studyRoutes.FirstOrDefault(x => x.EstudioId == study.EstudioId);

                study.Parametros = study.Parametros
                    .Where(x => !x.TipoValor.In(VT.Observacion, VT.Etiqueta, VT.SinValor, VT.Texto, VT.Parrafo))
                    .ToList();

                study.Precio = (decimal)studyPrice;
                study.Destino = studyRoute?.Ruta?.Nombre;
                study.DestinoId = studyRoute?.RouteId;
                study.DestinoTipo = 1;
            }

            var promos = await _promotionRepository.GetPackPromos(price.PrecioListaId, filterDto.SucursalId, filterDto.MedicoId, (int)filterDto.PaqueteId);

            if (promos != null && promos.Count > 0)
            {
                foreach (var promo in promos)
                {
                    priceDto.Promociones.Add(new PriceListInfoPromoDto
                    {
                        PromocionId = promo.PromocionId,
                        Promocion = promo.Promocion.Nombre,
                        Descuento = promo.DescuentoCantidad,
                        DescuentoPorcentaje = promo.DescuentoPorcentaje
                    });
                }
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
            var notifications = await _notificationsRepository.GetAll("Lista de precios", true);
            var createnotification = notifications.FirstOrDefault(x => x.Tipo == "Create");
            if (createnotification.Activo)
            {
                var mensaje = createnotification.Contenido.Replace("[Nlista]", newprice.Clave);
                mensaje = mensaje.Replace("[Lsucursal]", string.Join(",", newprice.Sucursales.Select(y => y.Sucursal.Nombre)));
                var contract = new NotificationContract(mensaje, false, DateTime.Now);
                await _publishEndpoint.Publish(contract);

            }
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

            var notifications = await _notificationsRepository.GetAll("Lista de precios", true);
            var createnotification = notifications.FirstOrDefault(x => x.Tipo == "Update");
            var mensaje = createnotification.Contenido.Replace("[Nlista]", existing.Clave);
            mensaje = mensaje.Replace("fecha", DateTime.Now.ToShortDateString());
            if (createnotification.Activo)
            {

                var contract = new NotificationContract(mensaje, false, DateTime.Now);
                await _publishEndpoint.Publish(contract);

            }

            if (existing.Activo != price.Activo && price.Activo)
            {
                createnotification = notifications.FirstOrDefault(x => x.Tipo == "Active");
                var mensajeActive = createnotification.Contenido.Replace("[Nlista]", existing.Clave);
                mensaje = mensajeActive.Replace("fecha", DateTime.Now.ToShortDateString());
                if (createnotification.Activo)
                {
                    var contractActive = new NotificationContract(mensaje, false, DateTime.Now);
                    await _publishEndpoint.Publish(contractActive);
                }
            }
            if (existing.Activo != price.Activo && !price.Activo)
            {
                createnotification = notifications.FirstOrDefault(x => x.Tipo == "Disabled");
                var mensajeActive = createnotification.Contenido.Replace("[Nlista]", existing.Clave);
                mensaje = mensajeActive.Replace("fecha", DateTime.Now.ToShortDateString());
                if (createnotification.Activo)
                {
                    var contractActive = new NotificationContract(mensaje, false, DateTime.Now);
                    await _publishEndpoint.Publish(contractActive);
                }
            }

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

            var range = template.Workbook.Worksheet("Lista de Precios").Range("Precios");
            var table = template.Workbook.Worksheet("Lista de Precios").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
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
            template.AddVariable("Estudios", price.Estudios);
            template.AddVariable("Paquetes", price.Paquete);
            template.AddVariable("Sucursales", price.Sucursales);
            template.AddVariable("Compañias", price.Compañia);
            template.Generate();
            var range = template.Workbook.Worksheet("Estudios").Range("Estudios");
            var table = template.Workbook.Worksheet("Estudios").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            var rangep = template.Workbook.Worksheet("Paquetes").Range("Paquetes");
            var tablep = template.Workbook.Worksheet("Paquetes").Range("$A$3:" + rangep.RangeAddress.LastAddress).CreateTable();
            tablep.Theme = XLTableTheme.TableStyleMedium2;

            var ranges = template.Workbook.Worksheet("Sucursales").Range("Sucursales");
            var tables = template.Workbook.Worksheet("Sucursales").Range("$A$3:" + ranges.RangeAddress.LastAddress).CreateTable();
            tables.Theme = XLTableTheme.TableStyleMedium2;
            var rangec = template.Workbook.Worksheet("Compañias").Range("Compañias");
            var tablec = template.Workbook.Worksheet("Compañias").Range("$A$3:" + rangec.RangeAddress.LastAddress).CreateTable();
            tablec.Theme = XLTableTheme.TableStyleMedium2;
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
