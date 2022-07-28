using Service.MedicalRecord.Dtos.Appointment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Application.IApplication
{
    public interface IAppointmentApplication
    {
        Task<List<AppointmentList>> GetAllLab(SearchAppointment search);
        Task<List<AppointmentList>> GetAllDom(SearchAppointment search);

        Task<AppointmentForm> GetByIdLab(string id);
        Task<AppointmentForm> GetByIdDom(string id);
         Task<AppointmentList> CreateLab(AppointmentForm appointmentLab);
        Task<AppointmentList> CreateDom(AppointmentForm appointmentDom);
        Task<AppointmentList> UpdateLab(AppointmentForm appointmentLab);
        Task<AppointmentList> UpdateDom(AppointmentForm appointmentDom);
        Task<(byte[] file, string fileName)> ExportForm(exportFormDto data);
        Task<(byte[] file, string fileName)> ExportList(SearchAppointment search);
    }
}
