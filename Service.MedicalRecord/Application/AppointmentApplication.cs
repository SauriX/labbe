using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Domain.Appointments;
using Service.MedicalRecord.Dtos.Appointment;
using Service.MedicalRecord.Repository;
using Service.MedicalRecord.Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.MedicalRecord.Mapper;
using Service.MedicalRecord.Utils;
using Service.MedicalRecord.Client.IClient;
using System;
using Service.MedicalRecord.Dictionary;
using ClosedXML.Report;
using ClosedXML.Excel;
using Shared.Extensions;

namespace Service.MedicalRecord.Application
{
    public class AppointmentApplication: IAppointmentApplication
    {
        public readonly IAppointmentResposiotry _repository;
        private readonly ICatalogClient _catalogCliente;

        public AppointmentApplication(IAppointmentResposiotry repository, ICatalogClient catalogCliente)
        {
            _repository = repository;
            _catalogCliente = catalogCliente;   
        }
        public async Task<List<AppointmentList>> GetAllLab(SearchAppointment search) { 
            return (await _repository.GetAllLab(search)).ToApointmentListDtoLab();
        }
        public async Task<List<AppointmentList>> GetAllDom(SearchAppointment search) {
            return (await _repository.GetAllDom(search)).ToApointmentListDtoDom();
        }

        public async Task<AppointmentForm> GetByIdLab(string id) { 
                return  (await _repository.GetByIdLab(id)).ToDtoAppointmentLab();
        }
        public async Task<AppointmentForm> GetByIdDom(string id) {
            return (await _repository.GetByIdDom(id)).ToDtoAppointmentDom();
        }
        public async Task<AppointmentList> CreateLab(AppointmentForm appointmentLab) {
            var newAppointment = appointmentLab.toModel();
            var date = DateTime.Now.ToString("ddMMyy");
            var codeRange = await _catalogCliente.GetCodeRange(appointmentLab.SucursalId);
            var lastCode = await _repository.GetLastCode(date);

            var consecutive = Code.GetCode(codeRange, lastCode);
            var code = $"{consecutive}{date}";
            newAppointment.Cita = code;
            await _repository.CreateLab(newAppointment);
            newAppointment = await _repository.GetByIdLab(newAppointment.Id.ToString());

            return newAppointment.ToApointmentListDtoLab();
        }
        public async  Task<AppointmentList> CreateDom(AppointmentForm appointmentDom)
        {
            var newAppointment = appointmentDom.toModelDom();
            var date = DateTime.Now.ToString("ddMMyy");
            var codeRange = await _catalogCliente.GetCodeRange(appointmentDom.SucursalId);
            var lastCode = await _repository.GetLastCode(date);

            var consecutive = Code.GetCode(codeRange, lastCode);
            var code = $"{consecutive}{date}";
            newAppointment.Cita = code;
            await _repository.CreateDom(newAppointment);
            newAppointment = await _repository.GetByIdDom(newAppointment.Id.ToString());
                
            return newAppointment.ToApointmentListDtoDom();
        }
        public async Task<AppointmentList> UpdateLab(AppointmentForm appointmentLab) {
            var existing = await _repository.GetByIdLab(appointmentLab.Id.ToString());
            var newAppointment = appointmentLab.toModel(existing);
            await _repository.UpdateLab(newAppointment);
            newAppointment = await _repository.GetByIdLab(newAppointment.Id.ToString());

            return newAppointment.ToApointmentListDtoLab();
        }
        public async Task<AppointmentList> UpdateDom(AppointmentForm appointmentDom) {
            var existing = await _repository.GetByIdDom(appointmentDom.Id.ToString());
            var newAppointment = appointmentDom.toModelDom(existing);
            await _repository.UpdateDom(newAppointment);

            newAppointment = await _repository.GetByIdDom(newAppointment.Id.ToString());

            return newAppointment.ToApointmentListDtoDom();
        }
        public async Task<(byte[] file, string fileName)> ExportList(SearchAppointment search)
        {
            
            if (search.tipo == "laboratorio")
            {
                var studys = await GetAllLab(search);
                var path = Assets.CitaList;

                var template = new XLTemplate(path);

                template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
                template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
                template.AddVariable("Titulo", "Citas");
                template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
                template.AddVariable("Cotizaciones", studys);

                template.Generate();

                var range = template.Workbook.Worksheet("Cotizaciones").Range("Cotizaciones");
                var table = template.Workbook.Worksheet("Cotizaciones").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
                table.Theme = XLTableTheme.TableStyleMedium2;

                template.Format();

                return (template.ToByteArray(), $"Catálogo de Citas.xlsx");
            }
            else {
                var studys = await GetAllDom(search);
                var path = Assets.CitaList;

                var template = new XLTemplate(path);

                template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
                template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
                template.AddVariable("Titulo", "Citas");
                template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
                template.AddVariable("Cotizaciones", studys);

                template.Generate();

                var range = template.Workbook.Worksheet("Cotizaciones").Range("Cotizaciones");
                var table = template.Workbook.Worksheet("Cotizaciones").Range("$A$3:" + range.RangeAddress.LastAddress).CreateTable();
                table.Theme = XLTableTheme.TableStyleMedium2;

                template.Format();

                return (template.ToByteArray(), $"Catálogo de Citas.xlsx");
            }

            

        }

        public async Task<(byte[] file, string fileName)> ExportForm(exportFormDto data)
        {
            if (data.Tipo == "laboratorio")
            {
                var study = await GetByIdLab(data.Id);
                study.fechaNacimiento = DateTime.Now;
                var path = Assets.CitaForm;

                var template = new XLTemplate(path);
                template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
                template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
                template.AddVariable("Titulo", "Cita");
                template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
                template.AddVariable("Cotizacion", study);

                template.Generate();

                template.Format();

                return (template.ToByteArray(), $"Catálogo de Cita ({study.nomprePaciente}).xlsx");
            }
            else {
                var study = await GetByIdDom(data.Id);
                study.fechaNacimiento = DateTime.Now;
                var path = Assets.CitaForm;

                var template = new XLTemplate(path);
                template.AddVariable("Direccion", "Avenida Humberto Lobo #555");
                template.AddVariable("Sucursal", "San Pedro Garza García, Nuevo León");
                template.AddVariable("Titulo", "Cita");
                template.AddVariable("Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
                template.AddVariable("Cotizacion", study);

                template.Generate();

                template.Format();

                return (template.ToByteArray(), $"Catálogo de Cita ({study.nomprePaciente}).xlsx");
            }

        }
    }
}
