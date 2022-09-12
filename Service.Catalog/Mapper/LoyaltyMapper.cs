using Service.Catalog.Domain.Loyalty;
using Service.Catalog.Domain.Price;
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
                Id = Guid.NewGuid(),
                Clave = model.Clave,
                Nombre = model.Nombre,
                CantidadDescuento = model.CantidadDescuento,
                Fecha = $"{model.FechaInicial.ToShortDateString()}-{model.FechaFinal.ToShortDateString()}",
                TipoDescuento = model.TipoDescuento.Trim(),
                PrecioListaId = model.PrecioLista.Select(x => x.PrecioLista.Id).ToList(),
                PrecioLista = model.PrecioLista.Select(x => x.PrecioLista.Nombre).ToList(),
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
                PrecioListaId = x.PrecioLista.Select(x => x.PrecioLista.Id).ToList(),
                PrecioLista = x.PrecioLista.Select(x => x.PrecioLista.Nombre).ToList(),
                Activo = x.Activo
            });
        }
        public static LoyaltyListDto ToLoyaltyDto(this Loyalty model)
        {
            if (model == null) return null;

            return  new LoyaltyListDto
            {
                Id = model.Id,
                Clave = model.Clave,
                Nombre = model.Nombre,
                CantidadDescuento = model.CantidadDescuento,
                Fecha = $"{model.FechaInicial.ToShortDateString()}-{model.FechaFinal.ToShortDateString()}",
                TipoDescuento = model.TipoDescuento.Trim(),
                PrecioListaId = model.PrecioLista.Select(x => x.PrecioLista.Id).ToList(),
                PrecioLista = model.PrecioLista.Select(x => x.PrecioLista.Nombre).ToList(),
                Activo = model.Activo
            };
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
                PrecioLista = model.PrecioLista.ToPriceListLoyaltyDto(),
                CantidadDescuento = model.CantidadDescuento,
                Fecha2 = $"{model?.FechaInicial.ToShortDateString()}-{model?.FechaFinal.ToShortDateString()}",
                FechaInicial = model.FechaInicial.Date,
                FechaFinal = model.FechaFinal.Date,
                Activo = model.Activo,
            };
        }

        public static Loyalty ToModelCreate(this LoyaltyFormDto dto)
        {
            if (dto == null) return null;

            return new Loyalty
            {
                Id = Guid.NewGuid(),
                Clave = dto.Clave.Trim(),
                Nombre = dto.Nombre.Trim(),
                TipoDescuento = dto.TipoDescuento.Trim(),
                PrecioLista = dto.PrecioLista.Select(x => new LoyaltyPriceList
                {
                    PrecioListaId = x.PrecioListaId,
                    UsuarioCreoId = dto.UsuarioId,
                    FechaCreo = DateTime.Now,
                }).ToList(),
                CantidadDescuento = dto.CantidadDescuento,
                FechaInicial = dto.FechaInicial.Date,
                FechaFinal = dto.FechaFinal.Date,
                Activo = dto.Activo,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now,
            };
        }

        public static Loyalty ToModelUpdate(this LoyaltyFormDto dto, Loyalty model)
        {
            if (dto == null || model == null) return null;

            return new Loyalty
            {
                Id = model.Id,
                Clave = dto.Clave.Trim(),
                Nombre = dto.Nombre.Trim(),
                TipoDescuento = dto.TipoDescuento.Trim(),
                PrecioLista = dto.PrecioLista.Select(x => new LoyaltyPriceList
                {
                    LoyaltyId = model.Id,
                    PrecioListaId = x.PrecioListaId,
                    UsuarioCreoId = dto.UsuarioId,
                    FechaCreo = DateTime.Now,
                }).ToList(),
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

        private static List<LoyaltyPriceListDto> ToPriceListLoyaltyDto(this IEnumerable<LoyaltyPriceList> price)
        {
            return price.Select(x => new LoyaltyPriceListDto
            {
                LealtadId = x.LoyaltyId,
                PrecioListaId = x.PrecioListaId,
                Nombre = x.PrecioLista.Nombre,
            }).ToList();
        }
    }
}
