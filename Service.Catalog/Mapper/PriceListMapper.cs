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
                Clave = model.Clave,
                Nombre = model.Nombre,
                Activo = model.Activo,
                Compañia = (ICollection<Domain.Company.Price_Company>)model?.Compañia.Select(x => new PriceListCompanyDto
                {
                    Id = x.CompañiaId,
                    Clave = x.Compañia.Clave,
                    Nombre = x.Compañia.NombreComercial,
                }).ToList(),
                Medicos = (ICollection<Price_Medics>)model?.Medicos.Select(x => new PriceListMedicDto
                {
                    Id = x.MedicoId,
                    Clave = x.Medico.Clave,
                    Nombre = x.Medico.Nombre,
                }).ToList(),
                Sucursales = (ICollection<Price_Branch>)model?.Sucursales.Select(x => new PriceListBranchDto
                {
                    Id = x.SucursalId,
                    Clave = x.Sucursal.Clave,
                    Nombre = x.Sucursal.Nombre,
                }).ToList(),
            };
        }
        public static PriceListListDto ToPriceListListComDto(this Price_Company model)
        {
            if (model == null) return null;

            return new PriceListListDto
            {
                    Id = model.CompañiaId,
                    Clave = model.Compañia.Clave,
                    Nombre = model.Compañia.NombreComercial,
                
            };
        }

        public static IEnumerable<PriceListListDto> ToPriceListListDto(this List<PriceList> model)
        {
            if (model == null) return null;

            return model.Select(x => new PriceListListDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Nombre = x.Nombre,
                Activo = x.Activo,
                Estudios = (ICollection<PriceList_Study>)x?.Estudios.Select(x => new PriceListStudyDto
                {
                    Id = x.EstudioId,
                    Clave = x.Estudio.Clave,
                    Nombre = x.Estudio.Nombre,
                    Area = x.Estudio.Area.Nombre,
                    Activo = true,
                }).ToList(),
                Compañia = (ICollection<Domain.Company.Price_Company>)x?.Compañia.Select(x => new PriceListCompanyDto
                {
                    Id = x.CompañiaId,
                    Clave = x.Compañia.Clave,
                    Nombre = x.Compañia.NombreComercial,
                }).ToList(),
                Medicos = (ICollection<Price_Medics>)x?.Medicos.Select(x => new PriceListMedicDto
                {
                    Id = x.MedicoId,
                    Clave = x.Medico.Clave,
                    Nombre = x.Medico.Nombre,
                }).ToList(),
                Sucursales = (ICollection<Price_Branch>)x?.Sucursales.Select(x => new PriceListBranchDto
                {
                    Id = x.SucursalId,
                    Clave = x.Sucursal.Clave,
                    Nombre = x.Sucursal.Nombre,
                }).ToList(),
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
                Estudios = (ICollection<PriceList_Study>)model?.Estudios.Select(x => new PriceListStudyDto
                {
                    Id = x.EstudioId,
                    Clave = x.Estudio.Clave,
                    Nombre = x.Estudio.Nombre,
                    Area = x.Estudio.Area.Nombre,
                    Activo = true,
                }).ToList(),
                Compañia = (ICollection<Domain.Company.Price_Company>)model?.Compañia.Select(x => new PriceListCompanyDto
                {
                    Id = x.CompañiaId,
                    Clave = x.Compañia.Clave,
                    Nombre = x.Compañia.NombreComercial,
                }).ToList(),
                Medicos = (ICollection<Price_Medics>)model?.Medicos.Select(x => new PriceListMedicDto
                {
                    Id = x.MedicoId,
                    Clave = x.Medico.Clave,
                    Nombre = x.Medico.Nombre,
                }).ToList(),
                Sucursales = (ICollection<Price_Branch>)model?.Sucursales.Select(x => new PriceListBranchDto
                {
                    Id = x.SucursalId,
                    Clave = x.Sucursal.Clave,
                    Nombre = x.Sucursal.Nombre,
                }).ToList(),
            };
        }

        public static PriceList ToModel(this PriceListFormDto dto)
        {
            if (dto == null) return null;

            return new PriceList
            {
                Clave = dto.Clave.Trim(),
                Nombre = dto.Nombre.Trim(),
                Visibilidad = dto?.Visibilidad,
                Activo = dto.Activo,
                Estudios = (ICollection<PriceList_Study>)dto.Estudios.Select(x => new PriceListStudyDto
                {
                    Id = x.EstudioId,
                    Clave = x.Estudio.Clave,
                    Nombre = x.Estudio.Nombre,
                    Area = x.Estudio.Area.Nombre,
                    //Precio = x.Estudio.,
                    Activo = true,
                }).ToList(),
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now,
                Compañia = (ICollection<Domain.Company.Price_Company>)dto?.Compañia.Select(x => new PriceListCompanyDto
                {
                    Id = x.CompañiaId,
                    Clave = x.Compañia.Clave,
                    Nombre = x.Compañia.NombreComercial,
                }).ToList(),
                Medicos = (ICollection<Price_Medics>)dto?.Medicos.Select(x => new PriceListMedicDto
                {
                    Id = x.MedicoId,
                    Clave = x.Medico.Clave,
                    Nombre = x.Medico.Nombre,
                }).ToList(),
                Sucursales = (ICollection<Price_Branch>)dto?.Sucursales.Select(x => new PriceListBranchDto
                {
                    Id = x.SucursalId,
                    Clave = x.Sucursal.Clave,
                    Nombre = x.Sucursal.Nombre,
                }).ToList(),

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
                Estudios = (ICollection<PriceList_Study>)model.Estudios.Select(x => new PriceListStudyDto
                {
                    Id = x.EstudioId,
                    Clave = x.Estudio.Clave,
                    Nombre = x.Estudio.Nombre,
                    Area = x.Estudio.Area.Nombre,
                    Activo = true,
                }).ToList(),
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModificoId = dto.UsuarioId,
                FechaModifico = DateTime.Now,
                Compañia = (ICollection<Domain.Company.Price_Company>)dto?.Compañia.Select(x => new PriceListCompanyDto
                {
                    Id = x.CompañiaId,
                    Clave = x.Compañia.Clave,
                    Nombre = x.Compañia.NombreComercial,
                }).ToList(),
                Medicos = (ICollection<Price_Medics>)dto?.Medicos.Select(x => new PriceListMedicDto
                {
                    Id = x.MedicoId,
                    Clave = x.Medico.Clave,
                    Nombre = x.Medico.Nombre,
                }).ToList(),
                Sucursales = (ICollection<Price_Branch>)dto?.Sucursales.Select(x => new PriceListBranchDto
                {
                    Id = x.SucursalId,
                    Clave = x.Sucursal.Clave,
                    Nombre = x.Sucursal.Nombre,
                }).ToList(),

            };
        }
    }
}
