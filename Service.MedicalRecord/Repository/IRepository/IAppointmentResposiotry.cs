using Service.MedicalRecord.Domain.Appointments;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.MedicalRecord.Repository.IRepository
{
    public interface IAppointmentResposiotry
    {
        Task<List<AppointmentLab>> GetAllLab();
        Task<List<AppointmentDom>> GetAllDom();
        Task CreateLab(AppointmentLab appointmentLab);
        Task CreateDom(AppointmentDom appointmentDom);
        Task UpdateLab(AppointmentLab appointmentLab);
        Task UpdateDom(AppointmentDom appointmentDom);
    }
}
