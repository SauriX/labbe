﻿using Service.Catalog.Domain.Price;
using Service.Catalog.Domain.Study;
using Service.Catalog.Dtos.Pack;
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
                Id = model.Id,
                Clave = model.Clave.Trim(),
                Nombre = model.Nombre.Trim(),
                Visibilidad = model?.Visibilidad,
                Activo = model.Activo,
                Estudios = model?.Estudios?.Select(x => new PriceListStudyDto
                {
                    Id = x.EstudioId,
                    Clave = x.Estudio.Clave,
                    Nombre = x.Estudio.Nombre.Trim(),
                    Area = x.Estudio.Area.Nombre.Trim(),
                    Departamento = x.Estudio.Area.Departamento.Nombre,
                    Precio = x.Precio,
                    Activo = true,
                })?.ToList(),
                Compañia = model?.Compañia?.Select(x => new PriceListCompanyDto
                {
                    Id = x.Id,
                    Clave = x.Compañia.Clave.Trim(),
                    Nombre = x.Compañia.NombreComercial.Trim(),
                    Precio = x.Precio,
                })?.ToList(),
                Medicos = model?.Medicos?.Select(x => new PriceListMedicDto
                {
                    Id = x.MedicoId,
                    Clave = x.Medico.Clave.Trim(),
                    Nombre = x.Medico.Nombre.Trim(),
                    Precio = x.Precio,
                })?.ToList(),
                Sucursales = model?.Sucursales?.Select(x => new PriceListBranchDto
                {
                    Id = x.SucursalId,
                    Clave = x.Sucursal.Clave.Trim(),
                    Nombre = x.Sucursal.Nombre.Trim(),
                    Precio = x.Precio,
                })?.ToList(),
            };
        }
        ///ToPriceList companymedicssucursal Aplication
        public static IEnumerable<PriceListCompanyDto> ToPriceListListComDto(this List<Price_Company> model)
        {
            if (model == null) return null;

            return model.Select(x => new PriceListCompanyDto
            {
                Id = x.Compañia.Id,
                Clave = x.Compañia.Clave,
                Nombre = x.Compañia.NombreComercial,
                Precio = x.Precio,
                ListaPrecio = x.PrecioLista?.Nombre


            });
        }
        public static IEnumerable<PriceListMedicDto> ToPriceListListMedDto(this List<Price_Medics> model)
        {
            if (model == null) return null;

            return model.Select(x => new PriceListMedicDto
            {
                Id = x.MedicoId,
                Clave = x.Medico.Clave,
                Nombre = x.Medico.Nombre,
                Precio = x.Precio,
                ListaPrecio = x.PrecioLista?.Nombre
            });
        }
        public static IEnumerable<PriceListBranchDto> ToPriceListListSucDto(this List<Price_Branch> model)
        {
            if (model == null) return null;

            return model.Select(x => new PriceListBranchDto
            {
                Id = x.SucursalId,
                Clave = x.Sucursal.Clave,
                Nombre = x.Sucursal.Nombre,
                Precio = x.Precio,
                ListaPrecio = x.PrecioLista?.Nombre
            });
        }

        ///ToPriceList companymedicssucursal Aplication arriba
        public static IEnumerable<PriceListListDto> ToPriceListListDto(this List<PriceList> model)
        {
            if (model == null) return null;

            return model.Select(x => new PriceListListDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Nombre = x.Nombre,
                Visibilidad = x?.Visibilidad,
                Activo = x.Activo,
                Estudios = x?.Estudios?.Select(x => new PriceListStudyDto
                {
                    Id = x.EstudioId,
                    Clave = x.Estudio.Clave,
                    Nombre = x.Estudio.Nombre,
                    Area = x.Estudio.Area.Nombre.Trim(),
                    Departamento = x.Estudio.Area.Departamento.Nombre,
                    Precio = x.Precio,
                    Activo = x.Activo,
                })?.ToList(),
                Compañia = x?.Compañia?.Select(y => new PriceListCompanyDto
                {
                    Id = y.CompañiaId,
                    Clave = y.Compañia.Clave,
                    Nombre = y.Compañia.NombreComercial,
                    // Precio = x.Precio,
                    ListaPrecio = x.Nombre
                })?.ToList(),
                Medicos = x?.Medicos?.Select(x => new PriceListMedicDto
                {
                    Id = x.MedicoId,
                    Clave = x.Medico.Clave,
                    Nombre = x.Medico.Nombre,
                    Precio = x.Precio,
                })?.ToList(),
                Sucursales = x?.Sucursales?.Select(x => new PriceListBranchDto
                {
                    Id = x.SucursalId,
                    Clave = x.Sucursal.Clave,
                    Nombre = x.Sucursal.Nombre,
                    Precio = x.Precio,
                })?.ToList(),
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
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = DateTime.Now,
                Estudios = model?.Estudios?.Select(x => new PriceListStudyDto
                {
                    Id = x.EstudioId,
                    Clave = x.Estudio.Clave.Trim(),
                    Nombre = x.Estudio.Nombre.Trim(),
                    Area = x.Estudio.Area.Nombre.Trim(),
                    Departamento = x.Estudio.Area.Departamento.Nombre,
                    Precio = x.Precio,
                    Activo = x.Activo,
                })?.ToList(),
                Paquete = model?.Paquete?.Select(x => new PriceListStudyDto
                {
                    Id = x.PaqueteId,
                    Clave = x.Paquete.Clave.Trim(),
                    Nombre = x.Paquete.Nombre.Trim(),
                    Area = x.Paquete.Area.Nombre.Trim(),
                    Departamento = x.Paquete.Area.Departamento.Nombre,
                    Precio = x.Precio,
                    Activo = x.Activo,
                    Pack = x.Paquete.studies.Select(x => new PackStudyDto
                    {
                        Id = x.EstudioId,
                        Clave = x.Estudio.Clave,
                        Nombre = x.Estudio.Nombre,
                        Area = x.Estudio.Area.Nombre,
                        Activo = true,
                    }).ToList(),
                })?.ToList(),
                Compañia = model?.Compañia?.Select(y => new PriceListCompanyDto
                {
                    Id = y.CompañiaId,
                    Clave = y.Compañia.Clave.Trim(),
                    Nombre = y.Compañia.NombreComercial.Trim(),
                    Precio = y.Precio,
                    Activo = y.Activo,
                    ListaPrecio = model.Nombre
                })?.ToList(),
                Medicos = model?.Medicos?.Select(x => new PriceListMedicDto
                {
                    Id = x.MedicoId,
                    Clave = x.Medico.Clave.Trim(),
                    Nombre = x.Medico.Nombre.Trim(),
                    Precio = x.Precio,
                    Activo = x.Activo,
                    ListaPrecio = model.Nombre
                })?.ToList(),
                Sucursales = model?.Sucursales?.Select(x => new PriceListBranchDto
                {
                    Id = x.SucursalId,
                    Clave = x.Sucursal.Clave.Trim(),
                    Nombre = x.Sucursal.Nombre.Trim(),
                    Precio = x.Precio,
                    Activo = x.Activo,
                    ListaPrecio = model.Nombre
                })?.ToList(),
            };
        }

        public static PriceListInfoStudyDto ToPriceListInfoStudyDto(this PriceList_Study model)
        {
            if (model == null) return null;

            return new PriceListInfoStudyDto
            {
                PrecioListaId = model.PrecioListaId.ToString(),
                EstudioId = model.EstudioId,
                Clave = model.Estudio.Clave,
                Nombre = model.Estudio.Nombre,
                PrecioListaPrecio = model.Precio,
                Parametros = model.Estudio.Parameters.Select(x => x.Parametro).ToParameterListDto(),
                Indicaciones = model.Estudio.Indications.Select(x => x.Indicacion).ToIndicationListDto(),
            };
        }

        public static PriceListInfoPackDto ToPriceListInfoPackDto(this PriceList_Packet model)
        {
            if (model == null) return null;

            return new PriceListInfoPackDto
            {
                PrecioListaId = model.PrecioListaId.ToString(),
                PaqueteId = model.PaqueteId,
                Clave = model.Paquete.Clave,
                Nombre = model.Paquete.Nombre,
                PrecioListaPrecio = model.Precio,
                Estudios = model.Paquete.studies.Select(x => new PriceListInfoStudyDto
                {
                    PrecioListaId = model.PrecioListaId.ToString(),
                    EstudioId = x.EstudioId,
                    Clave = x.Estudio.Clave,
                    Nombre = x.Estudio.Nombre,
                    //PrecioListaPrecio = x.Precio,
                    Parametros = x.Estudio.Parameters.Select(x => x.Parametro).ToParameterListDto(),
                    Indicaciones = x.Estudio.Indications.Select(x => x.Indicacion).ToIndicationListDto(),
                }),
            };
        }

        public static PriceList ToModel(this PriceListFormDto dto)
        {
            if (dto == null) return null;

            return new PriceList
            {

                Clave = dto.Clave?.Trim(),
                Nombre = dto.Nombre?.Trim(),
                Visibilidad = dto?.Visibilidad,
                Activo = dto.Activo,
                UsuarioCreoId = dto.UsuarioCreoId,
                FechaCreo = DateTime.Now,
                Estudios = dto?.Estudios?.Select(x => new PriceList_Study
                {
                    EstudioId = x.Id,
                    Precio = x.Precio,
                    Activo = true,
                })?.ToList(),
                Paquete = dto?.Paquete?.Select(x => new PriceList_Packet
                {
                    PaqueteId = x.Id,
                    Precio = x.Precio,
                    Activo = x.Activo,
                })?.ToList(),
                Compañia = dto?.Compañia?.Select(x => new Price_Company
                {
                    PrecioListaId = x.Id,
                    Activo = x.Activo,
                    CompañiaId = x.Id,
                })?.ToList(),
                Medicos = dto?.Medicos?.Select(x => new Price_Medics
                {
                    PrecioListaId = x.Id,
                    Activo = x.Activo,
                    MedicoId = x.Id,
                })?.ToList(),
                Sucursales = dto?.Sucursales?.Select(x => new Price_Branch
                {
                    PrecioListaId = x.Id,
                    Activo = x.Activo,
                    SucursalId = x.Id,
                })?.ToList(),

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
                FechaCreo = DateTime.Now,
                UsuarioModificoId = dto.UsuarioId,
                FechaModifico = DateTime.Now,
                Estudios = dto?.Estudios?.Select(x => new PriceList_Study
                {
                    PrecioListaId = model.Id,
                    Id = x.Id,
                    EstudioId = x.Id,
                    Precio = x.Precio,
                    Activo = x.Activo,
                })?.ToList(),
                Paquete = dto?.Paquete?.Select(x => new PriceList_Packet
                {
                    PrecioListaId = model.Id,
                    PaqueteId = x.Id,
                    Precio = x.Precio,
                    Activo = x.Activo,
                })?.ToList(),
                Compañia = dto?.Compañia?.Select(x => new Price_Company
                {
                    PrecioListaId = model.Id,
                    Activo = x.Activo,
                    CompañiaId = x.Id,
                })?.ToList(),
                Medicos = dto?.Medicos?.Select(x => new Price_Medics
                {
                    PrecioListaId = model.Id,
                    Activo = x.Activo,
                    MedicoId = x.Id,

                })?.ToList(),
                Sucursales = dto?.Sucursales?.Select(x => new Price_Branch
                {
                    PrecioListaId = model.Id,
                    SucursalId = x.Id,
                    Activo = x.Activo,
                })?.ToList(),

            };
        }
    }
}
