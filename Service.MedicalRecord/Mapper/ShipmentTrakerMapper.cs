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
               //Estudio = x.Recipiente,
               Paciente = x.NombrePaciente,
               Solicitud = x.Solicitud.Clave,
               ConfirmacionOrigen = true,
               ConfirmacionDestino = x.Escaneado,

           }).ToList();
        }
        public static List<ReciveShipmentStudyDto> toReciveShipmentStudyDto(this List<TrackingOrderDetail> model)
        {
            return model.Select(x => new ReciveShipmentStudyDto
            {
                Id = x.Id,
                //Estudio = x.Recipiente,
                Paciente = x.NombrePaciente,
                Solicitud = x.Solicitud.Clave,
                ConfirmacionOrigen = true,
                ConfirmacionDestino = x.Escaneado,
                //Temperatura = x.Temperatura,

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

        public static ReciveShipmentTracking toReciveShipmentTrackingDto(this TrackingOrder model, RouteTracking tracking)
        {

            if (tracking != null)
            {

                return new ReciveShipmentTracking
                {
                    Id = model.Id,
                    SucursalOrigen = "",
                    SucursalDestino = "",
                    ResponsableOrigen = "",
                    ResponsableDestino = "",
                    Medioentrega = "",
                    FechaEnvio = tracking.FechaCreo,
                    HoraEnvio = tracking.FechaCreo,
                    FechaEnestimada = tracking.FechaDeEntregaEstimada,
                    HoraEnestimada = tracking.FechaDeEntregaEstimada,
                    HoraEnreal = model.FechaMod,
                    FechaEnreal = model.FechaMod,
                    Estudios = model.Estudios.ToList().toReciveShipmentStudyDto().ToList(),
                    Seguimiento = model.Clave,
                    Ruta = "",
                    Nombre = "",
                    //Temperatura = model.Temperatura
                };
            }
            return new ReciveShipmentTracking
            {
                Id = model.Id,
                SucursalOrigen = "",
                SucursalDestino = "",
                ResponsableOrigen = "",
                ResponsableDestino = "",
                Medioentrega = "",
                HoraEnreal = model.FechaMod,
                FechaEnreal = model.FechaMod,
                Estudios = model.Estudios.ToList().toReciveShipmentStudyDto().ToList(),
                Seguimiento = model.Clave,
                Ruta = "",
                Nombre = "",
                //Temperatura = model.Temperatura
            };
        }
    }
}
