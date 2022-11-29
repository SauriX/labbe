using Service.Catalog.Domain.Parameter;
using Service.Catalog.Dtos.Parameter;
using Service.Catalog.Dtos.Reagent;
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
                Area = model.Area?.Nombre,
                Departamento = model.Area?.Departamento?.Nombre,
                Activo = model.Activo,
                Requerido = model.Requerido,
                Unidades = model.UnidadId,
                UnidadNombre = model.Unidad?.Nombre,
                TipoValor = model.TipoValor,
                DeltaCheck = model.DeltaCheck,
                MostrarFormato = model.MostrarFormato,
                ValorInicial = model?.ValorInicial,
                ValorFinal = model.ValorFinal,
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
                Area = x.Area?.Nombre,
                Departamento = x.Area?.Departamento?.Nombre,
                Activo = x.Activo,
                Requerido = x.Requerido,
                Unidades = x.UnidadId,
                UnidadNombre = x.Unidad?.Nombre,
                TipoValor = x.TipoValor,
                DeltaCheck = x.DeltaCheck,
                MostrarFormato = x.MostrarFormato,
                ValorInicial = x.ValorInicial,
                ValorFinal = x.ValorFinal,
            });
        }

        public static IEnumerable<ParameterValueStudyDto> ToParameterValueStudyDto(this IEnumerable<Parameter> model)
        {
            if (model == null) return null;

            return model.Select(x => new ParameterValueStudyDto
            {
                Id = x.Id.ToString(),
                Clave = x.Clave,
                Nombre = x.Nombre,
                NombreCorto = x.NombreCorto,
                Area = x.Area?.Nombre,
                Departamento = x.Area?.Departamento?.Nombre,
                Activo = x.Activo,
                Requerido = x.Requerido,
                Unidades = x.UnidadId,
                UnidadNombre = x.Unidad?.Nombre,
                TipoValor = x.TipoValor,
                Formula = x?.Formula,
                DeltaCheck = x.DeltaCheck,
                MostrarFormato = x.MostrarFormato,
                ValorInicial = x?.ValorInicial,
                ValorFinal = x.ValorFinal,
                TipoValores = x.TipoValores?.Select(x => x.ToParameterValueDto())?.ToList(),
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
                Unidades = model.UnidadId,
                UnidadNombre = model.Unidad?.Nombre,
                Formula = model.Formula,
                ValorInicial = model?.ValorInicial,
                DepartamentoId = model.DepartamentoId,
                AreaId = model.AreaId,
                UnidadSi = model.UnidadSiId,
                UnidadSiNombre = model.UnidadSi?.Nombre,
                Fcsi = model.FCSI,
                Activo = model.Activo,
                Requerido = model.Requerido,
                TipoValor = model.TipoValor,
                Estudios = model.Estudios.ToIndicationStudyDto(),
                Reactivos = model.Reactivos.ToReagentDto(),
                Area = model.Area?.Nombre,
                Departamento = model.Area?.Departamento?.Nombre,
                DeltaCheck = model.DeltaCheck,
                MostrarFormato = model.MostrarFormato,
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

        private static IEnumerable<ReagentListDto> ToReagentDto(this IEnumerable<ParameterReagent> model)
        {
            if (model == null) return null;

            return model.Select(x => x.Reactivo).Select(x => new ReagentListDto
            {
                Id = x.Id.ToString(),
                ClaveSistema = x.ClaveSistema,
                Nombre = x.Nombre,
                NombreSistema = x.NombreSistema
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
                CriticoMinimo = model.CriticoMinimo,
                CriticoMaximo = model.CriticoMaximo,
                HombreCriticoMinimo = model.CriticoMinimoHombre,
                HombreCriticoMaximo = model.CriticoMaximoHombre,
                MujerCriticoMinimo = model.CriticoMinimoMujer,
                MujerCriticoMaximo = model.CriticoMaximoMujer,
                MedidaTiempoId = model.MedidaTiempoId,
                Opcion = model.Opcion,
                DescripcionTexto = model.DescripcionTexto,
                DescripcionParrafo = model.DescripcionParrafo,
                PrimeraColumna = model.PrimeraColumna,
                SegundaColumna = model.SegundaColumna,
                TerceraColumna = model.TerceraColumna,
                CuartaColumna = model.CuartaColumna,
                QuintaColumna = model.QuintaColumna
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
                CriticoMinimo = x.CriticoMinimo,
                CriticoMaximo = x.CriticoMaximo,
                HombreCriticoMinimo = x.CriticoMinimoHombre,
                HombreCriticoMaximo = x.CriticoMaximoHombre,
                MujerCriticoMinimo = x.CriticoMinimoMujer,
                MujerCriticoMaximo = x.CriticoMaximoMujer,
                MedidaTiempoId = x.MedidaTiempoId,
                Opcion = x.Opcion,
                DescripcionTexto = x.DescripcionTexto,
                DescripcionParrafo = x.DescripcionParrafo,
                PrimeraColumna = x.PrimeraColumna,
                SegundaColumna = x.SegundaColumna,
                TerceraColumna = x.TerceraColumna,
                CuartaColumna = x.CuartaColumna,
                QuintaColumna = x.QuintaColumna
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
                CriticoMinimo = dto.CriticoMinimo,
                CriticoMaximo = dto.CriticoMaximo,
                CriticoMinimoHombre = dto.HombreCriticoMinimo,
                CriticoMaximoHombre = dto.HombreCriticoMaximo,
                CriticoMinimoMujer = dto.MujerCriticoMinimo,
                CriticoMaximoMujer = dto.MujerCriticoMaximo,
                MedidaTiempoId = dto.MedidaTiempoId,
                Opcion = dto.Opcion?.ToString(),
                DescripcionTexto = dto.DescripcionTexto?.ToString(),
                DescripcionParrafo = dto.DescripcionParrafo?.ToString(),
                PrimeraColumna = dto?.PrimeraColumna,
                SegundaColumna = dto?.SegundaColumna,
                TerceraColumna = dto?.TerceraColumna,
                CuartaColumna = dto?.CuartaColumna,
                QuintaColumna = dto?.QuintaColumna,
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
                CriticoMinimo = x.CriticoMinimo,
                CriticoMaximo = x.CriticoMaximo,
                CriticoMinimoHombre = x.HombreCriticoMinimo,
                CriticoMaximoHombre = x.HombreCriticoMaximo,
                CriticoMinimoMujer = x.MujerCriticoMinimo,
                CriticoMaximoMujer = x.MujerCriticoMaximo,
                MedidaTiempoId = x.MedidaTiempoId,
                Opcion = x.Opcion?.ToString(),
                DescripcionTexto = x.DescripcionTexto?.ToString(),
                DescripcionParrafo = x.DescripcionParrafo?.ToString(),
                PrimeraColumna = x?.PrimeraColumna,
                SegundaColumna = x?.SegundaColumna,
                TerceraColumna = x?.TerceraColumna,
                CuartaColumna = x?.CuartaColumna,
                QuintaColumna = x?.QuintaColumna,
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
                CriticoMinimo = dto.CriticoMinimo,
                CriticoMaximo = dto.CriticoMaximo,
                CriticoMinimoHombre = dto.HombreCriticoMinimo,
                CriticoMaximoHombre = dto.HombreCriticoMaximo,
                CriticoMinimoMujer = dto.MujerCriticoMinimo,
                CriticoMaximoMujer = dto.MujerCriticoMaximo,
                MedidaTiempoId = dto.MedidaTiempoId,
                Opcion = dto.Opcion.ToString(),
                DescripcionTexto = dto.DescripcionTexto.ToString(),
                DescripcionParrafo = dto.DescripcionParrafo.ToString(),
                PrimeraColumna = dto.PrimeraColumna,
                SegundaColumna = dto.SegundaColumna,
                TerceraColumna = dto.TerceraColumna,
                CuartaColumna = dto.CuartaColumna,
                QuintaColumna = dto.QuintaColumna,
                Activo = true,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModificoId = dto.UsuarioId,
                FechaModifico = DateTime.Now
            };
        }

        public static Parameter ToModelCreate(this ParameterFormDto dto)
        {
            if (dto == null) return null;

            return new Parameter
            {
                Id = Guid.NewGuid(),
                Clave = dto.Clave,
                Nombre = dto.Nombre,
                TipoValor = dto.TipoValor,
                ValorInicial = dto.ValorInicial,
                NombreCorto = dto.NombreCorto,
                UnidadId = dto.Unidades,
                Formula = dto.Formula,
                DepartamentoId = dto.DepartamentoId,
                AreaId = dto.AreaId,
                UnidadSiId = dto.UnidadSi,
                FCSI = dto.Fcsi,
                Activo = dto.Activo,
                Requerido = dto.Requerido,
                UsuarioCreoId = dto.UsuarioId,
                FechaCreo = DateTime.Now,
                Reactivos = dto.Reactivos.Select(x => new ParameterReagent
                {
                    ReactivoId = Guid.Parse(x.Id),
                    UsuarioCreoId = dto.UsuarioId.ToString(),
                    FechaCreo = DateTime.Now
                }).ToList(),
            };
        }

        public static Parameter ToModelUpdate(this ParameterFormDto dto, Parameter model)
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
                UnidadId = dto.Unidades,
                Formula = dto.Formula,
                DepartamentoId = dto.DepartamentoId,
                AreaId = dto.AreaId,
                UnidadSiId = dto.UnidadSi,
                FCSI = dto.Fcsi,
                Activo = dto.Activo,
                Requerido = dto.Requerido,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModificoId = dto.UsuarioId,
                FechaModifico = DateTime.Now,
                DeltaCheck = dto.DeltaCheck,
                MostrarFormato = dto.MostrarFormato,
                Reactivos = dto.Reactivos.Select(x => new ParameterReagent
                {
                    ReactivoId = Guid.Parse(x.Id),
                    ParametroId = model.Id,
                    UsuarioCreoId = dto.UsuarioId.ToString(),
                    UsuarioModId = dto.UsuarioId.ToString(),
                    FechaCreo = DateTime.Now,
                    FechaMod = DateTime.Now,
                }).ToList(),
            };
        }
    }
}
