using Service.Catalog.Domain.Tags;
using Service.Catalog.Dtos.Tags;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Catalog.Mapper
{
    public static class TagMapper
    {
        public static TagListDto ToTagsListDto(this Tag model)
        {
            if (model == null) return null;
            var Suma = model.Estudios.Select(y => y.Estudio).ToList().ToStudyListDto();
            return new TagListDto
            {
                Id = model.Id,
                Clave = model.Clave.Trim(),
                Nombre = model.Nombre.Trim(),
                NombreCorto = model.NombreCorto.Trim(),
                Cantidad = model.Cantidad,
                Estudios = (ICollection<TagStudy>)(IEnumerable<Dtos.Tags.TagListDto>)Suma,
                Activo = model.Activo

            };
        }
        public static IEnumerable<TagListDto> ToTagsListDto(this List<Tag> model)
        {
            if (model == null) return null;
            return model.Select(x => new TagListDto
            {
                Id = x.Id,
                Clave = x.Clave?.Trim(),
                Nombre = x.Nombre?.Trim(),
                NombreCorto = x.NombreCorto.Trim(),
                Cantidad = x.Cantidad,
                Activo = x.Activo,
                Estudios = (ICollection<TagStudy>)(IEnumerable<Dtos.Tags.TagListDto>)(x.Estudios?.Select(y => y.Estudio)?.ToList()?.ToStudyListDto())
            });
        }
        public static TagFormDto ToTagsFormDto(this Tag model)
        {
            if (model == null) return null;

            return new TagFormDto
            {
                Id = model.Id,
                Clave = model.Clave.Trim(),
                Nombre = model.Nombre.Trim(),
                NombreCorto = model.NombreCorto.Trim(),
                Cantidad = model.Cantidad,
                Activo = model.Activo,
                Estudios = (ICollection<TagStudy>)(model.Estudios?.Select(y => y.Estudio)?.ToList()?.ToStudyListDto())
            };
        }

        public static Tag ToModel(this TagFormDto dto)
        {
            if (dto == null) return null;

            return new Tag
            {
                Clave = dto.Clave.Trim(),
                Nombre = dto.Nombre.Trim(),
                NombreCorto = dto.NombreCorto.Trim(),
                Cantidad = dto.Cantidad,
                Activo = dto.Activo,
                UsuarioCreoId = dto?.UsuarioCreoId,
                FechaCreo = DateTime.Now,
                Estudios = dto.Estudios.Select(x => new TagStudy
                {
                    EstudioId = x.EstudioId,
                    FechaCreo = DateTime.Now,
                    UsuarioCreoId = dto.UsuarioCreoId,
                    FechaMod = DateTime.Now,
                }).ToList(),
            };
        }

        public static Tag ToModel(this TagFormDto dto, Tag model)
        {
            if (model == null) return null;

            return new Tag
            {
                Id = dto.Id,
                Clave = model.Clave,
                Nombre = dto.Nombre.Trim(),
                NombreCorto = dto.NombreCorto.Trim(),
                Cantidad = dto.Cantidad,
                Activo = dto.Activo,
                UsuarioCreoId = model?.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModId = dto.UsuarioModId,
                FechaMod = DateTime.Now,
                Estudios = dto.Estudios.Select(x => new TagStudy
                {
                    TagsId = model.Id,
                    EstudioId = x.EstudioId,
                    FechaCreo = model.FechaCreo,
                    UsuarioCreoId = model.UsuarioCreoId,
                    FechaMod = DateTime.Now,
                }).ToList(),
            };
        }
    }
}
