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
                tipoValor = parameters.TipoValor,
                estudios = parameters.Estudios?.Select(y => y.Estudio)?.ToList().ToStudyListDtos(),
                areas = parameters.Area,
                reactivos= parameters.Reagent.ToReagentFormDto(),
                format= parameters.Format
                
            };
        }
        public static TipoValor toTipoValor(this ValorTipeForm tipeForm) {  
            return new TipoValor
            {
                IdParametro = Guid.Parse(tipeForm.idParametro),
                Nombre = tipeForm.nombre.ToString(),
                ValorInicial = tipeForm.valorInicial.ToString(),
                ValorFinal = tipeForm.valorFinal.ToString(),
                ValorInicialNumerico = tipeForm.valorInicialNumerico.ToString(),
                ValorFinalNumerico = tipeForm.valorFinalNumerico.ToString(),
                RangoEdadInicial = tipeForm.rangoEdadInicial.ToString(),
                RangoEdadFinal = tipeForm.rangoEdadFinal.ToString(),
                HombreValorInicial = tipeForm.hombreValorInicial.ToString(),
                HombreValorFinal = tipeForm.hombreValorFinal.ToString(),
                MujerValorInicial = tipeForm.mujerValorInicial.ToString(),
                MujerValorFinal = tipeForm.mujerValorFinal.ToString(),
                MedidaTiempo = tipeForm.medidaTiempo.ToString(),
                Opcion = tipeForm.opcion.ToString(),
                DescripcionTexto = tipeForm.descripcionTexto.ToString(),
                DescripcionParrafo = tipeForm.descripcionParrafo.ToString(),
                Activo = true,
                UsuarioCreoId = Guid.NewGuid(),
                FechaCreo = DateTime.Now,
                UsuarioModId = Guid.NewGuid(),
                FechaMod = DateTime.Now,
            };
        }
        public static TipoValor toTipoValorUpdate(this ValorTipeForm tipeForm)
        {
            return new TipoValor
            {
                IdTipo_Valor = Guid.Parse(tipeForm.id),
                IdParametro = Guid.Parse(tipeForm.idParametro),
                Nombre = tipeForm.nombre.ToString(),
                ValorInicial = tipeForm.valorInicial.ToString(),
                ValorFinal = tipeForm.valorFinal.ToString(),
                ValorInicialNumerico = tipeForm.valorInicialNumerico.ToString(),
                ValorFinalNumerico = tipeForm.valorFinalNumerico.ToString(),
                RangoEdadInicial = tipeForm.rangoEdadInicial.ToString(),
                RangoEdadFinal = tipeForm.rangoEdadFinal.ToString(),
                HombreValorInicial = tipeForm.hombreValorInicial.ToString(),
                HombreValorFinal = tipeForm.hombreValorFinal.ToString(),
                MujerValorInicial = tipeForm.mujerValorInicial.ToString(),
                MujerValorFinal = tipeForm.mujerValorFinal.ToString(),
                MedidaTiempo = tipeForm.medidaTiempo.ToString(),
                Opcion = tipeForm.opcion.ToString(),
                DescripcionTexto = tipeForm.descripcionTexto.ToString(),
                DescripcionParrafo = tipeForm.descripcionParrafo.ToString(),
                Activo = true,
                UsuarioCreoId = Guid.NewGuid(),
                FechaCreo = DateTime.Now,
                UsuarioModId = Guid.NewGuid(),
                FechaMod = DateTime.Now,
            };
        }
        public static ValorTipeForm toTipoValorForm(this TipoValor tipeForm)
        {
            return new ValorTipeForm
            {
                 id = tipeForm.IdTipo_Valor.ToString(),
                 idParametro = tipeForm.IdParametro.ToString(),
                 nombre = tipeForm.Nombre,
                 valorInicial = int.Parse(tipeForm.ValorInicial),
                 valorFinal = int.Parse(tipeForm.ValorFinal),
                 valorInicialNumerico = int.Parse(tipeForm.ValorInicialNumerico),
                 valorFinalNumerico = int.Parse(tipeForm.ValorFinalNumerico),
                 rangoEdadInicial = int.Parse(tipeForm.RangoEdadInicial),
                 rangoEdadFinal = int.Parse(tipeForm.RangoEdadFinal),
                 hombreValorInicial = int.Parse(tipeForm.HombreValorInicial),
                 hombreValorFinal = int.Parse(tipeForm.HombreValorFinal),
                 mujerValorInicial = int.Parse(tipeForm.MujerValorInicial),
                 mujerValorFinal = int.Parse(tipeForm.MujerValorFinal),
                 medidaTiempo = int.Parse(tipeForm.MedidaTiempo),
                 opcion = tipeForm.Opcion,
                 descripcionTexto = tipeForm.DescripcionTexto,
                 descripcionParrafo = tipeForm.DescripcionParrafo,

            };
        }
        public static IEnumerable<ValorTipeForm> toTipoValorFormList(this List<TipoValor> model)
        {
            return model.Select(tipeForm=> new ValorTipeForm
            {
                id = tipeForm.IdTipo_Valor.ToString(),
                idParametro = tipeForm.IdParametro.ToString(),
                nombre = tipeForm.Nombre,
                valorInicial = int.Parse(tipeForm.ValorInicial),
                valorFinal = int.Parse(tipeForm.ValorFinal),
                valorInicialNumerico = int.Parse(tipeForm.ValorInicialNumerico),
                valorFinalNumerico = int.Parse(tipeForm.ValorFinalNumerico),
                rangoEdadInicial = int.Parse(tipeForm.RangoEdadInicial),
                rangoEdadFinal = int.Parse(tipeForm.RangoEdadFinal),
                hombreValorInicial = int.Parse(tipeForm.HombreValorInicial),
                hombreValorFinal = int.Parse(tipeForm.HombreValorFinal),
                mujerValorInicial = int.Parse(tipeForm.MujerValorInicial),
                mujerValorFinal = int.Parse(tipeForm.MujerValorFinal),
                medidaTiempo = int.Parse(tipeForm.MedidaTiempo),
                opcion = tipeForm.Opcion,
                descripcionTexto = tipeForm.DescripcionTexto,
                descripcionParrafo = tipeForm.DescripcionParrafo,

            });
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
                FechaMod = DateTime.Now,
                TipoValor = form.tipoValor,
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
                FechaMod = DateTime.Now,
                TipoValor = form.tipoValor
                
            };
        }
    }
}
