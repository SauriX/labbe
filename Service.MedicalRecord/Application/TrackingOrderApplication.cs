﻿using ClosedXML.Excel;
using ClosedXML.Report;
using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Domain.TrackingOrder;
using Service.MedicalRecord.Dtos.TrackingOrder;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Repository.IRepository;
using Shared.Error;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using SharedResponses = Shared.Dictionary.Responses;
using System.Linq;
using Service.MedicalRecord.Domain.Catalogs;
using Service.MedicalRecord.Utils;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Domain.RouteTracking;
using Service.MedicalRecord.Dtos.RouteTracking;

namespace Service.MedicalRecord.Application.IApplication
{
    public class TrackingOrderApplication : ITrackingOrderApplication
    {
        private readonly ITrackingOrderRepository _repository;
        private readonly IRepository<Branch> _branchRepository;
        private readonly IRouteTrackingRepository _routeTrackingRepository;
        private readonly ICatalogClient _catalogClient;
        public readonly IShipmentTrackingRepository _Shipmentrepository;
        public readonly IRequestRepository _requestRepository;

        public TrackingOrderApplication(ITrackingOrderRepository repository, IRepository<Branch> branchRepository, IRouteTrackingRepository routeTrackingRepository,ICatalogClient catalogClient, IShipmentTrackingRepository shipmentrepository,IRequestRepository requestRepository)
        {
            _repository = repository;
            _branchRepository = branchRepository;
            _routeTrackingRepository = routeTrackingRepository;
            _catalogClient = catalogClient;
            _Shipmentrepository = shipmentrepository;
            _requestRepository = requestRepository;
        }
        public async Task<TrackingOrderDto> Create(TrackingOrderFormDto order)
        {
           
            var newOrder = order.ToModel();

            var date = DateTime.Now.ToString("yyMMdd");
            var branchid = Guid.Parse(newOrder.OrigenId);
            var branch = await _branchRepository.GetOne(x => x.Id == branchid);
            var lastCode = await _repository.GetLastCode(Guid.Parse(newOrder.OrigenId), date);

            var code = Codes.GetCode(branch.Codigo, lastCode);
            newOrder.Clave = code;

            List<TrackingOrderDetail> trackingOrderDetails = new List<TrackingOrderDetail>();

            foreach (var item in newOrder.Estudios) {
                var request = await _requestRepository.GetById(item.SolicitudId);
                var requestStudie = request.Estudios.FirstOrDefault(x=>x.EstudioId == item.EtiquetaId);
                item.SolicitudEstudioId = requestStudie.Id;
                trackingOrderDetails.Add(item);
            }
            newOrder.Id = Guid.NewGuid();
            newOrder.Estudios = trackingOrderDetails;
            await _repository.Create(newOrder);
            var seguimientoId = newOrder.Id;
            var shipment = await _Shipmentrepository.GetRouteTracking(seguimientoId);
            if (shipment == null)
            {
                var ruteOrder = await _routeTrackingRepository.GetById(seguimientoId);
                var list = ruteOrder.ToRouteTrackingDtoList();
                List<string> IdRoutes = new ();
                IdRoutes.Add(list.rutaId.ToString());
                var routes = await _catalogClient.GetRutas(IdRoutes);
                var route = routes.FirstOrDefault(x => Guid.Parse(x.Id) == list.rutaId);
                DateTime oDate = Convert.ToDateTime(list.Fecha);
                list.Fecha = oDate.AddDays(route.TiempoDeEntrega).ToString();
                var routeT = new RouteTracking
                {
                    Id = Guid.NewGuid(),
                    SegumientoId = Guid.Parse(ruteOrder.Estudios.FirstOrDefault().SeguimientoId.ToString()),
                    RutaId = Guid.Parse(ruteOrder.RutaId),
                    SucursalId = Guid.Parse(ruteOrder.DestinoId),
                    FechaDeEntregaEstimada = DateTime.Parse(list.Fecha),
                    SolicitudId = ruteOrder.Estudios.FirstOrDefault().SolicitudId,
                    HoraDeRecoleccion = DateTime.Now,
                    UsuarioCreoId = ruteOrder.UsuarioCreoId,
                    FechaCreo = DateTime.Now,

                };
                await _routeTrackingRepository.Create(routeT);
            }
                return newOrder.ToTrackingOrderFormDto();
        }
        public async Task<TrackingOrder> GetExistingOrder(Guid orderId)
        {
            var order = await _repository.FindAsync(orderId);

            if (order == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, SharedResponses.NotFound);
            }

            return order;
        }

        public async Task<TrackingOrderCurrentDto> GetOrderById(Guid orderId)
        {
            var existingOrder = await GetExistingOrder(orderId);
            return existingOrder.toCurrentOrderDto(existingOrder.Estudios.ToStudiesRequestRouteDto(orderId));

        }
        
        public async Task Update(TrackingOrderFormDto order)
        {
            var existingOrder = await GetExistingOrder(order.Id);
            var updatedOrder = order.toUpdateModel(existingOrder);

            List<TrackingOrderDetail> trackingOrderDetails = new List<TrackingOrderDetail>();

            foreach (var item in updatedOrder.Estudios)
            {
                var request = await _requestRepository.GetById(item.SolicitudId);
                var requestStudie = request.Estudios.FirstOrDefault(x => x.EstudioId == item.EtiquetaId);
                item.SolicitudEstudioId = requestStudie.Id;
                trackingOrderDetails.Add(item);
            }

            updatedOrder.Estudios=trackingOrderDetails;
            await _repository.Update(updatedOrder);
            
        }

        public async Task<IEnumerable<EstudiosListDto>> FindEstudios(List<int> estudios)
        {
            var estudiosEncontrados = await _repository.FindEstudios(estudios);
            var estudis = estudiosEncontrados.ToStudiesRequestRouteDto();
            return estudis;
        }

        public async Task<IEnumerable<EstudiosListDto>> FindAllEstudios(List<int> estudios, string request)
        {
            var estudiosEncontrados = await _repository.FindAllEstudios(estudios,request);
            var estudis = estudiosEncontrados.ToStudiesRequestRouteDto(true);
            return estudis;
        }

        public async Task<IEnumerable<RquestStudiesDto>> FindRequestEstudios(string request)
        {
            var estudiosEncontrados = await _repository.FindStudiesRequest(request);
            var estudis = estudiosEncontrados.torequestedStudi();
            return estudis;
        }

        public async Task<(byte[] file, string fileName)> ExportForm(TrackingOrderFormDto order)
        {
            try
            {
                var path = Assets.TrackingOrderForm;

                var template = new XLTemplate(path);

                template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
                template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
                template.AddVariable("Titulo", "Orden de Seguimiento");
                template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
                template.AddVariable("Orden", order);
                template.AddVariable("Estudios", order.Estudios);

                template.Generate();

                var range = template.Workbook.Worksheet("OrdenSeguimiento").Range("Estudios");
                var table = template.Workbook.Worksheet("OrdenSeguimiento").Range("$A$10:" + range.RangeAddress.LastAddress).CreateTable();
                table.Theme = XLTableTheme.TableStyleMedium2;

                template.Format();

                return (template.ToByteArray(), "Creación de Orden de Seguimiento.xlsx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ConfirmarRecoleccion(Guid seguimientoId)
        {
            var shipment = await _Shipmentrepository.GetRouteTracking(seguimientoId);
                shipment.HoraDeRecoleccion = DateTime.Now;
                shipment.FechaModifico = DateTime.Now;
                await _routeTrackingRepository.Update(shipment);
            
               
         
            return await _repository.ConfirmarRecoleccion(seguimientoId);

        }

        public async Task<bool> CancelarRecoleccion(Guid seguimientoId)
        {
            return await _repository.CancelarRecoleccion(seguimientoId);

        }

        
    }
}
