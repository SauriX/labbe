using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Dtos.ShipmentTracking;
using Service.MedicalRecord.Dtos.TrackingOrder;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Repository.IRepository;
using System;
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
            var usuarioorigen = await _identityClient.GetByid(RuteTracking.UsuarioCreoId.ToString());
            var ruta = await _catalogClient.GetRuta(Guid.Parse(TrackingOrder.RutaId));
                if ( !string.IsNullOrEmpty(TrackingOrder.SucursalDestinoId) )
            {
               var sucursalDestino = await _catalogClient.GetBranch(Guid.Parse(TrackingOrder.SucursalDestinoId));
                sucursalDestinos = sucursalDestino.nombre;
            }

            
            var sucursalOrigen = await _catalogClient.GetBranch(Guid.Parse(TrackingOrder.SucursalOrigenId));
            var shipment = TrackingOrder.toShipmentTrackingDto(RuteTracking);
            shipment.ResponsableOrigen = $"{usuarioorigen.Nombre} {usuarioorigen.PrimerApellido} {usuarioorigen.SegundoApellido}";
            shipment.Medioentrega = ruta.PaqueteriaId != null ? "laboratorio ramos" : ruta.PaqueteriaId.ToString();
            shipment.Ruta = ruta.Clave;
            shipment.Nombre = ruta.Nombre;
            shipment.SucursalDestino = sucursalDestinos;
            shipment.SucursalOrigen = sucursalOrigen.nombre;
         


            return shipment;
        }

        public async Task<TrackingOrderFormDto> getorder(Guid id) { 
        
            var order  = await _repository.getTrackingOrder(id);

            return order.toTrackingOrderFormDtos();

        }
    }
}
