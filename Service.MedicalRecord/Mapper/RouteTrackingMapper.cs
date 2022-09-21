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
                Seguimiento = x.Id.ToString(),
                Clave = x.Clave,
                Sucursal = x.Estudios.Count>0? x.Estudios.FirstOrDefault().Solicitud.Sucursal.Nombre :"",
                Fecha = x.FechaCreo,
                Status = x.Activo.ToString(),
                Estudios = x.Estudios.ToList().ToStudyRouteTrackingDto()
            }).ToList();
        }
        public static RouteTrackingListDto ToRouteTrackingDto(this TrackingOrder x)
        {
            if (x == null) return null;

            return new RouteTrackingListDto
            {
                Id = x.Id,
                Seguimiento = x.Id.ToString(),
                Clave = x.Clave,
                Sucursal = x.SucursalOrigenId,
                Fecha = x.FechaCreo,
                Status = x.Activo.ToString(),
                Estudios = x.Estudios.ToList().ToStudyRouteTrackingDto(),
                Solicitud = x.Estudios.FirstOrDefault().Solicitud.Id

            };
        }
        public static List<RouteTrackingStudyListDto> ToStudyRouteTrackingDto(this ICollection<TrackingOrderDetail> model)
        {
            return model.Select(x => new RouteTrackingStudyListDto
            {
                Id = x.EstudioId,
                Nombre = x.Estudio,
                Area = "",
                Status = 1,
                Registro = x.FechaCreo.ToString(),
                Seleccion = false,
                Clave = x.Solicitud.Clave,
                // NombreEstatus = x.Estatus.Nombre,
            }).ToList();
        }
    }
}
