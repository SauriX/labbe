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
                Id = model.Id,
                ExpedienteId = model.ExpedienteId,
                SucursalId = model.SucursalId,
                Clave = model.Clave,
                EsNuevo = model.EsNuevo,
                Registro = $"{model.FechaCreo:dd/MM/yyyy}"
            };
        }

        public static RequestOrderDto ToRequestOrderDto(this Request model)
        {
            if (model == null) return null;

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
            };
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

        public static List<RequestStudy> ToModel(this IEnumerable<RequestStudyDto> dto, Guid requestId, IEnumerable<RequestStudy> studies)
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
                    Paquete = x.Paquete,
                    ListaPrecioId = x.ListaPrecioId,
                    ListaPrecio = x.ListaPrecio,
                    PromocionId = x.PromocionId,
                    Promocion = x.Promocion,
                    EstatusId = study?.EstatusId ?? Status.Request.Pendiente,
                    Descuento = x.Descuento,
                    Cargo = x.Cargo,
                    Copago = x.Copago,
                    Precio = x.Precio,
                    PrecioFinal = x.PrecioFinal
                };
            }).ToList();
        }

        public static IEnumerable<RequestStudy> ToModel(this IEnumerable<RequestStudyDto> dto)
        {
            return new List<RequestStudy>();
        }
    }
}
