using Service.Catalog.Domain.Maquila;
using Service.Catalog.Dtos.Maquilador;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Catalog.Mapper
{
    public static class MaquiladorMapper
    {
        public static MaquilaListDto ToMaquilaListDto(this Maquila model)
        {
            if (model == null) return null;

            return new MaquilaListDto
            {
                Id = model.Id,
                Clave = model.Clave,
                Nombre = model.Nombre,
                Correo = model.Correo,
                Telefono = model.Telefono,
                Direccion = $"{model.Calle} {model.NumeroExterior}, {model.Colonia.Colonia}, {model.Colonia.Ciudad.Ciudad}, {model.Colonia.Ciudad.Estado.Estado}",
                Activo = model.Activo
            };
        }

        public static IEnumerable<MaquilaListDto> ToMaquilaListDto(this List<Maquila> model)
        {
            if (model == null) return null;

            return model.Select(x => new MaquilaListDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Nombre = x.Nombre,
                Correo = x.Correo,
                Telefono = x.Telefono,
                Direccion = $"{x.Calle} {x.NumeroExterior}, {x.Colonia.Colonia}, {x.Colonia.Ciudad.Ciudad}, {x.Colonia.Ciudad.Estado.Estado}",
                Activo = x.Activo
            });
        }

        public static MaquilaFormDto ToMaquilaFormDto(this Maquila model)
        {
            if (model == null) return null;

            return new MaquilaFormDto
            {
                Id = model.Id,
                Clave = model.Clave,
                Nombre = model.Nombre,
                Correo = model.Correo,
                Telefono = model.Telefono,
                PaginaWeb = model.PaginaWeb,
                Calle = model.Calle,
                CodigoPostal = model.Colonia.CodigoPostal,
                ColoniaId = model.ColoniaId,
                Ciudad = model.Colonia.Ciudad.Ciudad,
                Estado = model.Colonia.Ciudad.Estado.Estado,
                NumeroExterior = model.NumeroExterior,
                NumeroInterior = model.NumeroInterior,
                Activo = model.Activo,
            };
        }

        public static Maquila ToModel(this MaquilaFormDto dto)
        {
            if (dto == null) return null;

            return new Maquila
            {
                Id = dto.Id,
                Clave = dto.Clave.Trim(),
                Nombre = dto.Nombre.Trim(),
                NumeroExterior = dto.NumeroExterior.Trim(),
                NumeroInterior = dto.NumeroInterior?.Trim(),
                Calle = dto.Calle.Trim(),
                ColoniaId = dto.ColoniaId,
                Correo = dto.Correo.Trim(),
                PaginaWeb = dto.PaginaWeb?.Trim(),
                Telefono = dto.Telefono,
                Activo = dto.Activo,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now
            };
        }

        public static Maquila ToModel(this MaquilaFormDto dto, Maquila model)
        {
            if (dto == null || model == null) return null;

            return new Maquila
            {
                Id = model.Id,
                Clave = dto.Clave.Trim(),
                Nombre = dto.Nombre.Trim(),
                NumeroExterior = dto.NumeroExterior.Trim(),
                NumeroInterior = dto.NumeroInterior?.Trim(),
                Calle = dto.Calle.Trim(),
                ColoniaId = dto.ColoniaId,
                Correo = dto.Correo.Trim(),
                PaginaWeb = dto.PaginaWeb?.Trim(),
                Telefono = dto.Telefono,
                Activo = dto.Activo,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModificoId = dto.UsuarioId,
                FechaModifico = DateTime.Now
            };
        }
    }
}
