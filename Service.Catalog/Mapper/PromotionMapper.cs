using Service.Catalog.Domain.Promotion;
using Service.Catalog.Dtos.Promotion;
using System.Collections.Generic;
using System.Linq;

namespace Service.Catalog.Mapper
{
    public static class PromotionMapper
    {
        public static PromotionListDto ToPromotionListDto(this Promotion model)
        {
            if (model == null) return null;
            var listaDeprecios = model.prices.AsQueryable().Where(x => x.Activo == true).FirstOrDefault().Precio.Nombre;
            return new PromotionListDto
            {
                Id = model.Id,
                Clave = model.Clave,
                Nombre = model.Nombre, 
                Periodo = $"{model.FechaInicio}-{model.FechaInicio}",
                NombreListaPrecio = listaDeprecios,
                Activo = model.Activo,
            };
        }
        public static IEnumerable<PromotionListDto> ToPromotionListDto(this List<Promotion> model)
        {
            if (model == null) return null;

            return model.Select(x => new PromotionListDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Nombre = x.Nombre,
                Periodo = $"{x.FechaInicio}-{x.FechaInicio}",
                NombreListaPrecio = x.prices.AsQueryable().Where(x => x.Activo == true).FirstOrDefault().Precio.Nombre,
                Activo = x.Activo,
            });
        }
       /* public static IEnumerable<PromotionEstudioListDto> TopromotionEstudioListDto(this Promotion model)
        { 
            var listaEstudios = model.studies.Select(x => new PromotionEstudioListDto {

                        Id = x.Study.Id,
                        Clave = x.Study.Clave,
                        Nombre = x.Study.Nombre,
                        DescuentoPorcentaje = x.Discountporcent,
                        DescuentoCantidad = x.DiscountNumeric,
                        Lealtad = x.Loyality,
                        FechaInicial = x.FechaInicio,
                        FechaFinal = x.FechaFinal,
                        Activo = x.Activo,
                        Precio = 
                        Paquete

            });
            
        }*/
       /* public static PromotionFormDto ToPromotionFormDto(this Promotion model)
        {
            if (model == null) return null;

            return new PromotionFormDto
            {
                Id = model.Id,
                Clave = model.Clave,
                Nombre= model.Nombre,
                TipoDescuento = model.TipoDeDescuento,
                FechaInicial = model.FechaInicio,
                FechaFinal = model.FechaFinal,
                Activo = model.Activo,
                IdListaPrecios= model.prices.AsQueryable().Where(x => x.Activo == true).FirstOrDefault().Precio.Id.ToString(),
                Lealtad = model.Visibilidad,
                Estudio = model.studies.ToList()
                SucMedCom
            };
        }*/
    }
}
