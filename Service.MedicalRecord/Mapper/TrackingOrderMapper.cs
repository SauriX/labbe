using Service.MedicalRecord.Domain.TrackingOrder;
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
                    //Estudio = x.Estudio,
                    ExpedienteId = x.ExpedienteId,
                    NombrePaciente = x.NombrePaciente,
                    Temperatura = x.Temperatura,
                    Escaneado = x.Escaneado,


                }).ToArray()


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
                    //Estudio = x.Estudio,
                    ExpedienteId = x.ExpedienteId,
                    NombrePaciente = x.NombrePaciente,
                    Temperatura = x.Temperatura,
                    Escaneado = x.Escaneado,
                    FechaCreo = DateTime.Now
                    

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
        public static IEnumerable<EstudiosListDto> ToStudiesRequestRouteDto(this IEnumerable<Domain.Request.RequestStudy> model)
        {
            if (model == null) return null;

            //return model.Select(x =>  new StudiesRequestRouteDto
            //{
            //Estudio = x.Nombre,
            //    Clave = x.Clave,
            //    Paciente = x.Solicitud.Expediente.NombreCompleto,
            //    Solicitud = x.Solicitud.Clave,
            //});
            return model.GroupBy(x => x.Solicitud.Id)/*.GroupBy(x => x.Tapon.Clave)*/.Select(x => new EstudiosListDto
            {
                //solicitud = x.,
                Estudios = x.Select(y => new StudiesRequestRouteDto
                {
                    Estudio = y.Nombre,
                    EstudioId = y.EstudioId,
                    Clave = y.Clave,
                    NombrePaciente = y.Solicitud.Expediente.NombreCompleto,
                    Solicitud = y.Solicitud.Clave,
                    TaponNombre = y.Tapon.Clave,
                    SolicitudId = y.Solicitud.Id,
                    ExpedienteId = y.Solicitud.ExpedienteId
                }).ToList(),
            });
        }
    }
}
