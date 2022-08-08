using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.MedicalRecord.Mapper
{
    public static class RequestMapper
    {
        public static RequestDto ToRequestDto(this Request model)
        {
            if (model == null) return null;

            return new RequestDto
            {
                SolicitudId = model.Id,
                ExpedienteId = model.ExpedienteId,
                SucursalId = model.SucursalId,
                Clave = model.Clave,
                EsNuevo = model.EsNuevo,
                Registro = $"{model.FechaCreo:dd/MM/yyyy}"
            };
        }

        public static RequestGeneralDto ToRequestGeneralDto(this Request model)
        {
            if (model == null) return null;

            return new RequestGeneralDto
            {
                SolicitudId = model.Id,
                ExpedienteId = model.ExpedienteId,
                Procedencia = model.Procedencia,
                CompañiaId = model.CompañiaId,
                MedicoId = model.MedicoId,
                Afiliacion = model.Afiliacion,
                Urgencia = model.Urgencia,
                Correo = model.EnvioCorreo,
                Whatsapp = model.EnvioWhatsApp,
                Observaciones = model.Observaciones
            };
        }

        public static RequestOrderDto ToRequestOrderDto(this Request model)
        {
            if (model == null) return null;

            var studies = model.Paquetes?.SelectMany(x => x.Estudios)?.ToList() ?? new List<RequestStudy>();
            studies.AddRange(model.Estudios ?? new List<RequestStudy>());

            return new RequestOrderDto
            {
                Sucursal = "Sucursal",
                FolioVenta = "Folio de venta",
                FechaVenta = DateTime.Now.ToString("yyyy-MM-dd"),
                Personal = model.UsuarioCreo,
                Paciente = model.Expediente.NombreCompleto,
                FechaNacimiento = model.Expediente.FechaDeNacimiento.ToString("dd-MM-yyyy"),
                Expediente = model.Expediente.Expediente,
                Codigo = "Codigo",
                PacienteId = "PacienteId",
                FechaEntrega = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"),
                Medico = "Medico",
                Estudios = studies.Select(x => new RequestOrderStudyDto
                {
                    Clave = x.Clave,
                    Estudio = x.Nombre,
                    Precio = x.Precio.ToString()
                }).ToList()
            };
        }

        public static List<RequestPackDto> ToRequestPackDto(this IEnumerable<RequestPack> model)
        {
            if (model == null) return null;

            return model.Select(x => new RequestPackDto
            {
                SolicitudId = x.SolicitudId,
                PaqueteId = x.PaqueteId,
                Clave = x.Clave,
                Nombre = x.Nombre,
                ListaPrecioId = x.ListaPrecioId,
                ListaPrecio = x.ListaPrecio,
                PromocionId = x.PromocionId,
                Promocion = x.Promocion,
                Dias = x.Dias,
                Horas = x.Horas,
                DepartamentoId = x.DepartamentoId,
                AreaId = x.AreaId,
                AplicaDescuento = x.AplicaDescuento,
                AplicaCargo = x.AplicaCargo,
                AplicaCopago = x.AplicaCopago,
                Precio = x.Precio,
                Descuento = x.Descuento,
                DescuentoPorcentaje = x.DescuentoPorcentaje,
                PrecioFinal = x.PrecioFinal,
                Estudios = x.Estudios.ToRequestStudyDto()
            }).ToList();
        }

        public static List<RequestStudyDto> ToRequestStudyDto(this IEnumerable<RequestStudy> model)
        {
            if (model == null) return null;

            return model.Select(x => new RequestStudyDto
            {
                SolicitudId = x.SolicitudId,
                EstudioId = x.EstudioId,
                Clave = x.Clave,
                Nombre = x.Nombre,
                PaqueteId = x.PaqueteId,
                ListaPrecioId = x.ListaPrecioId,
                ListaPrecio = x.ListaPrecio,
                PromocionId = x.PromocionId,
                Promocion = x.Promocion,
                EstatusId = x.EstatusId,
                Dias = x.Dias,
                Horas = x.Horas,
                DepartamentoId = x.DepartamentoId,
                AreaId = x.AreaId,
                AplicaDescuento = x.AplicaDescuento,
                AplicaCargo = x.AplicaCargo,
                AplicaCopago = x.AplicaCopago,
                Precio = x.Precio,
                Descuento = x.Descuento,
                DescuentoPorcentaje = x.DescuentoPorcentaje,
                PrecioFinal = x.PrecioFinal,
            }).ToList();
        }

        public static Request ToModel(this RequestDto dto)
        {
            if (dto == null) return null;

            return new Request
            {
                Id = Guid.NewGuid(),
                ExpedienteId = dto.ExpedienteId,
                SucursalId = dto.SucursalId,
                Clave = dto.Clave,
                EsNuevo = true,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now,
            };
        }

        public static List<RequestPack> ToModel(this IEnumerable<RequestPackDto> dto, Guid requestId, IEnumerable<RequestPack> studies, Guid userId)
        {
            if (dto == null) return null;

            return dto.Select(x =>
            {
                var Pack = studies.FirstOrDefault(s => s.PaqueteId == x.PaqueteId);

                return new RequestPack
                {
                    SolicitudId = requestId,
                    PaqueteId = x.PaqueteId,
                    Clave = x.Clave,
                    Nombre = x.Nombre,
                    ListaPrecioId = x.ListaPrecioId,
                    ListaPrecio = x.ListaPrecio,
                    PromocionId = x.PromocionId,
                    Promocion = x.Promocion,
                    DepartamentoId = x.DepartamentoId,
                    AreaId = x.AreaId,
                    AplicaDescuento = x.AplicaDescuento,
                    AplicaCargo = x.AplicaCargo,
                    AplicaCopago = x.AplicaCopago,
                    Dias = x.Dias,
                    Horas = x.Horas,
                    Precio = x.Precio,
                    Descuento = x.Descuento,
                    DescuentoPorcentaje = x.DescuentoPorcentaje,
                    PrecioFinal = x.PrecioFinal,
                    UsuarioCreoId = Pack?.UsuarioCreoId ?? userId,
                    FechaCreo = Pack?.FechaCreo ?? DateTime.Now,
                    UsuarioModificoId = Pack == null ? null : userId,
                    FechaModifico = Pack == null ? null : DateTime.Now
                };
            }).ToList();
        }

        public static List<RequestStudy> ToModel(this IEnumerable<RequestStudyDto> dto, Guid requestId, IEnumerable<RequestStudy> studies, Guid userId)
        {
            if (dto == null) return null;

            return dto.Select(x =>
            {
                var study = studies.FirstOrDefault(s => s.EstudioId == x.EstudioId);

                return new RequestStudy
                {
                    SolicitudId = requestId,
                    EstudioId = x.EstudioId,
                    Clave = x.Clave,
                    Nombre = x.Nombre,
                    PaqueteId = x.PaqueteId,
                    ListaPrecioId = x.ListaPrecioId,
                    ListaPrecio = x.ListaPrecio,
                    PromocionId = x.PromocionId,
                    Promocion = x.Promocion,
                    DepartamentoId = x.DepartamentoId,
                    AreaId = x.AreaId,
                    EstatusId = study?.EstatusId ?? Status.Request.Pendiente,
                    AplicaDescuento = x.AplicaDescuento,
                    AplicaCargo = x.AplicaCargo,
                    AplicaCopago = x.AplicaCopago,
                    Dias = x.Dias,
                    Horas = x.Horas,
                    Precio = x.Precio,
                    Descuento = x.Descuento,
                    DescuentoPorcentaje = x.DescuentoPorcentaje,
                    PrecioFinal = x.PrecioFinal,
                    UsuarioCreoId = study?.UsuarioCreoId ?? userId,
                    FechaCreo = study?.FechaCreo ?? DateTime.Now,
                    UsuarioModificoId = study == null ? null : userId,
                    FechaModifico = study == null ? null : DateTime.Now
                };
            }).ToList();
        }

        public static IEnumerable<RequestStudy> ToModel(this IEnumerable<RequestStudyDto> dto)
        {
            return new List<RequestStudy>();
        }
    }
}
