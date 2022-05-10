using FluentValidation;
using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.Parameter;
using Service.Catalog.Dtos.Catalog;
using Service.Catalog.Dtos.Indication;
using Service.Catalog.Dtos.Parameters;
using Service.Catalog.Dtos.Reagent;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Service.Catalog.Dtos.Study
{
    public class StudyFormDto
    {
          public  int Id {get; set;}
          public string Clave {get; set;}
          public  int Orden {get; set;}
          public string Nombre {get; set;}
          public string Titulo {get; set;}
          public string NombreCorto {get; set;}
          public bool Visible {get; set;}
          public  int Dias {get; set;}
          public bool Activo {get; set;}
          public  int Area {get; set;}
          public  int Departamento {get; set;}
          public  int Formato {get; set;}
          public  int Maquilador {get; set;}
          public  int Metodo {get; set;}
          public  int Tipomuestra {get; set;}
          public  int Tiemporespuesta {get; set;}
          public  int Diasrespuesta {get; set;}
          public  int Tapon {get; set;}
          public  int Cantidad {get; set;}
          public bool Prioridad {get; set;}
          public  bool Urgencia {get; set;}
          public IEnumerable<CatalogListDto> WorkList { get; set; }
          public IEnumerable<ParameterListDto> Parameters { get; set; }
          public IEnumerable<IndicationListDto> Indicaciones { get; set; }
          public IEnumerable<ReagentListDto> Reactivos { get; set; }
          public IEnumerable<CatalogListDto> Paquete { get; set; }

        public Area Areas { get; set; }
        public Domain.Maquila.Maquila Maquila { get; set; }
        public Format Format { get; set; }
        public  Method Method { get; set; }
        public  SampleType SampleType { get; set; }
        public Domain.Tapon.Tapon Tapa { get; set; }
        public Guid UsuarioId { get; set; }
    }
    public class StudyFormDtoValidator : AbstractValidator<StudyFormDto>
    {
        public StudyFormDtoValidator()
        {
            RuleFor(x => x.Clave).NotEmpty().MaximumLength(100);//.NotEqual();
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Orden).NotEmpty();
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Titulo).NotEmpty();
            RuleFor(x => x.NombreCorto).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Visible).NotEmpty();
            RuleFor(x => x.Area).NotEmpty();
            RuleFor(x => x.Departamento).NotEmpty();
            RuleFor(x => x.Prioridad).NotEmpty();
            RuleFor(x => x.Urgencia).NotEmpty();
            RuleFor(x => x.Activo).NotEmpty();

        }

    }
}
