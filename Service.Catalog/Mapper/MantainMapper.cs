﻿ using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.EquipmentMantain;
using Service.Catalog.Dtos.Equipmentmantain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Catalog.Mapper
{
    public static class MantainMapper
    {
        public static MantainListDto ToMantainListDto(this Mantain model)
        {
            if (model == null) return null;

            return new MantainListDto
            {
                Id = model.Id,
                Clave = model.clave,
                Fecha = model.Fecha_Prog.ToShortDateString(),
                activo = model.Activo
            };
        }

        public static List<MantainListDto> ToMantainListDto(this List<Mantain > model)
        {
            if (model == null) return null;

            return model.Select(x => new MantainListDto
            {
                Id = x.Id,
                Clave = x.clave,
                Fecha = x.Fecha_Prog.ToShortDateString(),
                activo = x.Activo
            }).ToList();
        }

        public static MantainFormDto ToMaquilaFormDto(this Mantain model)
         {
            if (model == null) return null;
            List<MantainImageDto> images = new List<MantainImageDto>();
            if (model.images != null)
            {
                foreach (var imagen in model.images)
                {
                     imagen.Ruta = imagen.Ruta.Replace("wwwroot/images/mantain", "");
                    var image = new MantainImageDto
                    {
                        Clave = imagen.Clave,
                        ImagenUrl = imagen.Ruta
                    };
                    images.Add(image);
                }
            }
            return new MantainFormDto
            {
                Id = model.Id.ToString(),
                IdEquipo = model.EquipoId,
                Fecha = model.Fecha_Prog,
                Descripcion = model.Descrip,
                imagenUrl = images,
                Clave = model.clave,
                No_serie = model.Num_Serie,

                ide = model.Equipo.Equipment.Id
            };
        }
        public static EquimentDetailDto ToDetailDto(this Equipos model)
        {
            if (model == null) return null;

            return new EquimentDetailDto
            {
                Clave = model.Clave,
                Nombre = model.Nombre,
                Serie = model.Valores.Last().Num_Serie.ToString(),
                id = model.Valores.Last().EquipmentBranchId.ToString(),
            };
        }
        public static Mantain ToModel(this MantainFormDto dto)
        {
            if (dto == null) return null;

            return new Mantain
            {
               
                clave = dto.Clave,
                Fecha_Prog= dto.Fecha,
                Descrip= dto.Descripcion,
                Num_Serie= dto.No_serie,
                EquipoId = dto.IdEquipo,
                Activo = true,
                UsuarioCreoId= dto.IdUser,
                FechaCreo = DateTime.Now,
            };
        }

        public static Mantain ToModel(this MantainFormDto dto, Mantain model)
        {
            if (dto == null || model == null) return null;

            return new Mantain
            {
                Id = model.Id,
                clave = dto.Clave,
                Fecha_Prog = dto.Fecha,
                Descrip = dto.Descripcion,
                Num_Serie = dto.No_serie,
                EquipoId = dto.IdEquipo,
                Activo = dto.Ativo,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModId= dto.IdUser,
                FechaMod=DateTime.Now
            };
        }
    }
}
