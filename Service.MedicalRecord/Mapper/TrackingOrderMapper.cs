using Service.MedicalRecord.Domain.TrackingOrder;
using Service.MedicalRecord.Dtos.TrackingOrder;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.MedicalRecord.Mapper
{
    public static class TrackingOrderMapper
    {
        public static TrackingOrder ToModel(this TrackingOrderFormDto dto)
        {
            if (dto == null) return null;

            return new TrackingOrder
            {
                Id = new Guid(),
                MuestraId = dto.MuestraId,
                Temperatura = dto.Temperatura,
                SucursalDestinoId = dto.SucursalDestinoId,
                SucursalOrigenId = dto.SucursalOrigenId,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now,
                FechaModifico = DateTime.Now,
                Estudios = dto.Estudios.Select(x => new TrackingOrderDetail
                {
                    Id = new Guid(),
                    SucursalDestinoId = dto.SucursalDestinoId,
                    SucursalOrigenId = dto.SucursalOrigenId,
                    FechaCreo = DateTime.Now
                }).ToList()

            };
        }
        public static TrackingOrderDto ToTrackingOrderFormDto(this TrackingOrder model)
        {
            if (model == null) return null;

            return new TrackingOrderDto
            {
                Id = model.Id,
                ClaveEstudio = model.ClaveEstudio,
                Escaneado = model.Escaneado,
                EscaneoCodigoBarras = model.EscaneoCodigoBarras,
                Estudio = model.Estudio,
                MuestraId = model.MuestraId,
                PacienteId = model.PacienteId,
                SolicitudId = model.SolicitudId,
                SucursalDestinoId = model.SucursalDestinoId,
                SucursalOrigenId = model.SucursalOrigenId,
                Temperatura = model.Temperatura

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
                    Clave = y.Clave,
                    Paciente = y.Solicitud.Expediente.NombreCompleto,
                    Solicitud = y.Solicitud.Clave,
                    TaponNombre = y.Tapon.Clave
                }).ToList(),
            });
        }
    }
}
