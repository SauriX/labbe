using Identidad.Api.ViewModels.Medicos;
using Identidad.Api.ViewModels.Menu;
using Service.Catalog.Domain.Catalog;
using Service.Catalog.Dtos.Medicos;
using Service.Catalog.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Identidad.Api.mapper
{
    public static class MedicosMapper
    {
        public static MedicsListDto ToMedicsListDto(this Medics model)
        {
            if (model == null) return null;

            return new MedicsListDto
            {
                IdMedico = model.IdMedico,
                Clave = model.Clave.Trim(),
                NombreCompleto = model.Nombre.Trim(),
                Correo = model.Correo.Trim(),
                Telefono = model.Telefono,
                Celular = model.Celular,
                Direccion = $"{model.Calle.Trim()} {model.NumeroExterior} {model.ColoniaId} {model.CiudadId} {model.EstadoId}",
                Clinicas = model.Clinicas.Select(y => y.Clinica).ToList().ToCatalogListDto(),
                Observaciones = model.Observaciones.Trim(),
                EspecialidadId = model.EspecialidadId,
                Activo = model.Activo

            };
        }
        public static IEnumerable<MedicsListDto> ToMedicsListDto(this List<Medics> model)
        {
            if (model == null) return null;
            return model.Select(x => new MedicsListDto
            {
                IdMedico = x.IdMedico,
                Clave = x.Clave?.Trim(),
                NombreCompleto = x.Nombre?.Trim() + " " + x.PrimerApellido?.Trim() + " " + x.SegundoApellido?.Trim(),
                Correo = x.Correo?.Trim(),
                Telefono = x.Telefono,
                Celular = x.Celular,
                Direccion = x.Calle?.Trim() + " " + x.NumeroExterior + " " + x.ColoniaId + " " + x.EstadoId + " " + x.CiudadId + " " + x.CodigoPostal,
                EspecialidadId = x.EspecialidadId,
                Observaciones = x.Observaciones?.Trim(),
                Activo = x.Activo,
                Clinicas = x.Clinicas?.Select(y => y.Clinica)?.ToList()?.ToCatalogListDto()
            });
        }
        public static MedicsFormDto ToMedicsFormDto(this Medics model)
        {
            if (model == null) return null;
            return new MedicsFormDto
            {
                IdMedico = model.IdMedico,
                Clave = model.Clave.Trim(),
                Nombre = model.Nombre.Trim(),
                PrimerApellido = model.PrimerApellido.Trim(),
                SegundoApellido = model.SegundoApellido.Trim(),
                Correo = model.Correo.Trim(),
                Telefono = model.Telefono,
                Celular = model.Celular,
                Calle = model.Calle.Trim(),
                CodigoPostal = model.CodigoPostal,
                EstadoId = model.EstadoId,
                CiudadId = model.CiudadId,
                ColoniaId = model.ColoniaId,
                NumeroExterior = model.NumeroExterior,
                NumeroInterior = model.NumeroInterior,
                Observaciones = model.Observaciones.Trim(),
                EspecialidadId = model.EspecialidadId,
                Activo = model.Activo,
                Clinicas = model.Clinicas.Select(x => x.Clinica).ToList().ToCatalogListDto()
            };
        }

        public static Medics ToModel(this MedicsFormDto dto)
        {
            if (dto == null) return null;

            return new Medics
            {
                Clave = dto.Clave.Trim(),
                Nombre = dto.Nombre.Trim(),
                PrimerApellido = dto.PrimerApellido.Trim(),
                SegundoApellido = dto.SegundoApellido.Trim(),
                EspecialidadId = dto.EspecialidadId,
                Observaciones = dto.Observaciones.Trim(),
                CodigoPostal = dto.CodigoPostal,
                EstadoId = dto.EstadoId,
                CiudadId = dto.CiudadId,
                NumeroExterior = dto.NumeroExterior,
                NumeroInterior = dto.NumeroInterior,
                Calle = dto.Calle.Trim(),
                ColoniaId = dto.ColoniaId,
                Correo = dto.Correo.Trim(),
                Celular = dto.Celular,
                Telefono = dto.Telefono,
                Activo = dto.Activo,
                UsuarioCreoId = dto.IdUsuario,
                FechaCreo = DateTime.Now
            };
        }

        public static Medics ToModel(this MedicsFormDto dto, Medics model)
        {
            if (model == null) return null;

            return new Medics
            {
                IdMedico = dto.IdMedico,
                Clave = model.Clave,
                Nombre = dto.Nombre.Trim(),
                PrimerApellido = dto.PrimerApellido.Trim(),
                SegundoApellido = dto.SegundoApellido.Trim(),
                EspecialidadId = dto.EspecialidadId,
                Observaciones = dto.Observaciones.Trim(),
                CodigoPostal = dto.CodigoPostal,
                EstadoId = dto.EstadoId,
                CiudadId = dto.CiudadId,
                NumeroExterior = dto.NumeroExterior,
                NumeroInterior = dto.NumeroInterior,
                Calle = dto.Calle.Trim(),
                ColoniaId = dto.ColoniaId,
                Correo = dto.Correo.Trim(),
                Celular = dto.Celular,
                Telefono = dto.Telefono,
                Activo = dto.Activo,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModId = dto.IdUsuario,
                FechaMod = DateTime.Now
            };
        }

    }
}