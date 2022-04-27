using Service.Catalog.Domain.Parameter;
using Service.Catalog.Dtos.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Catalog.Mapper
{
    public static class ParameterMapper
    {
        public static IEnumerable<ParameterList> ToParameterListDto(this List<Parameters> model)
        {
            if (model == null) return null;

            return model.Select(x => new ParameterList
            {
                id = x.IdParametro.ToString(),
                clave = x.Clave.ToString(),
                nombre = x.Nombre.ToString(),
                nombreCorto = x.NombreCorto.ToString(),
                area = x.Area.Nombre,
                departamento= x.Area.Departamento.Nombre,
                activo = x.Activo
            });
        }
        public static ParameterForm ToParameterFormDto(this Parameters parameters) {
            return new ParameterForm
            {
                id = parameters.IdParametro.ToString(),
                clave = parameters.Clave,
                nombre = parameters.Nombre,
                nombreCorto = parameters.NombreCorto.ToString(),
                unidades = parameters.Unidades.ToString(),
                formula = parameters.Formula.ToString(),
                formato = parameters.Formato.ToString(),
                valorInicial = parameters.ValorInicial.ToString(),
                departamento = parameters.DepartamentId,
                area = parameters.AreaId,
                reactivo = parameters.ReagentId,
                unidadSi = parameters.UnidadSi.ToString(),
                fcs = parameters.FCSI.ToString(),
                activo = parameters.Activo,
                formatoImpresion = parameters.FormatId,
                estudios= parameters.Estudios?.Select(y => y.Estudio)?.ToList().ToStudyListDto()
            };
        }
        public static Parameters toParameters(this ParameterForm form) {
            return new Parameters {
                IdParametro = Guid.NewGuid(),
                Clave = form.clave,
                Nombre = form.nombre,
                ValorInicial = form.valorInicial,
                NombreCorto = form.nombreCorto,
                Unidades = double.Parse(form.unidades),
                Formula = form.formula,
                Formato = form.formato,
                DepartamentId = form.departamento,
                AreaId = form.area,
                FormatId = form.formatoImpresion,
                ReagentId = form.reactivo,
                UnidadSi = form.unidadSi,
                FCSI = form.fcs,
                Activo=form.activo,
                UsuarioCreoId = Guid.NewGuid(),
                FechaCreo =DateTime.Now,
                UsuarioModId= Guid.NewGuid(),
                FechaMod = DateTime.Now
              };
        }

        public static Parameters toParameters(this ParameterForm form,Parameters model)
        {
            return new Parameters
            {
                IdParametro = model.IdParametro,
                Clave = form.clave,
                Nombre = form.nombre,
                ValorInicial = form.valorInicial,
                NombreCorto = form.nombreCorto,
                Unidades = double.Parse(form.unidades),
                Formula = form.formula,
                Formato = form.formato,
                DepartamentId = form.departamento,
                AreaId = form.area,
                FormatId = form.formatoImpresion,
                ReagentId = form.reactivo,
                UnidadSi = form.unidadSi,
                FCSI = form.fcs,
                Activo = form.activo,
                UsuarioCreoId = model.UsuarioCreoId,
                FechaCreo = model.FechaCreo,
                UsuarioModId = Guid.NewGuid(),
                FechaMod = DateTime.Now
            };
        }
    }
}
