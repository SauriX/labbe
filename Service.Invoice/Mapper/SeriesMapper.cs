using Service.Billing.Domain.Catalogs;
using Service.Billing.Domain.Series;
using Service.Billing.Dto.Series;
using Service.Billing.Dtos.Series;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Service.Billing.Mapper
{
    public static class SeriesMapper
    {
        private const byte TIPO_FACTURA = 1;

        public static IEnumerable<SeriesListDto> ToSeriesListDto(this List<Series> model)
        {
            if (model == null) return null;

            return model.Select(x => new SeriesListDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Descripcion = x.Descripcion,
                Sucursal = x.Sucursal,
                Año = x.FechaCreo.Year.ToString(),
                CFDI = x.CFDI,
                Activo = x.Activo,
                TipoSerie = x.TipoSerie == TIPO_FACTURA ? "FAC" : "REC",
                Tipo = x.TipoSerie
            });
        }

        public static Series ToModelCreate(this SeriesDto dto)
        {
            if (dto == null) return null;

            return new Series
            {
                Nombre = dto.Factura.Nombre,
                Contraseña = dto.Factura.Contraseña,
                Activo = dto.Factura.Estatus,
                CFDI = dto.Factura.CFDI,
                TipoSerie = dto.Factura.TipoSerie,
                Descripcion = dto.Factura.Observaciones,
                SucursalKey = dto.Factura.SucursalKey,
                Clave = dto.Factura.Clave,
                EmisorId = Guid.Parse("698E416E-E5CB-4E7A-90ED-74448D408F20"),
                UsuarioCreoId = dto.UsuarioId,
                SucursalId = Guid.Parse(dto.Expedicion.SucursalId),
                FechaCreo = dto.Factura.Año,
            };
        }

        public static Series ToTicketCreate(this TicketDto dto)
        {
            if (dto == null) return null;

            return new Series
            {
                Nombre = dto.Nombre,
                Clave = dto.Clave,
                TipoSerie = dto.TipoSerie,
                FechaCreo = DateTime.Now
            };
        }

        public static Series ToTicketUpdate(this TicketDto dto, Series model)
        {
            if (dto == null) return null;

            return new Series
            {
                Id = model.Id,
                Nombre = dto.Nombre,
                Clave = dto.Clave,
                TipoSerie = model.TipoSerie,
                FechaCreo = model.FechaCreo,
                FechaModifico = DateTime.Now,
            };
        }

        public static Series ToModelUpdate(this SeriesDto dto, Series model)
        {
            if (dto == null) return null;

            return new Series
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
                EmisorId = Guid.Parse("698E416E-E5CB-4E7A-90ED-74448D408F20"),
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = dto.Factura.Año,
                SucursalId = Guid.Parse(dto.Expedicion.SucursalId),
                UsuarioModificoId = dto.UsuarioId,
                FechaModifico = DateTime.Now,
            };
        }

        public static InvoiceSerieDto ToInvoiceSerieDto(this Series model)
        {
            if (model == null) return null;

            return new InvoiceSerieDto
            {
                Estatus = model.Activo,
                CFDI = model.CFDI,
                Clave = model.Clave,
                Contraseña = model.Contraseña,
                Nombre = model.Nombre,
                SucursalKey = model.SucursalKey,
                Observaciones = model.Descripcion,
            };
        }

        public static OwnerInfoDto ToOwnerInfoDto(this BranchInfo branch)
        {
            if (branch == null) return null;

            return new OwnerInfoDto
            {
                Nombre = branch.Nombre,
                Calle = branch.Calle,
                Colonia = branch.Colonia,
                Ciudad = branch.Ciudad,
                Estado = branch.Estado,
                Correo = branch.Correo,
                NoExterior = branch.NumeroExt,
                NoInterior = branch.NumeroInt,
                Telefono = branch.Telefono,
                CodigoPostal = branch.CodigoPostal,
            };
        }

        public static ExpeditionPlaceDto ToExpeditionPlaceDto(this BranchInfo branch)
        {
            if (branch == null) return null;

            return new ExpeditionPlaceDto
            {
                Calle = branch.Calle,
                Colonia = branch.Colonia,
                Municipio = branch.Ciudad,
                Estado = branch.Estado,
                NoExterior = branch.NumeroExt,
                NoInterior = branch.NumeroInt,
                Telefono = branch.Telefono,
                CodigoPostal = branch.CodigoPostal,
            };
        }
    }
}
