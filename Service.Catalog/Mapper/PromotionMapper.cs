using DocumentFormat.OpenXml.Spreadsheet;
using Service.Catalog.Domain.Price;
using Service.Catalog.Domain.Promotion;
using Service.Catalog.Dtos.PriceList;
using Service.Catalog.Dtos.Promotion;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Catalog.Mapper
{
    public static class PromotionMapper
    {
        public static PromotionListDto ToPromotionListDto(this Promotion model)
        {
            if (model == null) return null;

            return new PromotionListDto
            {
                Id = model.Id,
                Clave = model.Clave,
                Nombre = model.Nombre,
                Periodo = $"{model.FechaInicial:dd/MM/yyyy}-{model.FechaFinal:dd/MM/yyyy}",
                ListaPrecio = model.ListaPrecio?.Nombre,
                Activo = model.Activo,
            };
        }

        public static IEnumerable<PromotionListDto> ToPromotionListDto(this List<Promotion> model)
        {
            if (model == null) return new List<PromotionListDto>();

            return model.Select(x => x.ToPromotionListDto());
        }

        public static PromotionFormDto ToPromotionFormDto(this Promotion model)
        {
            if (model == null) return null;

            return new PromotionFormDto
            {
                Id = model.Id,
                Clave = model.Clave,
                Nombre = model.Nombre,
                ListaPrecioId = model.ListaPrecioId,
                TipoDescuento = model.TipoDeDescuento,
                Cantidad = model.Cantidad,
                AplicaMedicos = model.AplicaMedicos,
                FechaInicial = model.FechaInicial,
                FechaFinal = model.FechaFinal,
                Activo = model.Activo,
                Dias = new PromotionDayDto
                {
                    Lunes = model.Lunes,
                    Martes = model.Martes,
                    Miercoles = model.Miercoles,
                    Jueves = model.Jueves,
                    Viernes = model.Viernes,
                    Sabado = model.Sabado,
                    Domingo = model.Domingo
                },
                Medicos = model.Medicos.Select(x => x.MedicoId),
                Sucursales = model.Sucursales.Select(x => x.SucursalId)
            };
        }

        public static IEnumerable<PromotionStudyPackDto> ToPromotionStudyPackDto(this PriceList priceStudyPack, PromotionFormDto promoDto, Promotion model, bool initial)
        {
            var promos = new List<PromotionStudyPackDto>();

            foreach (var price in priceStudyPack.Estudios)
            {
                var promo = model.Estudios.FirstOrDefault(x => x.EstudioId == price.EstudioId);

                var descP = initial ? promo?.DescuentoPorcentaje ?? 0 : promoDto.TipoDescuento == "p" ? promoDto.Cantidad : promoDto.Cantidad * 100 / price.Precio;
                var descQ = initial ? promo?.DescuentoCantidad ?? 0 : promoDto.TipoDescuento == "p" ? price.Precio * promoDto.Cantidad / 100 : promoDto.Cantidad;

                descP = Math.Round(descP, 2);
                descQ = Math.Round(descQ, 2);

                promos.Add(new PromotionStudyPackDto
                {
                    EstudioId = price.EstudioId,
                    Clave = price.Estudio.Clave,
                    Nombre = price.Estudio.Nombre,
                    DescuentoPorcentaje = descP,
                    DescuentoCantidad = descQ,
                    Precio = price.Precio,
                    PrecioFinal = price.Precio - descQ,
                    Area = price.Estudio.Area?.Nombre,
                    AreaId = price.Estudio.AreaId,
                    DepartamentoId = price.Estudio.DepartamentoId,
                    FechaInicial = initial ? promo?.FechaInicial ?? DateTime.Now : promoDto.FechaInicial == DateTime.MinValue ? DateTime.Now : promoDto.FechaInicial,
                    FechaFinal = initial ? promo?.FechaFinal ?? DateTime.Now : promoDto.FechaFinal == DateTime.MinValue ? DateTime.Now : promoDto.FechaFinal,
                    Activo = !initial || (promo?.Activo ?? true),
                    Lunes = initial ? promo?.Lunes ?? true : promoDto.Dias.Lunes,
                    Martes = initial ? promo?.Martes ?? true : promoDto.Dias.Martes,
                    Miercoles = initial ? promo?.Miercoles ?? true : promoDto.Dias.Miercoles,
                    Jueves = initial ? promo?.Jueves ?? true : promoDto.Dias.Jueves,
                    Viernes = initial ? promo?.Viernes ?? true : promoDto.Dias.Viernes,
                    Sabado = initial ? promo?.Sabado ?? true : promoDto.Dias.Sabado,
                    Domingo = initial ? promo?.Domingo ?? true : promoDto.Dias.Domingo,
                });
            }

            foreach (var price in priceStudyPack.Paquetes)
            {
                var promo = model.Paquetes.FirstOrDefault(x => x.PaqueteId == price.PaqueteId);

                var descP = initial ? promo?.DescuentoPorcentaje ?? 0 : promoDto.TipoDescuento == "p" ? promoDto.Cantidad : promoDto.Cantidad * 100 / price.Precio;
                var descQ = initial ? promo?.DescuentoCantidad ?? 0 : promoDto.TipoDescuento == "p" ? price.Precio * promoDto.Cantidad / 100 : promoDto.Cantidad;

                promos.Add(new PromotionStudyPackDto
                {
                    PaqueteId = price.PaqueteId,
                    Clave = price.Paquete.Clave,
                    Nombre = price.Paquete.Nombre,
                    DescuentoPorcentaje = descP,
                    DescuentoCantidad = descQ,
                    Precio = price.Precio,
                    PrecioFinal = price.Precio - descQ,
                    Area = "PAQUETES",
                    AreaId = Catalogs.Area.PAQUETES,
                    DepartamentoId = Catalogs.Department.PAQUETES,
                    FechaInicial = initial ? promo?.FechaInicial ?? DateTime.Now : promoDto.FechaInicial == DateTime.MinValue ? DateTime.Now : promoDto.FechaInicial,
                    FechaFinal = initial ? promo?.FechaFinal ?? DateTime.Now : promoDto.FechaFinal == DateTime.MinValue ? DateTime.Now : promoDto.FechaFinal,
                    Activo = !initial || (promo?.Activo ?? true),
                    Lunes = initial ? promo?.Lunes ?? true : promoDto.Dias.Lunes,
                    Martes = initial ? promo?.Martes ?? true : promoDto.Dias.Martes,
                    Miercoles = initial ? promo?.Miercoles ?? true : promoDto.Dias.Miercoles,
                    Jueves = initial ? promo?.Jueves ?? true : promoDto.Dias.Jueves,
                    Viernes = initial ? promo?.Viernes ?? true : promoDto.Dias.Viernes,
                    Sabado = initial ? promo?.Sabado ?? true : promoDto.Dias.Sabado,
                    Domingo = initial ? promo?.Domingo ?? true : promoDto.Dias.Domingo,
                });
            }

            return promos.OrderBy(x => x.Clave);
        }

        public static Promotion ToModel(this PromotionFormDto dto)
        {
            if (dto == null) return null;

            var now = DateTime.Now;

            return new Promotion
            {
                Id = dto.Id,
                Clave = dto.Clave,
                Nombre = dto.Nombre,
                ListaPrecioId = dto.ListaPrecioId,
                TipoDeDescuento = dto.TipoDescuento,
                Cantidad = dto.Cantidad,
                AplicaMedicos = dto.AplicaMedicos,
                FechaInicial = dto.FechaInicial.Date,
                FechaFinal = dto.FechaFinal.Date,
                Lunes = dto.Dias.Lunes,
                Martes = dto.Dias.Martes,
                Miercoles = dto.Dias.Miercoles,
                Jueves = dto.Dias.Jueves,
                Viernes = dto.Dias.Viernes,
                Sabado = dto.Dias.Sabado,
                Domingo = dto.Dias.Domingo,
                Activo = dto.Activo,
                Sucursales = dto.ToBranchModel(now),
                Medicos = !dto.AplicaMedicos ? new List<PromotionMedic>() : dto.ToMedicModel(now),
                Estudios = dto.Estudios.ToStudyModel(dto.Id, dto.UsuarioId),
                Paquetes = dto.Estudios.ToPackModel(dto.Id, dto.UsuarioId),
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = now,
            };
        }

        public static Promotion ToModel(this PromotionFormDto dto, Promotion model)
        {
            if (dto == null) return null;

            var now = DateTime.Now;

            return new Promotion
            {
                Id = model.Id,
                Clave = dto.Clave,
                Nombre = dto.Nombre,
                ListaPrecioId = dto.ListaPrecioId,
                TipoDeDescuento = dto.TipoDescuento,
                Cantidad = dto.Cantidad,
                AplicaMedicos = dto.AplicaMedicos,
                FechaInicial = dto.FechaInicial,
                FechaFinal = dto.FechaFinal,
                Lunes = dto.Dias.Lunes,
                Martes = dto.Dias.Martes,
                Miercoles = dto.Dias.Miercoles,
                Jueves = dto.Dias.Jueves,
                Viernes = dto.Dias.Viernes,
                Sabado = dto.Dias.Sabado,
                Domingo = dto.Dias.Domingo,
                Activo = dto.Activo,
                Sucursales = dto.ToBranchModel(now, model),
                Medicos = !dto.AplicaMedicos ? new List<PromotionMedic>() : dto.ToMedicModel(now, model),
                Estudios = dto.Estudios.ToStudyModel(dto.Id, dto.UsuarioId, model.Estudios),
                Paquetes = dto.Estudios.ToPackModel(dto.Id, dto.UsuarioId, model.Paquetes),
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModificoId = dto.UsuarioId,
                FechaModifico = now
            };
        }

        private static List<PromotionBranch> ToBranchModel(this PromotionFormDto dto, DateTime now, Promotion model = null)
        {
            return dto.Sucursales.Select(x =>
            {
                var existing = model?.Sucursales?.FirstOrDefault(b => b.SucursalId == x);

                return new PromotionBranch
                {
                    PromocionId = dto.Id,
                    SucursalId = x,
                    UsuarioCreoId = existing != null ? existing.UsuarioCreoId : dto.UsuarioId,
                    FechaCreo = existing != null ? existing.FechaCreo : now
                };
            }).ToList();
        }

        private static List<PromotionMedic> ToMedicModel(this PromotionFormDto dto, DateTime now, Promotion model = null)
        {
            return dto.Medicos.Select(x =>
            {
                var existing = model?.Medicos?.FirstOrDefault(b => b.MedicoId == x);

                return new PromotionMedic
                {
                    PromocionId = dto.Id,
                    MedicoId = x,
                    UsuarioCreoId = existing != null ? existing.UsuarioCreoId : dto.UsuarioId,
                    FechaCreo = existing != null ? existing.FechaCreo : now
                };
            }).ToList();
        }

        public static List<PromotionStudy> ToStudyModel(this IEnumerable<PromotionStudyPackDto> dto, int promoId, Guid userId, IEnumerable<PromotionStudy> promos = null)
        {
            if (dto == null) return new List<PromotionStudy>();

            return dto.Where(x => x.EsEstudio).Select(x =>
            {
                var existing = promos?.FirstOrDefault(p => p.EstudioId == x.EstudioId);

                return new PromotionStudy
                {
                    PromocionId = promoId,
                    EstudioId = (int)x.EstudioId,
                    Precio = x.Precio,
                    PrecioFinal = x.PrecioFinal,
                    DescuentoCantidad = x.DescuentoCantidad,
                    DescuentoPorcentaje = x.DescuentoPorcentaje,
                    Lunes = x.Lunes,
                    Martes = x.Martes,
                    Miercoles = x.Miercoles,
                    Jueves = x.Jueves,
                    Viernes = x.Viernes,
                    Sabado = x.Sabado,
                    Domingo = x.Domingo,
                    Activo = x.Activo,
                    FechaInicial = x.FechaInicial.Date,
                    FechaFinal = x.FechaFinal.Date,
                    UsuarioCreoId = existing?.UsuarioCreoId ?? userId,
                    FechaCreo = existing?.FechaCreo ?? DateTime.Now,
                    UsuarioModificoId = existing == null ? null : userId,
                    FechaModifico = existing == null ? null : DateTime.Now,
                };
            }).ToList();
        }

        public static List<PromotionPack> ToPackModel(this IEnumerable<PromotionStudyPackDto> dto, int promoId, Guid userId, IEnumerable<PromotionPack> promos = null)
        {
            if (dto == null) return new List<PromotionPack>();

            return dto.Where(x => !x.EsEstudio).Select(x =>
            {
                var existing = promos?.FirstOrDefault(p => p.PaqueteId == x.PaqueteId);

                return new PromotionPack
                {
                    PromocionId = promoId,
                    PaqueteId = (int)x.PaqueteId,
                    Precio = x.Precio,
                    PrecioFinal = x.PrecioFinal,
                    DescuentoCantidad = x.DescuentoCantidad,
                    DescuentoPorcentaje = x.DescuentoPorcentaje,
                    Lunes = x.Lunes,
                    Martes = x.Martes,
                    Miercoles = x.Miercoles,
                    Jueves = x.Jueves,
                    Viernes = x.Viernes,
                    Sabado = x.Sabado,
                    Domingo = x.Domingo,
                    Activo = x.Activo,
                    FechaInicial = x.FechaInicial.Date,
                    FechaFinal = x.FechaFinal.Date,
                    UsuarioCreoId = existing?.UsuarioCreoId ?? userId,
                    FechaCreo = existing?.FechaCreo ?? DateTime.Now,
                    UsuarioModificoId = existing == null ? null : userId,
                    FechaModifico = existing == null ? null : DateTime.Now,
                };
            }).ToList();
        }
    }
}
