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
                Maquilador = x.Maquilador?.Nombre,
                Metodo = x.Metodo?.Nombre,
                Activo = x.Activo,
            });
        }
        public static IEnumerable<StudyListDto> ToStudyListDtos(this List<Study> model, List<int> ids)
        {
            if (model == null) return null;

            List<Study> completeStudies = new();

            foreach(var id in ids)
            {
                Study study = model.Where(x => x.Id == id).FirstOrDefault();
                if(study != null)
                {
                    completeStudies.Add(study);
                }
            }

            return completeStudies.Select(x => new StudyListDto
            {
                Id = x.Id,
                Parametros = x.Parameters.OrderBy(x => x.Orden).Select(y => y.Parametro).ToParameterValueStudyDto(),
                Indicaciones = x.Indications.Select(y => y.Indicacion).ToIndicationListDto(),
                Metodo = x.Metodo?.Nombre,
                Clave = x.Clave,
                Tipo = x.SampleType?.Nombre

            });
        }
        public static IEnumerable<PriceStudyList> toPriceStudyList(this List<Study> models) {
            if (models == null) return null;
            return models.Select(model => new PriceStudyList {
                Id = model.Id,
                EstudioId = model.Id,
                Nombre= model.Nombre,
                Area= model.Area?.Nombre,
                Departamento= model.Area?.Departamento?.Nombre,
                Activo= false,
                Precio= 0,
                Clave= model.Clave,
                
            });
        }       

        public static IEnumerable<StudyTagDto> ToStudyTagDto(this IEnumerable<StudyTag> model)
        {
            if (model == null) return null;

            return model.Select(x => new StudyTagDto
            {
                Id = x.Id,
                EtiquetaId = x.EtiquetaId,
                EstudioId = x.EstudioId,
                ClaveEtiqueta = x.Etiqueta.Clave,
                ClaveInicial = x.Etiqueta.ClaveInicial,
                NombreEtiqueta = x.Etiqueta.Nombre,
                Cantidad = x.Cantidad,
                Color = x.Etiqueta.Color,
                Orden = x.Orden,
                NombreEstudio = x.Nombre ?? x.Estudio.Clave
            }).ToList();
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
                Maquilador = model.MaquiladorId,
                Metodo = model.MetodoId,
                Tipomuestra = model.SampleTypeId,
                Tiemporespuesta = model.TiempoResultado,
                Diasrespuesta = model.DiasResultado,
                Tapon = model.TaponId,
                Cantidad = model.Cantidad,
                Prioridad = model.Prioridad,
                Urgencia = model.Urgencia,
                WorkLists =model.WorkList,
                Parameters = model.Parameters.OrderBy(x => x.Orden).Select(y => y.Parametro).ToList().ToParameterListDto(),
                Indicaciones = model.Indications.Select(y => y.Indicacion).ToList().ToIndicationListDto(),
                Reactivos = model.Reagents.Select(y => y.Reagent).ToList().ToReagentListDto(),
                Paquete = model.Packets.Select(y => y.Packet).ToList().ToCatalogListDto(),
                Areas = model.Area,
                Maquila = model.Maquilador,
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
                Parameters = model.Parameters.Select((x, i)=> new ParameterStudy
                {
                    ParametroId = Guid.Parse(x.Id),
                    EstudioId = study.Id,
                    Activo = true,
                    UsuarioCreoId = Guid.Empty,
                    FechaCreo = DateTime.Now,
                    UsuarioModificoId = Guid.Empty,
                    FechaModifico = DateTime.Now, 
                    Orden = i,
                }).ToList(),
                WorkList = model.WorkLists,
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
                Parameters = model.Parameters.Select((x, i) => new ParameterStudy
                {
                    ParametroId = Guid.Parse(x.Id),
                    Activo = true,
                    UsuarioCreoId = Guid.Empty,
                    FechaCreo = DateTime.Now,
                    UsuarioModificoId = Guid.Empty,
                    FechaModifico = DateTime.Now,
                    Orden = i,
                }).ToList(),
                WorkList = model.WorkLists,
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
