using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Domain.RouteTracking;
using Service.MedicalRecord.Domain.TrackingOrder;
using Service.MedicalRecord.Dtos.RouteTracking;
using Service.MedicalRecord.Dtos.Sampling;
using System.Collections.Generic;
using System.Linq;


namespace Service.MedicalRecord.Mapper
{
    public static  class RouteTrackingMapper
    {
        public static List<RouteTrackingListDto> ToRouteTrackingDto(this ICollection<TrackingOrder> model)
        {
            if (model == null) return null;

            return model.Select(x => new RouteTrackingListDto
            {
                Id = x.Id,
                Seguimiento = x.Clave,
                Clave = x.Clave,
                Sucursal = x.Estudios.Count > 0 ? x.Estudios.FirstOrDefault().Solicitud.Sucursal.Nombre : "",
                Fecha = x.FechaCreo,
                Status = x.Activo.ToString(),
                Estudios = x.Estudios.ToList().ToStudyRouteTrackingDto(),
               
            }).ToList();
        }
        public static RouteTrackingFormDto ToRouteTrackingDto(this TrackingOrder x)
        {
            if (x == null) return null;

            return new RouteTrackingFormDto
            {
                Origen = x.Estudios.FirstOrDefault(y => y.Solicitud.Sucursal.Id.ToString() == x.SucursalOrigenId).Solicitud.Sucursal.Nombre,
                CLave = x.Clave,
                Responsable = "",
                Estudio = "",
                Tramsportista = "",
                Temperatura = x.Temperatura.ToString(),
                Entrega = x.FechaCreo.Date.AddDays(4).ToString(),
                Solicitud = x.Estudios.FirstOrDefault().Solicitud.Clave,
                FechaEnvio = x.FechaCreo.ToString(),
                Paciente = x.Estudios.FirstOrDefault().Solicitud.Expediente.NombreCompleto,
                Destino = x.Estudios.FirstOrDefault(y => y.Solicitud.Sucursal.Id.ToString() == x.SucursalDestinoId).Solicitud.Sucursal.Nombre,
                FechaEstimada = x.FechaCreo.Date.AddDays(5).ToString(),
                FechaReal = x.FechaCreo.Date.AddDays(6).ToString(),


            };
        }
        public static List<RouteTrackingStudyListDto> ToStudyRouteTrackingDto(this ICollection<TrackingOrderDetail> model)
        {
            return model.Select(x => new RouteTrackingStudyListDto
            {
                Id = x.EstudioId,
                Nombre = x.Estudio,
                Area = "",
                Status = x.Solicitud.Estudios.Where(y=>y.EstudioId==x.EstudioId).FirstOrDefault().EstatusId ,
                Registro = x.FechaCreo.ToString(),
                Seleccion = false,
                Clave = x.Solicitud.Clave,
                Expedienteid=x.ExpedienteId.ToString(),
            Solicitudid =x.SolicitudId.ToString(),
            Entrega=x.FechaMod==System.DateTime.MinValue?"":x.FechaMod.ToString(),
         NombreEstatus = x.Solicitud.Estudios.Where(y => y.EstudioId == x.EstudioId).FirstOrDefault().Estatus.Nombre ,
    }).ToList();
        }
    }
}
