using Service.Catalog.Domain.Indication;
using Service.Catalog.Domain.Parameter;
using Service.Catalog.Domain.Study;
using Service.Catalog.Dtos.Study;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Catalog.Mapper
{
    public static class StudyMapper
    {
        public static IEnumerable<StudyListDto> ToStudyListDto(this List<Study> model)
        {
            if (model == null) return null;

            return model.Select(x => new StudyListDto
            {
                Id = x.Id,
                Clave = x.Clave,
                Nombre = x.Nombre,
                Titulo = x.Titulo,
                Area = x.Area?.Nombre,
                AreaId = x.Area?.Id,
                Departamento = x.Area?.Departamento?.Nombre,
                Formato = x.Formato?.Nombre,
                Maquilador = x.Maquilador?.Nombre,
                Metodo = x.Metodo?.Nombre,
                Activo = x.Activo,
            });
        }
        public static IEnumerable<StudyListDto> ToStudyListDtos(this List<Study> model)
        {
            if (model == null) return null;

            return model.Select(x => new StudyListDto
            {
                Id = x.Id,
                Parametros = x.Parameters.Select(y => y.Parametro).ToParameterValueStudyDto(),
                Indicaciones = x.Indications.Select(y => y.Indicacion).ToIndicationListDto(),
            });
        }

        public static StudyFormDto ToStudyFormDto(this Study model)
        {
            if (model == null) return null;

            return new StudyFormDto
            {
                Id = model.Id,
                Clave = model.Clave,
                Orden = model.Orden,
                Nombre = model.Nombre,
                Titulo = model.Titulo,
                NombreCorto = model.NombreCorto,
                Visible = model.Visible,
                Dias = model.Dias,
                Activo = model.Activo,
                Area = model.AreaId,
                Departamento = model.DepartamentoId,
                Formato = model.FormatoId,
                Maquilador = model.MaquiladorId,
                Metodo = model.MetodoId,
                Tipomuestra = model.SampleTypeId,
                Tiemporespuesta = model.TiempoResultado,
                Diasrespuesta = model.DiasResultado,
                Tapon = model.TaponId,
                Cantidad = model.Cantidad,
                Prioridad = model.Prioridad,
                Urgencia = model.Urgencia,
                WorkList = model.WorkLists.Select(y => y.WorkList).ToList().ToCatalogListDto(),
                Parameters = model.Parameters.Select(y => y.Parametro).ToList().ToParameterListDto(),
                Indicaciones = model.Indications.Select(y => y.Indicacion).ToList().ToIndicationListDto(),
                Reactivos = model.Reagents.Select(y => y.Reagent).ToList().ToReagentListDto(),
                Paquete = model.Packets.Select(y => y.Packet).ToList().ToCatalogListDto(),
                Areas = model.Area,
                Maquila = model.Maquilador,
                Format = model.Formato,
                Method = model.Metodo,
                SampleType = model.SampleType,
                Tapa = model.Tapon
            };
        }
        public static Study ToModel(this StudyFormDto model, Study study)
        {
            if (model == null) return null;

            return new Study
            {
                Id = study.Id,
                Clave = model.Clave,
                Nombre = model.Nombre,
                Orden = model.Orden,
                Titulo = model.Titulo,
                NombreCorto = model.NombreCorto,
                Visible = model.Visible,
                DiasResultado = model.Diasrespuesta,
                Dias = model.Dias,
                TiempoResultado = model.Tiemporespuesta,
                AreaId = model.Area,
                DepartamentoId = model.Departamento,
                FormatoId = model.Formato,
                MaquiladorId = model.Maquilador,
                MetodoId = model.Metodo,
                SampleTypeId = model.Tipomuestra,
                TaponId = model.Tapon,
                Cantidad = model.Cantidad,
                Prioridad = model.Prioridad,
                Urgencia = model.Urgencia,
                Activo = model.Activo,
                UsuarioCreoId = study.UsuarioCreoId,
                FechaCreo = DateTime.Now,
                UsuarioModificoId = model.UsuarioId,
                FechaModifico = DateTime.Now,
                Parameters = model.Parameters.Select(x => new ParameterStudy
                {
                    ParametroId = Guid.Parse(x.Id),
                    EstudioId = study.Id,
                    Activo = true,
                    UsuarioCreoId = Guid.Empty,
                    FechaCreo = DateTime.Now,
                    UsuarioModificoId = Guid.Empty,
                    FechaModifico = DateTime.Now
                }).ToList(),
                WorkLists = model.WorkList.Select(x => new WorkListStudy
                {
                    WorkListId = x.Id,
                    EstudioId = study.Id,
                    Activo = true,
                    UsuarioCreoId = Guid.Empty,
                    FechaCreo = DateTime.Now,
                    UsuarioModId = Guid.Empty,
                    FechaMod = DateTime.Now
                }).ToList(),
                Indications = model.Indicaciones.Select(x => new IndicationStudy
                {
                    IndicacionId = x.Id,
                    EstudioId = study.Id,
                    Activo = true,
                    UsuarioCreoId = Guid.NewGuid(),
                    FechaCreo = DateTime.Now,
                    UsuarioModificoId = Guid.NewGuid(),
                    FechaModifico = DateTime.Now
                }).ToList(),
                Reagents = model.Reactivos.Select(x => new ReagentStudy
                {
                    ReagentId = Guid.Parse(x.Id),
                    EstudioId = study.Id,
                    Activo = true,
                    UsuarioCreoId = Guid.Empty,
                    FechaCreo = DateTime.Now,
                    UsuarioModId = Guid.Empty,
                    FechaMod = DateTime.Now
                }).ToList(),
            };
        }
        public static Study ToModel(this StudyFormDto model)
        {
            if (model == null) return null;

            return new Study
            {
                Clave = model.Clave,
                Nombre = model.Nombre,
                Orden = model.Orden,
                Titulo = model.Titulo,
                NombreCorto = model.NombreCorto,
                Visible = model.Visible,
                DiasResultado = model.Diasrespuesta,
                Dias = model.Dias,
                TiempoResultado = model.Tiemporespuesta,
                AreaId = model.Area,
                DepartamentoId = model.Departamento,
                FormatoId = model.Formato,
                MaquiladorId = model.Maquilador,
                MetodoId = model.Metodo,
                SampleTypeId = model.Tipomuestra,
                TaponId = model.Tapon,
                Cantidad = model.Cantidad,
                Prioridad = model.Prioridad,
                Urgencia = model.Urgencia,
                Activo = model.Activo,
                UsuarioCreoId = model.UsuarioId,
                FechaCreo = DateTime.Now,
                UsuarioModificoId = model.UsuarioId,
                FechaModifico = DateTime.Now,
                Parameters = model.Parameters.Select(x => new ParameterStudy
                {
                    ParametroId = Guid.Parse(x.Id),
                    Activo = true,
                    UsuarioCreoId = Guid.Empty,
                    FechaCreo = DateTime.Now,
                    UsuarioModificoId = Guid.Empty,
                    FechaModifico = DateTime.Now
                }).ToList(),
                WorkLists = model.WorkList.Select(x => new WorkListStudy
                {
                    WorkListId = x.Id,
                    Activo = true,
                    UsuarioCreoId = Guid.Empty,
                    FechaCreo = DateTime.Now,
                    UsuarioModId = Guid.Empty,
                    FechaMod = DateTime.Now
                }).ToList(),
                Indications = model.Indicaciones.Select(x => new IndicationStudy
                {
                    IndicacionId = x.Id,
                    Activo = true,
                    UsuarioCreoId = Guid.NewGuid(),
                    FechaCreo = DateTime.Now,
                    UsuarioModificoId = Guid.NewGuid(),
                    FechaModifico = DateTime.Now
                }).ToList(),
                Reagents = model.Reactivos.Select(x => new ReagentStudy
                {
                    ReagentId = Guid.Parse(x.Id),
                    Activo = true,
                    UsuarioCreoId = Guid.Empty,
                    FechaCreo = DateTime.Now,
                    UsuarioModId = Guid.Empty,
                    FechaMod = DateTime.Now
                }).ToList(),
            };
        }
    }
}
