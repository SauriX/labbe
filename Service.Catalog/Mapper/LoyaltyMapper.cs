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
            var listaDeprecios = model?.PrecioLista.AsQueryable().Where(x => x.Activo == true).FirstOrDefault().PrecioLista.Nombre;
            return new LoyaltyListDto
            {
                Id = model.Id,
                Clave = model.Clave,
                Nombre = model.Nombre,
                BeneficioAplicado = model.CantidadDescuento,
                Vigencia = $"{model.FechaInicial}-{model.FechaFinal}",
                Promocion = model.TipoDescuento.Trim(),
                NombreListaPrecio = listaDeprecios,
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
                BeneficioAplicado = x.CantidadDescuento,
                Vigencia = $"{x.FechaInicial}-{x.FechaFinal}",
                Promocion = x.TipoDescuento.Trim(),
                NombreListaPrecio = x?.PrecioLista.AsQueryable().Where(x => x.Activo == true).FirstOrDefault().PrecioLista.Nombre.Trim(),
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
                CantidadDescuento= model.CantidadDescuento,
                FechaInicial = model.FechaInicial,
                FechaFinal = model.FechaFinal,
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
                CantidadDescuento = dto.CantidadDescuento,
                FechaInicial = dto.FechaInicial,
                FechaFinal = dto.FechaFinal,
                Activo = dto.Activo,
                //UsuarioCreoId = dto.UsuarioCreoId,
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
                CantidadDescuento = dto.CantidadDescuento,
                FechaInicial = dto.FechaInicial,
                FechaFinal = dto.FechaFinal,
                Activo = dto.Activo,
                //UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                //UsuarioModId = dto.UsuarioModId,
                FechaMod = DateTime.Now,
            };
        }
    }
}
