using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Domain.TrackingOrder;
using Service.MedicalRecord.Dtos.DeliverOrder;
using Service.MedicalRecord.Dtos.Route;
using Service.MedicalRecord.Dtos.RouteTracking;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Service.MedicalRecord.Mapper
{
    public static class RouteTrackingMapper
    {
        public static List<RouteTrackingListDto> ToRouteTrackingDto(this ICollection<TrackingOrder> model, List<RequestTag> tags, List<RouteFormDto> tagRoutes = null)
        {
            if (model == null) return null;
             List< RouteTrackingListDto > routes = new List<RouteTrackingListDto>();

            if(model.Count <= 0)
            {
                foreach (var tag in tags)
                {
                    routes.Add(new RouteTrackingListDto
                    {
                        Id = Guid.Empty,
                        Seguimiento = "",
                        ClaveEtiqueta = tag.Clave,
                        Recipiente = tag.ClaveEtiqueta,
                        Cantidad = tag.Cantidad,
                        Estudios = string.Join(", ", tag.Estudios.Select(x => x.NombreEstudio)),
                        Solicitud = tag.Solicitud.Clave,
                        Estatus = tag.Solicitud.Estudios.FirstOrDefault().EstatusId,
                        Entrega = "",
                        Ruta = tagRoutes != null ? string.Join(", ", tagRoutes.Where(x => x.SucursalDestinoId.ToString().Contains(tag.DestinoId)).Select(y => y.Nombre)) : ""
                    });
                }
            } else
            {
                foreach (var item in model)
                {
                    foreach (var tag in tags)
                    {
                        routes.Add(new RouteTrackingListDto
                        {
                            Id = item.Etiquetas.Select(x => x.Id).Contains(tag.Id) ? item.Id : Guid.Empty,
                            Seguimiento = !string.IsNullOrEmpty(item.Clave) ? item.Clave : "",
                            ClaveEtiqueta = tag.Clave,
                            Recipiente = tag.ClaveEtiqueta,
                            Cantidad = tag.Cantidad,
                            Estudios = string.Join(", ", tag.Estudios.Select(x => x.NombreEstudio)),
                            Solicitud = tag.Solicitud.Clave,
                            Estatus = tag.Solicitud.Estudios.FirstOrDefault().EstatusId,
                            Entrega = !string.IsNullOrEmpty(item.FechaEntrega.ToString()) ? item.FechaEntrega.ToString("dd/MM/YYYY") : "",
                            Ruta = tagRoutes != null ? string.Join(", ", tagRoutes.Select(x => x.Nombre)) : ""
                        });
                    }
                }
            }

            return routes;
        }

        public static RouteTrackingDeliverListDto ToRouteTrackingDtoList(this TrackingOrder x)
        {
            if (x == null) return null;

            return  new RouteTrackingDeliverListDto
            {
                Id = x.Id,
                Seguimiento = x.Clave,
                Clave = x.Clave,
                Sucursal = x.Estudios.Count > 0 ? x.Estudios.FirstOrDefault().Solicitud.Sucursal.Nombre : "",
                Fecha = x.FechaCreo.ToString(),
                Status = x.Activo.ToString(),
                Estudios = x.Estudios.ToList().ToStudyRouteTrackingDto(x.Id),
                rutaId = Guid.Parse(x.RutaId)

            };
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
        public static List<RouteTrackingStudyListDto> ToStudyRouteTrackingDto(this ICollection<TrackingOrderDetail> model, Guid ruteid)
        {
            var modeling = model;
            return model.Select(x => new RouteTrackingStudyListDto
            {
                Id = x.EstudioId,
                Nombre = x.Estudio,
                Area = "",
                Status = x.Solicitud.Estudios.Where(y => y.EstudioId == x.EstudioId).FirstOrDefault()?.EstatusId,
                Registro = x.FechaCreo.ToString(),
                Seleccion = false,
                Clave = x.Solicitud.Clave,
                Expedienteid = x.ExpedienteId.ToString(),
                Solicitudid = x.SolicitudId.ToString(),
                Entrega = x.FechaMod == System.DateTime.MinValue ? "" : x.FechaMod.ToString(),
                NombreEstatus = x.Solicitud.Estudios.Where(y => y.EstudioId == x.EstudioId).FirstOrDefault()?.Estatus.Nombre,
                RouteId = ruteid.ToString(),
                
            }).ToList();
        }


        public static List<DeliverOrderStudyDto> toDeliverOrderStudyDto(this ICollection<RouteTrackingStudyListDto> model , TrackingOrder order) {
            return model.Select(x => new DeliverOrderStudyDto {
            Clave= x.Clave,
            Estudio =x.Nombre,
                Temperatura = Convert.ToDecimal(order.Temperatura),
                Paciente =order.Estudios.FirstOrDefault(y=>y.SolicitudId == Guid.Parse(x.Solicitudid) && y.EstudioId == x.Id).Solicitud.Expediente.NombreCompleto,
                ConfirmacionOrigen = order.Estudios.FirstOrDefault(y => y.SolicitudId == Guid.Parse(x.Solicitudid) && y.EstudioId == x.Id).Solicitud.Estudios.FirstOrDefault(w=>w.EstudioId==x.Id).EstatusId== Status.RequestStudy.TomaDeMuestra,
                ConfirmacionDestino =false,

            }).ToList();
        }

        public static DeliverOrderdDto toDeliverOrder(this RouteTrackingDeliverListDto order,string responsableEnvio) {

            return new DeliverOrderdDto
            {
                Destino=order.Sucursal,
                ResponsableRecive = "",
                FechaEntestimada = order.Fecha,
                Origen = order.Sucursal,
                ResponsableEnvio = responsableEnvio,
                TransportistqName = "",
                Medioentrega = "",
                FechaEnvio = "",


            };
        
            
        }
    }



}
