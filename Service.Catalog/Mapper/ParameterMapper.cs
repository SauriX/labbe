using Service.Catalog.Domain.Parameter;
using Service.Catalog.Dtos.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Catalog.Mapper
{
    public static class ParameterMapper
    {
        public static ParameterListDto ToParameterListDto(this Parameter model)
        {
            if (model == null) return null;

            return new ParameterListDto
            {
                Id = model.Id.ToString(),
                Clave = model.Clave,
                Nombre = model.Nombre,
                NombreCorto = model.NombreCorto,
                Area = model.Area.Nombre,
                Departamento = model.Area.Departamento.Nombre,
                Activo = model.Activo
            };
        }

        public static IEnumerable<ParameterListDto> ToParameterListDto(this IEnumerable<Parameter> model)
        {
            if (model == null) return null;

            return model.Select(x => new ParameterListDto
            {
                Id = x.Id.ToString(),
                Clave = x.Clave,
                Nombre = x.Nombre,
                NombreCorto = x.NombreCorto,
                Area = x.Area.Nombre,
                Departamento = x.Area.Departamento.Nombre,
                Activo = x.Activo
            });
        }

        public static ParameterFormDto ToParameterFormDto(this Parameter model)
        {
            if (model == null) return null;

            return new ParameterFormDto
            {
                Id = model.Id.ToString(),
                Clave = model.Clave,
                Nombre = model.Nombre,
                NombreCorto = model.NombreCorto,
                Unidades = model.Unidades,
                Formula = model.Formula,
                ValorInicial = model.ValorInicial,
                DepartamentoId = model.DepartamentoId,
                AreaId = model.AreaId,
                ReactivoId = model.ReactivoId.ToString(),
                UnidadSi = model.UnidadSi,
                Fcsi = model.FCSI,
                Activo = model.Activo,
                FormatoImpresionId = model.FormatoImpresionId,
                TipoValor = model.TipoValor,
                Estudios = model.Estudios.ToIndicationStudyDto(),
                Area = model.Area.Nombre,
                Departamento = model.Area.Departamento.Nombre,
                Reactivos = model.Reactivo.ToReagentFormDto().Nombre,
                Format = model.FormatoImpresion.Nombre
            };
        }

        private static IEnumerable<ParameterStudyDto> ToIndicationStudyDto(this IEnumerable<ParameterStudy> model)
        {
            if (model == null) return null;

            return model.Select(x => x.Estudio).Select(x => new ParameterStudyDto
            {
                Id = x.Clave,
                Nombre = x.Nombre
            });
        }

        public static ParameterValueDto ToParameterValueDto(this ParameterValue model)
        {
            if (model == null) return null;

            return new ParameterValueDto
            {
                Id = model.Id.ToString(),
                ParametroId = model.ParametroId.ToString(),
                Nombre = model.Nombre,
                ValorInicial = model.ValorInicial,
                ValorFinal = model.ValorFinal,
                ValorInicialNumerico = model.ValorInicialNumerico,
                ValorFinalNumerico = model.ValorFinalNumerico,
                RangoEdadInicial = model.RangoEdadInicial,
                RangoEdadFinal = model.RangoEdadFinal,
                HombreValorInicial = model.HombreValorInicial,
                HombreValorFinal = model.HombreValorFinal,
                MujerValorInicial = model.MujerValorInicial,
                MujerValorFinal = model.MujerValorFinal,
                MedidaTiempoId = model.MedidaTiempoId,
                Opcion = model.Opcion,
                DescripcionTexto = model.DescripcionTexto,
                DescripcionParrafo = model.DescripcionParrafo
            };
        }

        public static IEnumerable<ParameterValueDto> ToParameterValueDto(this List<ParameterValue> model)
        {
            if (model == null) return null;

            return model.Select(x => new ParameterValueDto
            {
                Id = x.Id.ToString(),
                ParametroId = x.ParametroId.ToString(),
                Nombre = x.Nombre,
                ValorInicial = x.ValorInicial,
                ValorFinal = x.ValorFinal,
                ValorInicialNumerico = x.ValorInicialNumerico,
                ValorFinalNumerico = x.ValorFinalNumerico,
                RangoEdadInicial = x.RangoEdadInicial,
                RangoEdadFinal = x.RangoEdadFinal,
                HombreValorInicial = x.HombreValorInicial,
                HombreValorFinal = x.HombreValorFinal,
                MujerValorInicial = x.MujerValorInicial,
                MujerValorFinal = x.MujerValorFinal,
                MedidaTiempoId = x.MedidaTiempoId,
                Opcion = x.Opcion,
                DescripcionTexto = x.DescripcionTexto,
                DescripcionParrafo = x.DescripcionParrafo
            });
        }

        public static ParameterValue ToModel(this ParameterValueDto dto)
        {
            if (dto == null) return null;

            return new ParameterValue
            {
                Id = dto.Id == null ? Guid.NewGuid() : Guid.Parse(dto.Id),
                ParametroId = Guid.Parse(dto.ParametroId),
                Nombre = dto.Nombre,
                ValorInicial = dto.ValorInicial,
                ValorFinal = dto.ValorFinal,
                ValorInicialNumerico = dto.ValorInicialNumerico,
                ValorFinalNumerico = dto.ValorFinalNumerico,
                RangoEdadInicial = dto.RangoEdadInicial,
                RangoEdadFinal = dto.RangoEdadFinal,
                HombreValorInicial = dto.HombreValorInicial,
                HombreValorFinal = dto.HombreValorFinal,
                MujerValorInicial = dto.MujerValorInicial,
                MujerValorFinal = dto.MujerValorFinal,
                MedidaTiempoId = dto.MedidaTiempoId,
                Opcion = dto.Opcion?.ToString(),
                DescripcionTexto = dto.DescripcionTexto?.ToString(),
                DescripcionParrafo = dto.DescripcionParrafo?.ToString(),
                Activo = true,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now,
            };
        }
        public static List<ParameterValue> ToModel(this List<ParameterValueDto> dto)
        {
            if (dto == null) return null;

            return dto.Select(x => new ParameterValue
            {
                Id = x.Id == null ? Guid.NewGuid() : Guid.Parse(x.Id),
                ParametroId = Guid.Parse(x.ParametroId),
                Nombre = x.Nombre,
                ValorInicial = x.ValorInicial,
                ValorFinal = x.ValorFinal,
                ValorInicialNumerico = x.ValorInicialNumerico,
                ValorFinalNumerico = x.ValorFinalNumerico,
                RangoEdadInicial = x.RangoEdadInicial,
                RangoEdadFinal = x.RangoEdadFinal,
                HombreValorInicial = x.HombreValorInicial,
                HombreValorFinal = x.HombreValorFinal,
                MujerValorInicial = x.MujerValorInicial,
                MujerValorFinal = x.MujerValorFinal,
                MedidaTiempoId = x.MedidaTiempoId,
                Opcion = x.Opcion?.ToString(),
                DescripcionTexto = x.DescripcionTexto?.ToString(),
                DescripcionParrafo = x.DescripcionParrafo?.ToString(),
                Activo = true,
                UsuarioCreoId = x.UsuarioId,
                FechaCreo = DateTime.Now,
            }).ToList();
        }
        public static ParameterValue ToModel(this ParameterValueDto dto, ParameterValue model)
        {
            if (dto == null || model == null) return null;

            return new ParameterValue
            {
                Id = model.Id,
                ParametroId = Guid.Parse(dto.ParametroId),
                Nombre = dto.Nombre,
                ValorInicial = dto.ValorInicial,
                ValorFinal = dto.ValorFinal,
                ValorInicialNumerico = dto.ValorInicialNumerico,
                ValorFinalNumerico = dto.ValorFinalNumerico,
                RangoEdadInicial = dto.RangoEdadInicial,
                RangoEdadFinal = dto.RangoEdadFinal,
                HombreValorInicial = dto.HombreValorInicial,
                HombreValorFinal = dto.HombreValorFinal,
                MujerValorInicial = dto.MujerValorInicial,
                MujerValorFinal = dto.MujerValorFinal,
                MedidaTiempoId = dto.MedidaTiempoId,
                Opcion = dto.Opcion.ToString(),
                DescripcionTexto = dto.DescripcionTexto.ToString(),
                DescripcionParrafo = dto.DescripcionParrafo.ToString(),
                Activo = true,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModificoId = dto.UsuarioId,
                FechaModifico = DateTime.Now
            };
        }

        public static Parameter ToModel(this ParameterFormDto dto)
        {
            if (dto == null) return null;

            return new Parameter
            {
                Clave = dto.Clave,
                Nombre = dto.Nombre,
                TipoValor = dto.TipoValor,
                ValorInicial = dto.ValorInicial,
                NombreCorto = dto.NombreCorto,
                Unidades = dto.Unidades,
                Formula = dto.Formula,
                DepartamentoId = dto.DepartamentoId,
                AreaId = dto.AreaId,
                FormatoImpresionId = dto.FormatoImpresionId,
                ReactivoId = Guid.Parse(dto.ReactivoId),
                UnidadSi = dto.UnidadSi,
                FCSI = dto.Fcsi,
                Activo = dto.Activo,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now
            };
        }

        public static Parameter ToModel(this ParameterFormDto dto, Parameter model)
        {
            if (dto == null || model == null) return null;

            return new Parameter
            {
                Id = model.Id,
                Clave = dto.Clave,
                Nombre = dto.Nombre,
                TipoValor = dto.TipoValor,
                ValorInicial = dto.ValorInicial,
                NombreCorto = dto.NombreCorto,
                //Unidades = dto.Unidades,
                Formula = dto.Formula,
                DepartamentoId = dto.DepartamentoId,
                AreaId = dto.AreaId,
                FormatoImpresionId = dto.FormatoImpresionId,
                ReactivoId = Guid.Parse(dto.ReactivoId),
                //UnidadSi = dto.UnidadSi,
                FCSI = dto.Fcsi,
                Activo = dto.Activo,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModificoId = dto.UsuarioId,
                FechaModifico = DateTime.Now
            };
        }
    }
}
