using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Domain.TrackingOrder;
using Service.MedicalRecord.Dtos.DeliverOrder;
using Service.MedicalRecord.Dtos.Route;
using Service.MedicalRecord.Dtos.RouteTracking;
using Service.MedicalRecord.Dtos.TrackingOrder;
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

            };
        }

        public static RouteTrackingFormDto ToRouteTrackingDto(this TrackingOrder model)
        {
            if (model == null) return null;

            var studyTags = model.Estudios;

            return new RouteTrackingFormDto
            {
                Origen = model.OrigenId,
                Destino = model.DestinoId,
                Clave = model.Clave,
                Temperatura = model.Temperatura,
                Recoleccion = model.DiaRecoleccion,
                Solicitud = model.Estudios.FirstOrDefault().Solicitud?.Clave,
                Activo = model.Activo,
                Escaneo = model.Escaneo,
                Muestra = model.Muestra,
                RutaId = model.RutaId,
                Etiquetas = studyTags.ToTagRouteDto()
            };
        }

        public static List<TagRouteDto> ToTagRouteDto(this IEnumerable<TrackingOrderDetail> tags)
        {
            return tags.Select(x => new TagRouteDto
            {
                Id = x.Id,
                ClaveEtiqueta = x.Etiqueta.ClaveEtiqueta,
                Recipiente = x.Etiqueta.Clave,
                Estudios = string.Join(", ", x.Etiqueta.Estudios.Select(x => x.NombreEstudio)),
                Cantidad = x.Etiqueta.Cantidad,
                ClaveRuta = x.Etiqueta.Destino,
                Escaneo = x.Escaneado,
                Solicitud = x.Solicitud.Clave
            }).ToList();
        }

        public static List<TagTrackingOrderDto> ToTagTrackingOrderDto(this IEnumerable<RequestTag> tags)
        {
            return tags.Select(x => new TagTrackingOrderDto
            {
                Id = x.Id,
                ClaveEtiqueta = x.ClaveEtiqueta,
                Recipiente = x.Clave,
                Estudios = string.Join(", ", x.Estudios.Select(x => x.NombreEstudio)),
                Cantidad = x.Cantidad,
                ClaveRuta = x.Destino,
                Escaneo = false,
                Solicitud = x.Solicitud.Clave
            }).ToList();
        }

        public static TrackingOrder ToModelCreate(this RouteTrackingFormDto dto)
        {
            if (dto == null) return null;

            return new TrackingOrder
            {
                Id = Guid.NewGuid(),
                Muestra = dto.Muestra,
                DestinoId = dto.Destino,
                OrigenId = dto.Origen,
                Temperatura = dto.Temperatura,
                Escaneo = dto.Escaneo,
                FechaCreo = DateTime.Now,
                Estudios = dto.Estudios.Select(x => new TrackingOrderDetail
                {
                    Id = Guid.NewGuid(),

                }).ToList(),
                Activo = dto.Activo,
            };
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
