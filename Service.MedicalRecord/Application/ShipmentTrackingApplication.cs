using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Domain.TrackingOrder;
using Service.MedicalRecord.Dtos.ShipmentTracking;
using Service.MedicalRecord.Dtos.TrackingOrder;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Repository.IRepository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application
{
    public class ShipmentTrackingApplication: IShipmentTrackingApplication
    {
        public readonly IShipmentTrackingRepository _repository;
        private readonly ICatalogClient _catalogClient;
        private readonly IIdentityClient _identityClient;
        public ShipmentTrackingApplication(IShipmentTrackingRepository repository, ICatalogClient catalogClient, IIdentityClient identity)
        {
            _repository = repository;
            _catalogClient = catalogClient;
            _identityClient = identity;
        }
        public async Task<ShipmentTrackingDto> getByid(Guid id) {
            var sucursalDestinos = "";
            var TrackingOrder = await _repository.getTrackingOrder(id);
            var RuteTracking = await _repository.GetRouteTracking(id);
            var shipment = TrackingOrder.toShipmentTrackingDto(RuteTracking);
            if (!string.IsNullOrEmpty(TrackingOrder.DestinoId) && RuteTracking != null)
            {
                
            var usuarioorigen = await _identityClient.GetByid(RuteTracking.UsuarioCreoId.ToString());
            var ruta = await _catalogClient.GetRuta(Guid.Parse(TrackingOrder.RutaId));

               var sucursalDestino = await _catalogClient.GetBranch(Guid.Parse(TrackingOrder.DestinoId));
                sucursalDestinos = sucursalDestino.nombre;
                var sucursalOrigen = await _catalogClient.GetBranch(Guid.Parse(TrackingOrder.OrigenId));
                
                shipment.ResponsableOrigen = $"{usuarioorigen.Nombre} {usuarioorigen.PrimerApellido} {usuarioorigen.SegundoApellido}";
                shipment.Medioentrega = ruta.PaqueteriaId != null ? "laboratorio ramos" : ruta.PaqueteriaId.ToString();
                shipment.Ruta = ruta.Clave;
                shipment.Nombre = ruta.Nombre;
                shipment.SucursalDestino = sucursalDestinos;
                shipment.SucursalOrigen = sucursalOrigen.nombre;
            }

            if (shipment.Estudios.Any(x => x.ConfirmacionDestino))
            {
                var usuariodest = await _identityClient.GetByid(TrackingOrder.UsuarioModId.ToString());
                shipment.ResponsableDestino = $"{usuariodest.Nombre} {usuariodest.PrimerApellido} {usuariodest.SegundoApellido}";
                shipment.FechaEnreal = TrackingOrder.FechaMod;
                shipment.HoraEnreal = TrackingOrder.FechaMod;
            }
            return shipment;
        }
        public async Task<ReciveShipmentTracking> getByidRecive(Guid id)
        {
            var sucursalDestinos = "";
            var TrackingOrder = await _repository.getTrackingOrder(id);
            var RuteTracking = await _repository.GetRouteTracking(id);  
            var shipment = TrackingOrder.toReciveShipmentTrackingDto(RuteTracking);
            if (!string.IsNullOrEmpty(TrackingOrder.DestinoId) && RuteTracking != null)
            {

                var usuarioorigen = await _identityClient.GetByid(RuteTracking.UsuarioCreoId.ToString());
                var ruta = await _catalogClient.GetRuta(Guid.Parse(TrackingOrder.RutaId));

                var sucursalDestino = await _catalogClient.GetBranch(Guid.Parse(TrackingOrder.DestinoId));
                sucursalDestinos = sucursalDestino.nombre;
                var sucursalOrigen = await _catalogClient.GetBranch(Guid.Parse(TrackingOrder.OrigenId));

                shipment.ResponsableOrigen = $"{usuarioorigen.Nombre} {usuarioorigen.PrimerApellido} {usuarioorigen.SegundoApellido}";
                shipment.Medioentrega = ruta.PaqueteriaId != null ? "laboratorio ramos" : ruta.PaqueteriaId.ToString();
                shipment.Ruta = ruta.Clave;
                shipment.Nombre = ruta.Nombre;
                shipment.SucursalDestino = sucursalDestinos;
                shipment.SucursalOrigen = sucursalOrigen.nombre;
            }
            if (shipment.Estudios.Any(x => x.ConfirmacionDestino)) {
                var usuariodest = await _identityClient.GetByid(TrackingOrder.UsuarioModId.ToString());
                shipment.ResponsableDestino = $"{usuariodest.Nombre} {usuariodest.PrimerApellido} {usuariodest.SegundoApellido}";
                shipment.FechaEnreal = TrackingOrder.FechaMod;
                shipment.HoraEnreal = TrackingOrder.FechaMod;
            }
            return shipment;
        }
        public async Task UpdateTracking(ReciveShipmentTracking reciveShipment) {
            var TrackingOrder = await _repository.getTrackingOrder(reciveShipment.Id);
            TrackingOrder.Temperatura = reciveShipment.Temperatura;
            List<TrackingOrderDetail> Estudios = new List<TrackingOrderDetail>();
            foreach (var estudio in TrackingOrder.Estudios) {
                var exisiting = reciveShipment.Estudios.AsQueryable().Any(x=>x.Id==estudio.Id);

                if (exisiting) {
                    var exsistingStudy= reciveShipment.Estudios.FirstOrDefault(x=>x.Id==estudio.Id);
                    estudio.Temperatura = exsistingStudy.Temperatura;
                    estudio.Escaneado = exsistingStudy.ConfirmacionDestino;
                    estudio.FechaMod = exsistingStudy.ConfirmacionDestino?DateTime.Now:DateTime.MinValue;
                    
                    Estudios.Add(estudio);
                }
            
            }
            //TrackingOrder.Estudios = Estudios;
            TrackingOrder.FechaMod = DateTime.Now;
            TrackingOrder.UsuarioModId = reciveShipment.IdUser;
            await _repository.updateTrackingOrder(TrackingOrder);
        }
        public async Task<TrackingOrderFormDto> getorder(Guid id) { 
        
            var order  = await _repository.getTrackingOrder(id);
            List<StudyRouteDto> Estudios = new List<StudyRouteDto>();
            List<int> EstudioIds = new List<int>();
            foreach (var estudio in order.Estudios)
            {

                EstudioIds.Add(estudio.EtiquetaId);

            }
            var order2 = order.toTrackingOrderFormDtos();
            var exisiting = await _catalogClient.GetStudies(EstudioIds);
            foreach (var estudio in order2.Estudios) {
                if (exisiting.Any(x=>x.Id== estudio.EstudioId)) {
                    var data = exisiting.FirstOrDefault(x=>x.Id== estudio.EstudioId);

                    estudio.Clave = data.Clave;
                    Estudios.Add(estudio);
                }
            }
            order2.Estudios = Estudios.ToArray();
            return order2;

        }
    }
}
