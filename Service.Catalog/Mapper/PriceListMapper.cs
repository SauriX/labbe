using Service.Catalog.Domain.Price;
using Service.Catalog.Dtos;
using Service.Catalog.Dtos.PriceList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Catalog.Mapper
{
    public static class PriceListMapper
    {
        public static PriceListListDto ToPriceListListDto(this PriceList model)
        {
            if (model == null) return null;

            return new PriceListListDto
            {
                Id = model.Id.ToString(),
                Clave = model.Clave,
                Nombre = model.Nombre,
                Activo = model.Activo
            };
        }

        public static IEnumerable<PriceListListDto> ToPriceListListDto(this List<PriceList> model)
        {
            if (model == null) return null;

            return model.Select(x => new PriceListListDto
            {
                Id = x.Id.ToString(),
                Clave = x.Clave,
                Nombre = x.Nombre,
                Activo = x.Activo
            });
        }

        public static PriceListFormDto ToPriceListFormDto(this PriceList model)
        {
            if (model == null) return null;

            return new PriceListFormDto
            {
                Id = model.Id.ToString(),
                Clave = model.Clave.Trim(),
                Nombre = model.Nombre.Trim(),
                Visibilidad = model?.Visibilidad,
                Activo = model.Activo,
               // Estudios = (ICollection<PriceList_Study>)model.Estudios.ToPriceListStudyDto(),
            };
        }

        //private static IEnumerable<PriceListStudyDto> ToPriceListStudyDto(this IEnumerable<PriceList_Study> model)
        //{
        //    //if (model == null) return null;

        //    //return model.Select(x => x.Estudio).Select(x => new PriceListStudyDto
        //    //{
        //    //    Id = x.Id,
        //    //    Nombre = x.Nombre,
        //    //    Area = x.Area.Nombre,
        //    //});
        //}

        public static PriceList ToModel(this PriceListFormDto dto)
        {
            if (dto == null) return null;

            return new PriceList
            {
                Clave = dto.Clave.Trim(),
                Nombre = dto.Nombre.Trim(),
                Visibilidad = dto?.Visibilidad,
                Activo = dto.Activo,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now,
            };
        }

        public static PriceList ToModel(this PriceListFormDto dto, PriceList model)
        {
            if (dto == null || model == null) return null;

            return new PriceList
            {
                Id = model.Id,
                Clave = dto.Clave.Trim(),
                Nombre = dto.Nombre.Trim(),
                Visibilidad = dto?.Visibilidad,
                Activo = dto.Activo,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModificoId = dto.UsuarioId,
                FechaModifico = DateTime.Now,
            };
        }
    }
}
