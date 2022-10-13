using Service.MedicalRecord.Domain.RouteTracking;
using Service.MedicalRecord.Domain.TrackingOrder;
using Service.MedicalRecord.Dtos.ShipmentTracking;
using System.Collections.Generic;
using System.Linq;

namespace Service.MedicalRecord.Mapper
{
    public static class ShipmentTrakerMapper
    {
        public static List<ShipmentStudydto> toShipmentStudyDto(this List<TrackingOrderDetail> model) {
           return model.Select(x=>  new ShipmentStudydto {
               Estudio = x.Estudio,
               Paciente = x.NombrePaciente,
               Solicitud = x.Solicitud.Clave,
               ConfirmacionOrigen = true,
               ConfirmacionDestino = false,

           }).ToList();
        }

        public static ShipmentTrackingDto toShipmentTrackingDto(this TrackingOrder model,RouteTracking tracking) {

            if (tracking != null) { 

                return new ShipmentTrackingDto
                {
                    Id = model.Id,
                    SucursalOrigen = "",
                    SucursalDestino = "",
                    ResponsableOrigen = "",
                    ResponsableDestino = "",
                    Medioentrega = "",
                    FechaEnvio = tracking.FechaCreo,
                    HoraEnvio = tracking.FechaCreo,
                    FechaEnestimada =  tracking.FechaDeEntregaEstimada,
                    HoraEnestimada = tracking.FechaDeEntregaEstimada,
                    Estudios = model.Estudios.ToList().toShipmentStudyDto().ToList(),
                    Seguimiento = model.Clave,
                    Ruta = "",
                    Nombre = ""
                };
            }
            return new ShipmentTrackingDto
            {
                Id = model.Id,
                SucursalOrigen = "",
                SucursalDestino = "",
                ResponsableOrigen = "",
                ResponsableDestino = "",
                Medioentrega = "",
                Estudios = model.Estudios.ToList().toShipmentStudyDto().ToList(),
                Seguimiento = model.Clave,
                Ruta = "",
                Nombre = ""
            };
        }
    }
}
