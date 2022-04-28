using Service.Catalog.Domain.Maquilador;
using Service.Catalog.Dtos.Maquilador;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Catalog.Mapper
{
    public static class MaquiladorMapper
    {
        public static MaquiladorListDto ToMaquiladorListDto(this Maquilador model)
        {
            if (model == null) return null;

            return new MaquiladorListDto
            {
                Id = model.Id,
                Clave = model.Clave.Trim(),
                Nombre = model.Nombre.Trim(),
                Correo = model.Correo.Trim(),
                Telefono = model.Telefono,
                Direccion = $"{model.Calle.Trim()} {model.NumeroExterior} {model.ColoniaId} {model.CiudadId} {model.EstadoId}",
                Activo = model.Activo

            };
        }
        public static IEnumerable<MaquiladorListDto> ToMaquiladorListDto(this List<Maquilador> model)
        {
            if (model == null) return null;
            return model.Select(x => new MaquiladorListDto
            {
                Id = x.Id,
                Clave = x.Clave?.Trim(),
                Nombre = x.Nombre?.Trim(),
                Correo = x.Correo?.Trim(),
                Telefono = x.Telefono,
                Direccion = x.Calle?.Trim() + " " + x.NumeroExterior + " " + x.ColoniaId + " " + x.EstadoId + " " + x.CiudadId + " " + x.CodigoPostal,
                Activo = x.Activo,
            });
        }
        public static MaquiladorFormDto ToMaquiladorFormDto(this Maquilador model)
        {
            if (model == null) return null;
            return new MaquiladorFormDto
            {
                Id = model.Id,
                Clave = model.Clave.Trim(),
                Nombre = model.Nombre.Trim(),
                Correo = model.Correo.Trim(),
                Telefono = model.Telefono,
                PaginaWeb = model.PaginaWeb,
                Calle = model.Calle.Trim(),
                CodigoPostal = model.CodigoPostal,
                EstadoId = model.EstadoId,
                CiudadId = model.CiudadId,
                ColoniaId = model.ColoniaId,
                NumeroExterior = model.NumeroExterior,
                NumeroInterior = model.NumeroInterior,
                Activo = model.Activo,
            };
        }

        public static Maquilador ToModel(this MaquiladorFormDto dto)
        {
            if (dto == null) return null;

            return new Maquilador
            {
                Clave = dto.Clave.Trim(),
                Nombre = dto.Nombre.Trim(),
                CodigoPostal = dto.CodigoPostal,
                EstadoId = dto.EstadoId,
                CiudadId = dto.CiudadId,
                NumeroExterior = dto.NumeroExterior,
                NumeroInterior = dto.NumeroInterior,
                Calle = dto.Calle.Trim(),
                ColoniaId = dto.ColoniaId,
                Correo = dto.Correo.Trim(),
                PaginaWeb = dto.PaginaWeb,
                Telefono = dto.Telefono,
                Activo = dto.Activo,
                UsuarioCreoId = dto.UsuarioCreoId,
                FechaCreo = DateTime.Now,
            };
        }

        public static Maquilador ToModel(this MaquiladorFormDto dto, Maquilador model)
        {
            if (model == null) return null;

            return new Maquilador
            {
                Id = dto.Id,
                Clave = model.Clave,
                Nombre = dto.Nombre.Trim(),
                CodigoPostal = dto.CodigoPostal,
                EstadoId = dto.EstadoId,
                CiudadId = dto.CiudadId,
                NumeroExterior = dto.NumeroExterior,
                NumeroInterior = dto.NumeroInterior,
                Calle = dto.Calle.Trim(),
                ColoniaId = dto.ColoniaId,
                Correo = dto.Correo.Trim(),
                PaginaWeb = dto.PaginaWeb,
                Telefono = dto.Telefono,
                Activo = dto.Activo,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModId = dto.UsuarioModId,
                FechaMod = DateTime.Now,
            };
        }
    }
}
