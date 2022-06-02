using Service.Catalog.Domain.Packet;
using Service.Catalog.Domain.Study;
using Service.Catalog.Dtos.Pack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Catalog.Mapper
{
    public static class PackMapper
    {
        public static PackListDto ToPackListDto(this Packet model)
        {
            if (model == null) return null;

            return new PackListDto
            {
                Id = model.Id,
                Clave = model.Clave,
                Nombre = model.Nombre,
                NombreLargo = model.NombreLargo,
                Activo = model.Activo
            };
        }

        public static IEnumerable<PackListDto> ToPackListDto(this List<Packet> model)
        {
            if (model == null) return null;

            return model.Select(x => new PackListDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Nombre = x.Nombre,
                NombreLargo = x.NombreLargo,
                Pack = x.studies.Select(x => new PackStudyDto
                {
                    Id = x.EstudioId,
                    Clave = x.Estudio.Clave,
                    Nombre = x.Estudio.Nombre,
                    Area = x.Estudio.Area.Nombre,
                    Activo = true,
                }).ToList(),
                Activo =x.Activo,
                Departamento= x.Area.Departamento.Nombre,
                Area=x.Area.Nombre
                
            });
        }

        public static PackFormDto ToPackFormDto(this Packet model)
        {
            if (model == null) return null;
            return new PackFormDto
            {
                Id = model.Id,
                Clave = model.Clave,
                Nombre = model.Nombre,
                NombreLargo = model.NombreLargo,
                IdArea = model.AreaId,
                Area = model.Area.Nombre,
                IdDepartamento = model.DepartamentoId,
                Departamento = model.Area.Departamento.Nombre,
                Activo = model.Activo,
                visible = model.Visibilidad,
                Estudio = model.studies.Select(x => new PackStudyDto
                {
                    Id = x.EstudioId,
                    Clave = x.Estudio.Clave,
                    Nombre = x.Estudio.Nombre,
                    Area = x.Estudio.Area.Nombre,
                    Activo = true,
                }).ToList()
            };
        }

        public static Packet ToModel(this PackFormDto dto)
        {
            if (dto == null) return null;
            return new Packet
            {
                Id = dto.Id,
                Clave = dto.Clave,
                Nombre = dto.Nombre,
                Activo = dto.Activo,
                AreaId = dto.IdArea,
                DepartamentoId = dto.IdDepartamento,
                NombreLargo = dto.NombreLargo,
                Visibilidad = dto.visible,
                studies = dto.Estudio.Select(x => new PacketStudy
                {
                    PacketId = dto.Id,
                    EstudioId = x.Id,
                    Activo = x.Activo,
                    UsuarioCreoId = "",
                    FechaCreo = DateTime.Now,
                    UsuarioModId = 0,
                    FechaMod = DateTime.Now,
                }).ToList(),
                UsuarioCreoId = dto.IdUsuario,
                FechaCreo = DateTime.Now,
                UsuarioModificoId = dto.IdUsuario,
                FechaModifico = DateTime.Now,
            };
        }

        public static Packet ToModel(this PackFormDto dto,Packet model)
        {
            if (dto == null) return null;
            return new Packet
            {
                Id = model.Id,
                Clave = dto.Clave,
                Nombre = dto.Nombre,
                Activo = dto.Activo,
                AreaId = dto.IdArea,
                DepartamentoId = dto.IdDepartamento,
                NombreLargo = dto.NombreLargo,
                Visibilidad = dto.visible,
                studies = dto.Estudio.Select(x => new PacketStudy
                {
                    PacketId = dto.Id,
                    EstudioId = x.Id,
                    Activo = x.Activo,
                    UsuarioCreoId = "",
                    FechaCreo = DateTime.Now,
                    UsuarioModId = 0,
                    FechaMod = DateTime.Now,
                }).ToList(),
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModificoId = dto.IdUsuario,
                FechaModifico = DateTime.Now,
            };
        }
    }
}
