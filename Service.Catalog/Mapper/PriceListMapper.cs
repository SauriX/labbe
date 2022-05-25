using Service.Catalog.Domain.Company;
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
                    Activo = true,
                })?.ToList(),
                Compañia = x?.Compañia?.Select(x => new PriceListCompanyDto
                {
                    Id = x.CompañiaId,
                    Clave = x.Compañia.Clave,
                    Nombre = x.Compañia.NombreComercial,
                    Precio = x.Precio,
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
                    Activo = true,
                })?.ToList(),
                Compañia = model?.Compañia?.Select(x => new PriceListCompanyDto
                {
                    Id = x.CompañiaId,
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
                    EstudioId = x.EstudioId,
                    Precio = x.Precio,
                    Activo = x.Activo,
                })?.ToList(),
                Compañia = model?.Compañia?.Select(x => new Price_Company
                {
                    PrecioListaId = model.Id,
                    Activo = x.Activo,
                    CompañiaId = x.CompañiaId,
                })?.ToList(),
                Medicos = model?.Medicos?.Select(x => new Price_Medics
                {
                    PrecioListaId = model.Id,
                    Activo = x.Activo,
                    MedicoId = x.MedicoId,
                    
                })?.ToList(),
                Sucursales = model?.Sucursales?.Select(x => new Price_Branch
                {
                    PrecioListaId = model.Id,
                    SucursalId = x.SucursalId,
                    Activo = x.Activo,
                })?.ToList(),

            };
        }
    }
}
