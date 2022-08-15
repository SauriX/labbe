using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.Equipment;
using Service.Catalog.Dtos.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Catalog.Mapper
{
    public static class EquipmentMapper
    {
        public static EquipmentListDto ToEquipmentListDto(this Equipos model)
        {
            if (model == null) return null;

            return new EquipmentListDto
            {
                Id = model.Id,
                Nombre = model.Nombre,
                Clave = model.Clave,
                Activo = model.Activo
                // falta numero de serie
            };
        }

        public static IEnumerable<EquipmentListDto> ToEquipmentListDto(this IEnumerable<Equipos> model)
        {
            if (model == null) return null;

            return model.Select(x => new EquipmentListDto
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Clave = x.Clave,
                Activo = x.Activo
                // falta numero de serie
            });
        }

        public static EquipmentFormDto ToEquipmentFormDto(this Equipos model)
        {
            if (model == null) return null;

            return new EquipmentFormDto
            {
                Id = model.Id,
                Clave = model.Clave.Trim(),
                Nombre = model.Nombre.Trim(),
                Activo = model.Activo,
            };
        }

        //private static IEnumerable<EquipmentStudyDto> ToEquipmentStudyDto(this IEnumerable<EquipmentStudy> model)
        //{
        //    if (model == null) return null;

        //    return model.Select(x => x.Estudio).Select(x => new EquipmentStudyDto
        //    {
        //        Id = x.Id,
        //        Clave = x.Clave.Trim(),
        //        Nombre = x.Nombre,
        //        Area = x.Area.Nombre,
        //    });
        //}

        public static Equipos ToModel(this EquipmentFormDto dto)
        {
            if (dto == null) return null;

            return new Equipos
            {
                Clave = dto.Clave.Trim(),
                Nombre = dto.Nombre.Trim(),
                Activo = dto.Activo,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now,
                //falta numero de serie
            };
        }

        public static Equipos ToModel(this EquipmentFormDto dto, Equipos model)
        {
            if (dto == null || model == null) return null;

            return new Equipos
            {
                Id = model.Id,
                Clave = dto.Clave.Trim(),
                Nombre = dto.Nombre.Trim(),
                Activo = dto.Activo,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModificoId = dto.UsuarioId,
                FechaModifico = DateTime.Now,
                //falta numero de serie
            };
        }
    }
}
