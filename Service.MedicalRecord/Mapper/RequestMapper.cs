﻿using Service.MedicalRecord.Dictionary;
using Service.MedicalRecord.Domain.Request;
using Service.MedicalRecord.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Service.MedicalRecord.Mapper
{
    public static class RequestMapper
    {
        private const byte VIGENTE = 1;
        private const byte PARTICULAR = 2;
        private const byte URGENCIA_NORMAL = 1;
        //private const byte DESCUENTO_PORCENTAJE = 1;
        private const byte DESCUENTO_DINERO = 2;

        public static RequestDto ToRequestDto(this Request model)
        {
            if (model == null) return null;

            return new RequestDto
            {
                SolicitudId = model.Id,
                NombreMedico = model.Medico.Nombre,
                NombreCompania = model.Compañia == null ? "" : model.Compañia.Nombre,
                ClaveMedico = model.Medico.Clave,
                Observaciones = model.Observaciones,
                ExpedienteId = model.ExpedienteId,
                SucursalId = model.SucursalId,
                Clave = model.Clave,
                Parcialidad = model.Parcialidad,
                EsNuevo = model.EsNuevo,
                FolioWeeClinic = model.FolioWeeClinic,
                Registro = $"{model.FechaCreo:dd/MM/yyyy}"
            };
        }

        public static IEnumerable<RequestInfoDto> ToRequestInfoDto(this IEnumerable<Request> model)
        {
            if (model == null) return new List<RequestInfoDto>();

            return model.Select(x => new RequestInfoDto
            {
                SolicitudId = x.Id,
                ExpedienteId = x.ExpedienteId,
                Clave = x.Clave,
                ClavePatologica = x.ClavePatologica,
                Afiliacion = x.Afiliacion,
                Paciente = x.Expediente.NombreCompleto,
                Compañia = x.Procedencia == PARTICULAR ? "Particular" : x.Compañia.Clave,
                Procedencia = x.Procedencia == PARTICULAR ? "Particular" : x.Compañia.Nombre,
                Factura = "",
                Importe = x.TotalEstudios,
                Descuento = x.DescuentoTipo == DESCUENTO_DINERO ? x.Descuento : x.Total * x.Descuento,
                Total = x.Total,
                Saldo = x.Saldo,
                FolioWeeClinic = x.FolioWeeClinic,
                Estudios = x.Estudios.Select(s => new RequestStudyInfoDto
                {
                    Clave = s.Clave,
                    Nombre = s.Nombre,
                    EstatusId = s.EstatusId,
                    Estatus = s.Estatus.Clave,
                    Color = s.Estatus.Color
                })
            });
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

        public static RequestTotalDto ToRequestTotalDto(this Request model)
        {
            if (model == null) return null;

            return new RequestTotalDto
            {
                TotalEstudios = model.TotalEstudios,
                Descuento = model.Descuento,
                DescuentoTipo = model.DescuentoTipo,
                Cargo = model.Cargo,
                CargoTipo = model.CargoTipo,
                Copago = model.Copago,
                CopagoTipo = model.CopagoTipo,
                Total = model.Total,
                Saldo = model.Saldo,
            };
        }

        public static RequestPaymentDto ToRequestPaymentDto(this RequestPayment model)
        {
            if (model == null) return null;

            return new RequestPaymentDto
            {
                Id = model.Id,
                FormaPago = model.FormaPago,
                NumeroCuenta = model.NumeroCuenta,
                FechaPago = model.FechaPago,
                Cantidad = model.Cantidad,
                Serie = model.Serie,
                Numero = model.Numero,
                UsuarioRegistra = model.UsuarioRegistra,
                EstatusId = model.EstatusId
            };
        }

        public static IEnumerable<RequestPaymentDto> ToRequestPaymentDto(this List<RequestPayment> model)
        {
            if (model == null) return null;

            return model.Select(x => x.ToRequestPaymentDto());
        }

        public static RequestOrderDto ToRequestOrderDto(this Request model)
        {
            if (model == null) return null;

            var studies = model.Paquetes?.SelectMany(x => x.Estudios)?.ToList() ?? new List<RequestStudy>();
            studies.AddRange(model.Estudios ?? new List<RequestStudy>());

            return new RequestOrderDto
            {
                Clave = model.Clave,
                Sucursal = model.Sucursal.Nombre,
                FechaVenta = DateTime.Now.ToString("yyyy-MM-dd"),
                FechaSolicitud = model.FechaCreo.ToString("dd/MM/yyyy"),
                Fecha = model.FechaCreo.ToString("dd/MM/yyyy"),
                Personal = model.UsuarioCreo,
                Paciente = model.Expediente.NombreCompleto,
                FechaNacimiento = model.Expediente.FechaDeNacimiento.ToString("dd-MM-yyyy"),
                Edad = model.Expediente.Edad.ToString(),
                Sexo = model.Expediente.Genero,
                TelefonoPaciente = model.EnvioWhatsApp ?? model.Expediente.Telefono,
                Expediente = model.Expediente.Expediente,
                Medico = model.Medico?.Nombre,
                Compañia = model.Procedencia == PARTICULAR ? "Particular" : model.Compañia.Nombre,
                Correo = model.EnvioCorreo,
                Observaciones = model.Observaciones,
                Total = model.Total.ToString("C"),
                Descuento = model.DescuentoTipo == DESCUENTO_DINERO ? model.Descuento.ToString("C") : (model.TotalEstudios * model.Descuento).ToString("C"),
                Cargo = model.CargoTipo == DESCUENTO_DINERO ? model.Cargo.ToString("C") : (model.TotalEstudios * model.Cargo).ToString("C"),
                Estudios = studies.Select(x => new RequestOrderStudyDto
                {
                    Clave = x.Clave,
                    Estudio = x.Nombre,
                    Precio = x.Precio.ToString("C")
                }).ToList()
            };
        }

        public static List<RequestPackDto> ToRequestPackDto(this IEnumerable<RequestPack> model)
        {
            if (model == null) return new List<RequestPackDto>();

            return model.Select(x => new RequestPackDto
            {
                Id = x.Id,
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
            if (model == null) return new List<RequestStudyDto>();

            return model.Select(x => new RequestStudyDto
            {
                Id = x.Id,
                SolicitudId = x.SolicitudId,
                EstudioId = x.EstudioId,
                Clave = x.Clave,
                Nombre = x.Nombre,
                PaqueteId = x.PaqueteId,
                ListaPrecioId = x.ListaPrecioId,
                ListaPrecio = x.ListaPrecio,
                PromocionId = x.PromocionId,
                Promocion = x.Promocion,
                TaponId = x.TaponId,
                TaponColor = x.Tapon.Color,
                TaponClave = x.Tapon.Clave,
                TaponNombre = x.Tapon.Nombre,
                EstatusId = x.EstatusId,
                Estatus = x.Estatus.Nombre,
                Dias = x.Dias,
                Horas = x.Horas,
                FechaEntrega = x.FechaEntrega,
                DepartamentoId = x.DepartamentoId,
                AreaId = x.AreaId,
                AplicaDescuento = x.AplicaDescuento,
                AplicaCargo = x.AplicaCargo,
                AplicaCopago = x.AplicaCopago,
                Precio = x.Precio,
                Descuento = x.Descuento,
                DescuentoPorcentaje = x.DescuentoPorcentaje,
                PrecioFinal = x.PrecioFinal,
                NombreEstatus = x.Estatus.Nombre,
                FechaActualizacion = x.EstatusId == Status.RequestStudy.Capturado
                    ? x.FechaCaptura?.ToString("dd/MM/yyyy HH:mm")
                    : x.EstatusId == Status.RequestStudy.Validado
                    ? x.FechaValidacion?.ToString("dd/MM/yyyy HH:mm")
                    : x.EstatusId == Status.RequestStudy.Liberado
                    ? x.FechaLiberado?.ToString("dd/MM/yyyy HH:mm")
                    : x.EstatusId == Status.RequestStudy.Enviado
                    ? x.FechaEnviado?.ToString("dd/MM/yyyy HH:mm")
                    : "",
                UsuarioActualizacion = x.EstatusId == Status.RequestStudy.Capturado
                    ? x.UsuarioCaptura
                    : x.EstatusId == Status.RequestStudy.Validado
                    ? x.UsuarioValidacion
                    : x.EstatusId == Status.RequestStudy.Liberado
                    ? x.UsuarioLiberado
                    : x.EstatusId == Status.RequestStudy.Enviado
                    ? x.UsuarioEnviado
                    : "",
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
                Procedencia = PARTICULAR,
                Urgencia = URGENCIA_NORMAL,
                EstatusId = VIGENTE,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now,
                Activo = true,
            };
        }

        public static RequestPayment ToModel(this RequestPaymentDto dto)
        {
            if (dto == null) return null;

            return new RequestPayment
            {
                Id = Guid.NewGuid(),
                SolicitudId = dto.SolicitudId,
                FormaPagoId = dto.FormaPagoId,
                FormaPago = dto.FormaPago,
                NumeroCuenta = dto.NumeroCuenta,
                Cantidad = dto.Cantidad,
                Serie = dto.Serie,
                Numero = dto.Numero,
                FechaPago = DateTime.Now,
                EstatusId = Status.RequestPayment.Pagado,
                UsuarioRegistra = dto.UsuarioRegistra,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now,
            };
        }

        public static List<RequestPack> ToModel(this IEnumerable<RequestPackDto> dto, Guid requestId, IEnumerable<RequestPack> packs, Guid userId)
        {
            if (dto == null) return new List<RequestPack>();

            return dto.Select(x =>
            {
                var pack = packs?.FirstOrDefault(s => s.Id == x.Id);

                return new RequestPack
                {
                    Id = x.Id,
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
                    UsuarioCreoId = pack?.UsuarioCreoId ?? userId,
                    FechaCreo = pack?.FechaCreo ?? DateTime.Now,
                    UsuarioModificoId = pack == null ? null : userId,
                    FechaModifico = pack == null ? null : DateTime.Now
                };
            }).ToList();
        }

        public static List<RequestStudy> ToModel(this IEnumerable<RequestStudyDto> dto, Guid requestId, IEnumerable<RequestStudy> studies, Guid userId)
        {
            if (dto == null) return new List<RequestStudy>();

            return dto.Select(x =>
            {
                var study = studies?.FirstOrDefault(s => s.Id == x.Id);

                return new RequestStudy
                {
                    Id = x.Id,
                    SolicitudId = requestId,
                    EstudioId = x.EstudioId,
                    Clave = x.Clave,
                    Nombre = x.Nombre,
                    TaponId = x.TaponId,
                    PaqueteId = x.PaqueteId,
                    ListaPrecioId = x.ListaPrecioId,
                    ListaPrecio = x.ListaPrecio,
                    PromocionId = x.PromocionId,
                    Promocion = x.Promocion,
                    DepartamentoId = x.DepartamentoId,
                    AreaId = x.AreaId,
                    EstatusId = study?.EstatusId ?? Status.RequestStudy.Pendiente,
                    AplicaDescuento = x.AplicaDescuento,
                    AplicaCargo = x.AplicaCargo,
                    AplicaCopago = x.AplicaCopago,
                    Dias = x.Dias,
                    Horas = x.Horas,
                    FechaEntrega = x.FechaEntrega,
                    Precio = x.Precio,
                    Descuento = x.Descuento,
                    DescuentoPorcentaje = x.DescuentoPorcentaje,
                    PrecioFinal = x.PrecioFinal,
                    UsuarioCreoId = study?.UsuarioCreoId ?? userId,
                    FechaCreo = study?.FechaCreo ?? DateTime.Now,
                    UsuarioModificoId = study == null ? null : userId,
                    FechaModifico = study == null ? null : DateTime.Now,
                };
            }).ToList();
        }
    }
}
