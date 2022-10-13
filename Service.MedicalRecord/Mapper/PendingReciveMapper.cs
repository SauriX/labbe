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

            return model.Select(x => new PendingReciveDto
            {
                Id = x.Id.ToString(),
                Nseguimiento = x.Clave,
                Claveroute = x.RutaId,
                Sucursal =x.SucursalOrigenId,
                
                Study = x.Estudios.Select(y=> new ReciveStudyDto {

                    Id = y.Id.ToString(),
                    Estudio = y.Estudio,
                    Solicitud =y.Solicitud.Clave.ToString(),
                }).ToList(),
                Status = new StatusDto {
                    Created = true,
                    Smpling = true,
                    Route = false,
                    Entregado =false,
                },
            }).ToList();
        }
    }
}
