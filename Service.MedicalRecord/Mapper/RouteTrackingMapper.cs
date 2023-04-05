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

            var routes = new List<RouteTrackingListDto>();

            if (model.Count <= 0)
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
                        Estatus = tag.Solicitud.Estudios.FirstOrDefault()?.EstatusId ?? 0,
                        Entrega = "",
                        Ruta = tagRoutes?.Where(x => x.SucursalDestinoId.ToString().Contains(tag.DestinoId)).Select(y => y.Nombre).FirstOrDefault() ?? ""
                    });
                }
            }
            else
            {
                var tagIds = tags.Select(t => t.Id).ToList();
                var tagEstudios = tags.ToDictionary(t => t.Id, t => string.Join(", ", t.Estudios.Select(x => x.NombreEstudio)));
                var tagSolicitudes = tags.ToDictionary(t => t.Id, t => t.Solicitud.Clave);

                foreach (var item in model)
                {
                    var entrega = !string.IsNullOrEmpty(item.FechaEntrega.ToString("dd/MM/yyyy")) ? item.FechaEntrega.ToString("dd/MM/yyyy") : "";

                    foreach (var tag in item.Etiquetas.Where(et => tagIds.Contains(et.Id)))
                    {
                        var estudios = tagEstudios[tag.Id];
                        var solicitud = tagSolicitudes[tag.Id];

                        routes.Add(new RouteTrackingListDto
                        {
                            Id = item.Id,
                            Seguimiento = !string.IsNullOrEmpty(item.Clave) ? item.Clave : "",
                            ClaveEtiqueta = tag.Clave,
                            Recipiente = tag.ClaveEtiqueta,
                            Cantidad = tag.Cantidad,
                            Estudios = estudios,
                            Solicitud = solicitud,
                            Estatus = tag.Solicitud.Estudios.FirstOrDefault()?.EstatusId ?? 0,
                            Entrega = entrega,
                            Ruta = tagRoutes?.Select(x => x.Nombre).FirstOrDefault() ?? ""
                        });
                    }
                }
            }

            return routes;
        }


        public static RouteTrackingDeliverListDto ToRouteTrackingDtoList(this TrackingOrder x)
        {
            if (x == null) return null;

            return new RouteTrackingDeliverListDto
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

        public static TrackingOrder ToModelCreate(this RouteTrackingFormDto dto, string userId = null)
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
                UsuarioCreoId = Guid.Parse(userId),
                Estudios = dto.Estudios.Select(x => new TrackingOrderDetail
                {
                    Id = Guid.NewGuid(),
                    SeguimientoId = x.SeguimientoId,
                    EtiquetaId = x.EtiquetaId,
                    SolicitudId = x.SolicitudId,
                    Cantidad = x.Cantidad,
                    Escaneado = x.Escaneo,
                    Extra = x.Extra,
                    FechaCreo = DateTime.Now,
                    UsuarioCreoId = Guid.Parse(userId)
                }).ToList(),
                Activo = dto.Activo,
            };
        }

        public static TrackingOrder ToModelUpdate(this RouteTrackingFormDto dto, TrackingOrder model, string userId = null)
        {
            if (dto == null || model == null) return null;

            return new TrackingOrder
            {
                Id = model.Id,
                Muestra = model.Muestra,
                Clave = model.Clave,
                DestinoId = dto.Destino,
                OrigenId = dto.Origen,
                Activo = dto.Activo,
                FechaCreo = model.FechaCreo,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaMod = DateTime.Now,
                UsuarioModId = Guid.Parse(userId),
                Escaneo = dto.Escaneo,
                Estudios = dto.Estudios.Select(x => new TrackingOrderDetail
                {
                    Id = x.Id,
                    SeguimientoId = x.SeguimientoId,
                    EtiquetaId = x.EtiquetaId,
                    SolicitudId = x.SolicitudId,
                    Cantidad = x.Cantidad,
                    Escaneado = x.Escaneo,
                    Extra = x.Extra,
                    FechaCreo = x.FechoCreo,
                    UsuarioCreoId = x.UsuarioCreoId,
                    FechaMod = DateTime.Now,
                    UsuarioModId = Guid.Parse(userId)
                }).ToList()
            };
        }

        public static DeliverOrderdDto toDeliverOrder(this RouteTrackingDeliverListDto order, string responsableEnvio)
        {

            return new DeliverOrderdDto
            {
                Destino = order.Sucursal,
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
