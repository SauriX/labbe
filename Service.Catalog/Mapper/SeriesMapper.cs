using Service.Catalog.Domain.Branch;
using Service.Catalog.Domain.Series;
using Service.Catalog.Dto.Series;
using Service.Catalog.Dtos.Series;
using Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Catalog.Mapper
{
    public static class SeriesMapper
    {
        private const byte TIPO_FACTURA = 1;

        public static IEnumerable<SeriesListDto> ToSeriesListDto(this List<Serie> model)
        {
            if (model == null) return null;

            return model.Select(x => new SeriesListDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Descripcion = x.Descripcion,
                Sucursal = x.Sucursal.Nombre,
                Año = x.FechaCreo.Year.ToString(),
                CFDI = x.CFDI,
                Activo = x.Activo,
                TipoSerie = x.TipoSerie == TIPO_FACTURA ? "FAC" : "REC",
                Tipo = x.TipoSerie,
                Relacion = x.Relacion
            });
        }

        public static Serie ToModelCreate(this SeriesDto dto)
        {
            if (dto == null) return null;

            return new Serie
            {
                Nombre = dto.Factura.Nombre,
                Contraseña = dto.Factura.Contraseña,
                Activo = dto.Factura.Estatus,
                CFDI = dto.Factura.CFDI,
                TipoSerie = dto.Factura.TipoSerie,
                Descripcion = dto.Factura.Observaciones,
                SucursalKey = dto.Factura.SucursalKey,
                Clave = dto.Factura.Clave,
                UsuarioCreoId = dto.UsuarioId,
                SucursalId = Guid.Parse(dto.Expedicion.SucursalId),
                FechaCreo = dto.Factura.Año,
                Relacion = true,
            };
        }

        public static Serie ToTicketCreate(this TicketDto dto)
        {
            if (dto == null) return null;

            return new Serie
            {
                Nombre = dto.Nombre,
                Clave = dto.Clave,
                TipoSerie = dto.TipoSerie,
                FechaCreo = DateTime.Now
            };
        }

        public static Serie ToTicketUpdate(this TicketDto dto, Serie model)
        {
            if (dto == null) return null;

            return new Serie
            {
                Id = model.Id,
                Nombre = dto.Nombre,
                Clave = dto.Clave,
                TipoSerie = model.TipoSerie,
                FechaCreo = model.FechaCreo,
                FechaModifico = DateTime.Now,
            };
        }

        public static Serie ToModelUpdate(this SeriesDto dto, Serie model)
        {
            if (dto == null) return null;

            return new Serie
            {
                Id = model.Id,
                Nombre = dto.Factura.Nombre,
                Contraseña = dto.Factura.Contraseña,
                Activo = dto.Factura.Estatus,
                CFDI = dto.Factura.CFDI,
                Descripcion = dto.Factura.Observaciones,
                SucursalKey = dto.Factura.SucursalKey,
                Clave = dto.Factura.Clave,
                TipoSerie = model.TipoSerie,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = dto.Factura.Año,
                SucursalId = Guid.Parse(dto.Expedicion.SucursalId),
                UsuarioModificoId = dto.UsuarioId,
                FechaModifico = DateTime.Now,
                Relacion = true,
            };
        }

        public static InvoiceSerieDto ToInvoiceSerieDto(this Serie model)
        {
            if (model == null) return null;

            return new InvoiceSerieDto
            {
                Estatus = model.Activo,
                CFDI = model.CFDI,
                Clave = model.Clave,
                Contraseña = model.Contraseña,
                Nombre = model.Nombre,
                Observaciones = model.Descripcion,
            };
        }

        public static ExpeditionPlaceDto ToExpeditionPlaceDto(this Branch branch, string key)
        {
            if (branch == null) return null;

            return new ExpeditionPlaceDto
            {
                Calle = branch.Calle,
                Colonia = branch.Colonia.Colonia,
                Municipio = branch.Ciudad,
                Estado = branch.Estado,
                NoExterior = branch.NumeroExterior,
                NoInterior = branch.NumeroInterior,
                Telefono = branch.Telefono,
                CodigoPostal = branch.Codigopostal,
                SucursalId = branch.Id.ToString(),
                Correo = branch.Correo,
                SucursalKey = Crypto.DecryptString(branch.SucursalKey ?? "", key)
            };
        }
    }
}
