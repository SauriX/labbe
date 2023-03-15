using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Domain.RouteTracking;
using Service.MedicalRecord.Domain.TrackingOrder;
using Service.MedicalRecord.Dtos.PendingRecive;
using System.Collections.Generic;
using System.Linq;

namespace Service.MedicalRecord.Mapper
{
    public static class PendingReciveMapper
    {
        public static List<PendingReciveDto> ToPendingReciveDto(this List<TrackingOrder> model)
        {
            if (model == null) return null;
            List<PendingReciveDto> Recive = new List<PendingReciveDto>();
            foreach (TrackingOrder item in model)
            {
                foreach (var estudio in item.Estudios)
                {
                    Recive.Add(
                        new PendingReciveDto
                        {
                            Id = item.Id.ToString(),
                            //Nseguimiento = estudio.SolicitudEstudio.EstatusId == Status.RequestStudy.TomaDeMuestra || estudio.SolicitudEstudio.EstatusId == Status.RequestStudy.EnRuta ? estudio.IsExtra ? $"{item.Clave}-incluido" : item.Clave : "",
                            //Claveroute =item.RutaId,
                            //Solicitud = estudio.Solicitud.Clave,
                            //Estudio = $"{estudio.SolicitudEstudio.Clave}-{estudio.Recipiente}",
                            Sucursal = item.OrigenId,
                            Fechareal = item.FechaMod,
                            Status = new StatusDto
                            {
                                Created = true,
                                Smpling = true,
                                Route = false,
                                Entregado = false,
                            },

                        }
                        );
                
                
                }

            }
            return Recive;
        }
    }
}
