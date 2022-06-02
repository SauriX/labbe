using Service.Catalog.Domain.Loyalty;
using Service.Catalog.Dtos.Loyalty;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Catalog.Mapper
{
    public static class LoyaltyMapper
    {
        public static LoyaltyListDto ToLoyaltyListDto(this Loyalty model)
        {
            if (model == null) return null;
            return new LoyaltyListDto
            {
                Id = model.Id,
                Clave = model.Clave,
                Nombre = model.Nombre,
                CantidadDescuento = model.CantidadDescuento,
                Fecha = $"{model.FechaInicial.ToShortDateString()}-{model.FechaFinal.ToShortDateString()}",
                TipoDescuento = model.TipoDescuento.Trim(),
                IdListaPrecios = model?.PrecioListaStg?.Trim(),
                Activo = model.Activo
            };
        }

        public static IEnumerable<LoyaltyListDto> ToLoyaltyListDto(this List<Loyalty> model)
        {
            if (model == null) return null;

            return model.Select(x => new LoyaltyListDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Nombre = x.Nombre,
                CantidadDescuento = x.CantidadDescuento,
                Fecha = $"{x.FechaInicial.ToShortDateString()}-{x.FechaFinal.ToShortDateString()}",
                TipoDescuento = x.TipoDescuento.Trim(),
                IdListaPrecios = x?.PrecioListaStg?.Trim(),
                Activo = x.Activo
            });
        }

        public static LoyaltyFormDto ToLoyaltyFormDto(this Loyalty model)
        {
            if (model == null) return null;

            return new LoyaltyFormDto
            {
                Id = model.Id,
                Clave = model.Clave.Trim(),
                Nombre = model.Nombre.Trim(),
                TipoDescuento = model.TipoDescuento.Trim(),
                IdListaPrecios = model?.PrecioListaStg?.Trim(),
                CantidadDescuento = model.CantidadDescuento,
                Fecha2 = $"{model?.FechaInicial.ToShortDateString()}-{model?.FechaFinal.ToShortDateString()}",
                FechaInicial = model.FechaInicial.Date,
                FechaFinal = model.FechaFinal.Date,
                Activo = model.Activo,
            };
        }

        public static Loyalty ToModel(this LoyaltyFormDto dto)
        {
            if (dto == null) return null;

            return new Loyalty
            {
                Clave = dto.Clave.Trim(),
                Nombre = dto.Nombre.Trim(),
                TipoDescuento = dto.TipoDescuento.Trim(),
                PrecioListaStg = dto?.IdListaPrecios?.Trim(),
                CantidadDescuento = dto.CantidadDescuento,
                FechaInicial = dto.FechaInicial.Date,
                FechaFinal = dto.FechaFinal.Date,
                Activo = dto.Activo,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now,
            };
        }

        public static Loyalty ToModel(this LoyaltyFormDto dto, Loyalty model)
        {
            if (dto == null || model == null) return null;

            return new Loyalty
            {
                Id = model.Id,
                Clave = dto.Clave.Trim(),
                Nombre = dto.Nombre.Trim(),
                TipoDescuento = dto.TipoDescuento.Trim(),
                PrecioListaStg = dto?.IdListaPrecios?.Trim(),
                CantidadDescuento = dto.CantidadDescuento,
                FechaInicial = dto.FechaInicial.Date,
                FechaFinal = dto.FechaFinal.Date,
                Activo = dto.Activo,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModId = dto.UsuarioId,
                FechaMod = DateTime.Now,
            };
        }
    }
}
