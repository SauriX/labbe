using ClosedXML.Excel;
using ClosedXML.Report;
using Identidad.Api.Infraestructure.Repository.IRepository;
using Identidad.Api.Infraestructure.Services.IServices;
using Identidad.Api.mapper;
using Service.Catalog.Dictionary;
using Service.Catalog.Dtos.Medicos;
using Shared.Dictionary;
using Shared.Error;
using Shared.Extensions;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Identidad.Api.Infraestructure.Services
{
    public class MedicsApplication : IMedicsApplication
    {
        private readonly IMedicsRepository _repository;

        public MedicsApplication(IMedicsRepository repository)
        {
            _repository = repository;
        }

        public async Task<MedicsFormDto> GetById(Guid Id)
        {
            var Medicos = await _repository.GetById(Id);
            if (Medicos == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }
            return Medicos.ToMedicsFormDto();
        }
        public async Task<IEnumerable<MedicsListDto>> GetActive()
        {
            var Medicos = await _repository.GetActive();

            return Medicos.ToMedicsListDto();
        }
        public async Task<MedicsFormDto> Create(MedicsFormDto medic)
        {
            Helpers.ValidateGuid(medic.IdMedico.ToString(), out Guid guid);
           

            var code = await GenerateCode(medic);
            medic.Clave = code;
            var sameCode = medic.Clave == code;

            var newMedics = medic.ToModel();

            await _repository.Create(newMedics);

            medic = await GetById(newMedics.IdMedico);
            medic.ClaveCambio = !sameCode;

            return medic;
        }

        public async Task<IEnumerable<MedicsListDto>> GetAll(string search = null)
        {
            var doctors = await _repository.GetAll(search);
            return doctors.ToMedicsListDto();
        }
        public async Task<MedicsFormDto> Update(MedicsFormDto medics)
        {
            Helpers.ValidateGuid(medics.IdMedico.ToString(), out Guid guid);
            var existing = await _repository.GetById(medics.IdMedico);

            if (existing == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, Responses.NotFound);
            }

            var updatedAgent = medics.ToModel(existing);

            await _repository.Update(updatedAgent);
            return existing.ToMedicsFormDto();
        }

        private async Task<string> GenerateCode(MedicsFormDto medics, string suffix = null)
        {

            var code = medics.Nombre[..3].ToUpper();
            code += medics.PrimerApellido[..1].ToUpper();
            code += medics.SegundoApellido[..1].ToUpper();
            code += suffix;

            var exists = await _repository.GetByCode(code);

            if (exists != null)
            {
                if (code.Length == 5)
                {
                    return await GenerateCode(medics, "A");
                }

                var last = code[^1];
                var next = (char)((int)last + 1);

                return await GenerateCode(medics, next.ToString());
            }

            return code;
        }

        public async Task<(byte[]file, string fileName)> ExportList(string search)
        {
            var medics = await GetAll(search);

            var path = AssetsMedic.MedicList;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Medicos");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Medicos", medics);

            template.Generate();

            var range = template.Workbook.Worksheet("Medicos").Range("Medicos");
            var table = template.Workbook.Worksheet("Medicos").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium2;

            template.Format();

            return (template.ToByteArray(), "Catálogo de Médicos.xlsx");
        }

        public async Task<(byte[] file, string fileName)> ExportForm(Guid id)
        {
            var medics = await GetById(id);

            var path = AssetsMedic.MedicForm;

            var template = new XLTemplate(path);

            template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
            template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
            template.AddVariable("Titulo", "Medicos");
            template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            template.AddVariable("Medico", medics);
            //template.AddVariable("Medico", medics);

            template.Generate();

            template.Format();

            return (template.ToByteArray(), $"Catálogo de Médicos ({medics.Clave}).xlsx");
        }
    }
}