using Service.MedicalRecord.Domain.Appointments;
using Service.MedicalRecord.Dtos.Appointment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface IAppointmentResposiotry
    {
        Task<List<AppointmentLab>> GetAllLab(SearchAppointment search);
        Task<List<AppointmentDom>> GetAllDom(SearchAppointment search);
        Task<AppointmentLab> GetByIdLab(string id);
        Task<AppointmentDom> GetByIdDom(string id);
        Task CreateLab(AppointmentLab appointmentLab);
        Task CreateDom(AppointmentDom appointmentDom);
        Task UpdateLab(AppointmentLab appointmentLab);
        Task UpdateDom(AppointmentDom appointmentDom);
        Task<string> GetLastCode(string date);
    }
}
