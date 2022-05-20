﻿using Service.Catalog.Domain.Promotion;
using Service.Catalog.Dtos.PriceList;
using Service.Catalog.Dtos.Promotion;
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
            var listaDeprecios = model.prices.AsQueryable().Where(x => x.Activo == true).FirstOrDefault().PrecioLista.Nombre;
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
                NombreListaPrecio = x.prices.AsQueryable().Where(x => x.Activo == true).FirstOrDefault().PrecioLista.Nombre,
                Activo = x.Activo,
            });
        }
        public static IEnumerable<PromotionEstudioListDto> TopromotionEstudioListDto(this Promotion model)
        { var estudios = model.prices.AsQueryable().Where(y => y.PromocionId == model.Id && y.Activo == true).FirstOrDefault().PrecioLista.Estudios;
            var listaEstudios = model.studies.Select(x => new PromotionEstudioListDto
            {

                Id = x.Study.Id,
                Clave = x.Study.Clave,
                Nombre = x.Study.Nombre,
                DescuentoPorcentaje = x.Discountporcent,
                DescuentoCantidad = x.DiscountNumeric,
                Lealtad = x.Loyality,
                FechaInicial = x.FechaInicio,
                FechaFinal = x.FechaFinal,
                Activo = x.Activo,
                Precio = estudios.AsQueryable().Where(m=>m.EstudioId==x.StudyId).FirstOrDefault().Precio,
                Paquete = false

            }).ToList();
            var paquetes = model.prices.AsQueryable().Where(y => y.PromocionId == model.Id && y.Activo == true).FirstOrDefault().PrecioLista.Paquete;
            var listaPaquetes = model.packs.Select(x => new PromotionEstudioListDto
            {

                Id = x.Pack.Id,
                Clave = x.Pack.Clave,
                Nombre = x.Pack.Nombre,
                DescuentoPorcentaje = x.Discountporcent,
                DescuentoCantidad = x.DiscountNumeric,
                Lealtad = x.Loyality,
                FechaInicial = x.FechaInicio,
                FechaFinal = x.FechaFinal,
                Activo = x.Activo,
                Precio = paquetes.AsQueryable().Where(m => m.PaqueteId == x.PackId).FirstOrDefault().Precio,
                Paquete = false

            }).ToList();

            listaEstudios.AddRange(listaPaquetes);

            return listaEstudios;
        }
        public static PromotionFormDto ToPromotionFormDto(this Promotion model)
        {
            if (model == null) return null;

            return new PromotionFormDto
            {
                Id = model.Id,
                Clave = model.Clave,
                Nombre = model.Nombre,
                TipoDescuento = model.TipoDeDescuento,
                FechaInicial = model.FechaInicio,
                FechaFinal = model.FechaFinal,
                Activo = model.Activo,
                IdListaPrecios = model.prices.AsQueryable().Where(x => x.Activo == true).FirstOrDefault().PrecioLista.Id.ToString(),
                Lealtad = model.Visibilidad,
                Estudio = model.TopromotionEstudioListDto(),
                Branchs = model.branches.Select(x=>new PriceListBranchDto
                {
                    Id = x.BranchId,
                    Clave = x.Branch.Clave,
                    Nombre = x.Branch.Nombre,
                    Precio=0
                }).ToList(),
            };
        }

        public static Promotion ToModel(this PromotionFormDto dto)
        {
            if (dto == null) return null;

            return new Promotion
            {
                Id = dto.Id,
                Clave = dto.Clave,
                Nombre = dto.Nombre,
                TipoDeDescuento = dto.TipoDescuento,
                CantidadDescuento = dto.Cantidad,
                FechaInicio = dto.FechaInicial,
                FechaFinal = dto.FechaFinal,
                Visibilidad = dto.Lealtad,
                Activo = dto.Activo,
                UsuarioCreoId = dto.UsuarioId.ToString(),
                FechaCreo = DateTime.Now,
                branches = dto.Branchs.Select(x=>new PromotionBranch {
                    PromotionId = dto.Id,
                    BranchId = x.Id,
                    Activo = x.Active,
                    UsuarioCreoId = dto.UsuarioId,
                    FechaCreo = DateTime.Now,
                    UsuarioModId = dto.UsuarioId,
                    FechaMod= DateTime.Now,
                }).ToList(),
                packs = dto.Estudio.Where(x=>x.Paquete==true).Select(x=> new PromotionPack {
                    PromotionId = dto.Id,
                    PackId =x.Id,
                    Discountporcent = x.DescuentoPorcentaje,
                    DiscountNumeric = x.DescuentoCantidad,
                    Price = x.Precio,
                    FinalPrice = x.PrecioFinal,
                    Loyality = x.Lealtad,
                    FechaInicio = x.FechaInicial,
                    FechaFinal = x.FechaFinal,
                    Activo =x.Activo,
                    UsuarioCreoId = dto.UsuarioId,
                    FechaCreo = DateTime.Now,
                    UsuarioModId=dto.UsuarioId,
                    FechaMod = DateTime.Now
                }).ToList(),
                studies = dto.Estudio.Where(x => x.Paquete == false).Select(x => new PromotionStudy
                {
                    PromotionId = dto.Id,
                    StudyId = x.Id,
                    Discountporcent = x.DescuentoPorcentaje,
                    DiscountNumeric = x.DescuentoCantidad,
                    Price = x.Precio,
                    FinalPrice = x.PrecioFinal,
                    Loyality = x.Lealtad,
                    FechaInicio = x.FechaInicial,
                    FechaFinal = x.FechaFinal,
                    Activo = x.Activo,
                    UsuarioCreoId = dto.UsuarioId,
                    FechaCreo = DateTime.Now,
                    UsuarioModId = dto.UsuarioId,
                    FechaMod = DateTime.Now
                }).ToList(),
                PrecioListaId = Guid.Parse(dto.IdListaPrecios)
            };
        }

        public static Promotion ToModel(this PromotionFormDto dto, Promotion model)
        {
            if (dto == null) return null;

            return new Promotion
            {
                Id = model.Id,
                Clave = dto.Clave,
                Nombre = dto.Nombre,
                TipoDeDescuento = dto.TipoDescuento,
                CantidadDescuento = dto.Cantidad,
                FechaInicio = dto.FechaInicial,
                FechaFinal = dto.FechaFinal,
                Visibilidad = dto.Lealtad,
                Activo = dto.Activo,
                UsuarioCreoId = dto.UsuarioId.ToString(),
                FechaCreo = DateTime.Now,
                branches = dto.Branchs.Select(x => new PromotionBranch
                {
                    PromotionId = dto.Id,
                    BranchId = x.Id,
                    Activo = x.Active,
                    UsuarioCreoId = dto.UsuarioId,
                    FechaCreo = DateTime.Now,
                    UsuarioModId = dto.UsuarioId,
                    FechaMod = DateTime.Now,
                }).ToList(),
                packs = dto.Estudio.Where(x => x.Paquete == true).Select(x => new PromotionPack
                {
                    PromotionId = dto.Id,
                    PackId = x.Id,
                    Discountporcent = x.DescuentoPorcentaje,
                    DiscountNumeric = x.DescuentoCantidad,
                    Price = x.Precio,
                    FinalPrice = x.PrecioFinal,
                    Loyality = x.Lealtad,
                    FechaInicio = x.FechaInicial,
                    FechaFinal = x.FechaFinal,
                    Activo = x.Activo,
                    UsuarioCreoId = dto.UsuarioId,
                    FechaCreo = DateTime.Now,
                    UsuarioModId = dto.UsuarioId,
                    FechaMod = DateTime.Now
                }).ToList(),
                studies = dto.Estudio.Where(x => x.Paquete == false).Select(x => new PromotionStudy
                {
                    PromotionId = dto.Id,
                    StudyId = x.Id,
                    Discountporcent = x.DescuentoPorcentaje,
                    DiscountNumeric = x.DescuentoCantidad,
                    Price = x.Precio,
                    FinalPrice = x.PrecioFinal,
                    Loyality = x.Lealtad,
                    FechaInicio = x.FechaInicial,
                    FechaFinal = x.FechaFinal,
                    Activo = x.Activo,
                    UsuarioCreoId = dto.UsuarioId,
                    FechaCreo = DateTime.Now,
                    UsuarioModId = dto.UsuarioId,
                    FechaMod = DateTime.Now
                }).ToList(),
                PrecioListaId = Guid.Parse(dto.IdListaPrecios)
            };
        }
    }
}