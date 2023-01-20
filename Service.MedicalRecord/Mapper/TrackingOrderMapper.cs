using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Domain.TrackingOrder;
using Service.MedicalRecord.Dtos.RouteTracking;
using Service.MedicalRecord.Dtos.TrackingOrder;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.MedicalRecord.Mapper
{
    public static class TrackingOrderMapper
    {
        public static TrackingOrderFormDto toTrackingOrderFormDtos(this TrackingOrder model)
        {
            return new TrackingOrderFormDto
            {
                DiaRecoleccion = model.DiaRecoleccion,
                MuestraId = model.MuestraId,
                Temperatura = model.Temperatura,
                SucursalDestinoId = model.SucursalDestinoId,
                SucursalOrigenId = model.SucursalOrigenId,
                Activo = model.Activo,
                EscaneoCodigoBarras = model.EscaneoCodigoBarras,
                MaquiladorId = model.MaquiladorId,
                RutaId = model.RutaId,
                Clave = model.Clave,
                Fecha=model.FechaCreo,
                Estudios = model.Estudios.Select(x => new StudiesRequestRouteDto
                {
                    SolicitudId = x.SolicitudId,
                    EstudioId = x.EstudioId,
                    Estudio = x.Estudio,
                    Solicitud=x.Solicitud.Clave,
                    ExpedienteId = x.ExpedienteId,
                    NombrePaciente = x.NombrePaciente,
                    Temperatura = x.Temperatura,
                    Escaneado = x.Escaneado,


                }).ToArray(),



            };
        }
        public static TrackingOrderCurrentDto toCurrentOrderDto(this TrackingOrder model, IEnumerable<EstudiosListDto> estudios)
        {
            return new TrackingOrderCurrentDto
            {
                Id = model.Id,
                DiaRecoleccion = model.DiaRecoleccion,
                MuestraId = model.MuestraId,
                Temperatura = model.Temperatura,
                SucursalDestinoId = model.SucursalDestinoId,
                SucursalOrigenId = model.SucursalOrigenId,
                Activo = model.Activo,
                EscaneoCodigoBarras = model.EscaneoCodigoBarras,
                MaquiladorId = model.MaquiladorId,
                RutaId = model.RutaId,
                Clave = model.Clave,
                //Fecha = model.FechaCreo,
                EstudiosAgrupados = estudios.ToList(),
                IsInRute = estudios.Any(x => x.IsInRute && x.orderId == model.Id)
            };
        }
        public static TrackingOrder toUpdateModel(this TrackingOrderFormDto dto, TrackingOrder model)
        {
            return new TrackingOrder
            {
                Id = model.Id,
                MuestraId = dto.MuestraId,
                Temperatura = dto.Temperatura,
                SucursalDestinoId = dto.SucursalDestinoId,
                SucursalOrigenId = dto.SucursalOrigenId,
                UsuarioCreoId = dto.UsuarioCreoId,
                FechaMod = DateTime.Now,
                Activo = dto.Activo,
                EscaneoCodigoBarras = dto.EscaneoCodigoBarras,
                MaquiladorId = dto.MaquiladorId,
                RutaId = model.RutaId,
                DiaRecoleccion = dto.DiaRecoleccion,
                Clave = model.Clave,
                Estudios = dto.Estudios.Select(x => new TrackingOrderDetail
                {
                    SolicitudId = x.SolicitudId,
                    EstudioId = x.EstudioId,
                    Estudio = x.Clave + " - " + x.Estudio,
                    ExpedienteId = x.ExpedienteId,
                    NombrePaciente = x.NombrePaciente,
                    Temperatura = x.Temperatura,
                    Escaneado = x.Escaneado,
                    FechaCreo = DateTime.Now
                }).ToList()
            };
        }
        public static TrackingOrder ToModel(this TrackingOrderFormDto dto)
        {
            if (dto == null) return null;

            
            DateTime dt = DateTime.Now;
            TimeSpan ts = new TimeSpan(dto.HoraDeRecoleccion, 0, 0);
            dt = dt.Date + ts;

            

            var orderModel = new TrackingOrder
            {
                Id = new Guid(),
                MuestraId = dto.MuestraId,
                Temperatura = dto.Temperatura,
                SucursalDestinoId = dto.SucursalDestinoId,
                SucursalOrigenId = dto.SucursalOrigenId,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now,
                Activo = dto.Activo,
                EscaneoCodigoBarras = dto.EscaneoCodigoBarras,
                MaquiladorId = dto.MaquiladorId,
                RutaId = dto.RutaId,
                DiaRecoleccion = dt,
                Clave = dto.Clave,
                Estudios = dto.Estudios.Select(x => new TrackingOrderDetail
                {
                    Id = new Guid(),
                    SolicitudId = x.SolicitudId,
                    EstudioId = x.EstudioId,
                    Estudio = x.Clave + " - " + x.Estudio,
                    ExpedienteId = x.ExpedienteId,
                    NombrePaciente = x.NombrePaciente,
                    Temperatura = x.Temperatura,
                    Escaneado =false,
                    FechaCreo = DateTime.Now,
                    IsExtra = x.IsExtra
                    

                }).ToList()

            };
            return orderModel;
        }
        public static TrackingOrderDto ToTrackingOrderFormDto(this TrackingOrder model)
        {
            if (model == null) return null;

            return new TrackingOrderDto
            {
                Id = model.Id,
                Activo = model.Activo,

                //ClaveEstudio = model.ClaveEstudio,
                //Escaneado = model.Escaneado,
                //EscaneoCodigoBarras = model.EscaneoCodigoBarras,
                //Estudio = model.Estudio,
                //MuestraId = model.MuestraId,
                //PacienteId = model.PacienteId,
                //SolicitudId = model.SolicitudId,
                RutaId = model.RutaId,
                MuestraId = model.MuestraId,
                MaquiladorId = model.MaquiladorId,
                EscaneoCodigoBarras = model.EscaneoCodigoBarras,
                DiaRecoleccion = model.DiaRecoleccion,
                SucursalDestinoId = model.SucursalDestinoId,
                SucursalOrigenId = model.SucursalOrigenId,
                Temperatura = model.Temperatura,
                Clave = model.Clave
            };
        }
        public static IEnumerable<EstudiosListDto> ToStudiesRequestRouteDto(this IEnumerable<Domain.Request.RequestStudy> model,bool isExtra = false )
        {
            if (model == null) return null;


            return model.Select(x => new EstudiosListDto
            {
                
                solicitudId = x.SolicitudId,
                IsInRute = x.Solicitud.Estudios.Any(y => y.EstatusId == Status.RequestStudy.EnRuta && x.EstudioId == y.EstudioId),
                Estudio =   new StudiesRequestRouteDto
                {
                    Estudio = x.Nombre,
                    EstudioId = x.EstudioId,
                    Clave = x.Clave,
                    NombrePaciente = x.Solicitud.Expediente.NombreCompleto,
                    Solicitud = x.Solicitud.Clave,
                    TaponNombre = x.Tapon?.Clave,
                    SolicitudId = x.Solicitud.Id,
                    ExpedienteId = x.Solicitud.ExpedienteId,
                    Escaneado=true,
                    IsExtra = isExtra
                },
                IsExtra = isExtra
            });
        }

        public static IEnumerable<EstudiosListDto> ToStudiesRequestRouteDto(this IEnumerable<TrackingOrderDetail> model, Guid orderId)
        {
            if (model == null) return null;


            return model.Select(x => new EstudiosListDto
            {

                solicitudId = x.SolicitudId,    
                IsInRute = x.SolicitudEstudio.EstatusId == Status.RequestStudy.EnRuta ,
                orderId = orderId,
                Estudio = new StudiesRequestRouteDto
                {
                    Estudio = x.Estudio,
                    EstudioId = x.EstudioId,
                    Clave = x.SolicitudEstudio.Clave,
                    NombrePaciente = x.Solicitud.Expediente.NombreCompleto,
                    Solicitud = x.Solicitud.Clave,
                    SolicitudId = x.Solicitud.Id,
                    ExpedienteId = x.Solicitud.ExpedienteId,
                    Escaneado = true,
                    Temperatura = x.Temperatura

                },
            });
        }

        public static IEnumerable<RquestStudiesDto> torequestedStudi(this IEnumerable<Domain.Request.RequestStudy> model) {

                if (model == null) return null;
            return model.Select(x=> new RquestStudiesDto { 
                Id = x.EstudioId,
                Clave = x.Clave,
                Estudio = x.Nombre,
                Estatus = x.Estatus.Nombre,
                Dias = x.Dias.ToString(),
                Fecha = x.FechaTomaMuestra.ToString(),
                EstatusId = x.EstatusId
            });

        }
    }
}
