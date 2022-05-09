using ClosedXML.Excel;
using ClosedXML.Report;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Dictionary;
using Service.Catalog.Dtos.Study;
using Service.Catalog.Mapper;
using Service.Catalog.Repository.IRepository;
using Shared.Dictionary;
using Shared.Error;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Service.Catalog.Application
{
    public class StudyApplication:IStudyApplication
    {
        public readonly IStudyRepository _repository;
        public StudyApplication(IStudyRepository repository)
        {
            _repository = repository;
        }
        public async Task<StudyFormDto> GetById(int Id)
        {
            var estudio = await _repository.GetById(Id);
            if (estudio == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }
            return estudio.ToStudyFormDto();
        }
        public async Task<StudyFormDto> Create(StudyFormDto study)
        {
            var code = await ValidarClaveNombre(study);

            if (code != 0)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave o nombre"));
            }
            if (study.Id != 0)
            {
                throw new CustomException(HttpStatusCode.Conflict, Responses.NotPossible);
            }
            var newestudio = study.ToModel();

            await _repository.Create(newestudio);

            study = await GetById(newestudio.Id);;

            return study;
        }

        public async Task<IEnumerable<StudyListDto>> GetAll(string search = null)
        {
            var estudios = await _repository.GetAll(search);
            return estudios.ToStudyListDto();
        }
        public async Task<StudyFormDto> Update(StudyFormDto study)
        {
            var existing = await _repository.GetById(study.Id);
            var code = await ValidarClaveNombre(study);
            if (existing.Clave != study.Clave || existing.Nombre != study.Nombre)
            {
                if (code != 0)
                {
                    throw new CustomException(HttpStatusCode.Conflict, Responses.Duplicated("La clave o nombre"));
                }
            }

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedAgent = study.ToModel(existing);

            await _repository.Update(updatedAgent);
            return existing.ToStudyFormDto();
        }

        public async Task<(byte[] file, string fileName)> ExportList(string search = null)
        {
            var studys = await GetAll(search);

            var path = Assets.StudyList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Estudios");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Estudios", studys);

            template.Generate();

            var range = template.Workbook.Worksheet("Estudios").Range("Estudios");
            var table = template.Workbook.Worksheet("Estudios").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

           return (template.ToByteArray(), $"Catálogo de Estudios.xlsx");
        }

        public async Task<(byte[] file, string fileName)> ExportForm(int id)
        {
            var study = await GetById(id);

            var path = Assets.StudyForm;

            var template = new XLTemplate(path);
            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Estudio");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Estudio", study);
            template.AddVariable("Indication", study.Indicaciones);
            template.AddVariable("paquete", study.Paquete);
            template.AddVariable("Parameter",study.Parameters);
            template.AddVariable("Reagent", study.Reactivos);
            template.AddVariable("WorkList", study.WorkList);

            template.Generate();

            template.Format();

            return (template.ToByteArray(), $"Catálogo de Estudios ({study.Clave}).xlsx");
        }

        private async Task<int> ValidarClaveNombre(StudyFormDto study)
        {

            var clave = study.Clave;
            var name = study.Nombre;

            var exists = await _repository.ValidateClaveNamne(clave, name);

            if (exists)
            {
                return 1;
            }

            return 0;
        }
    }
}
