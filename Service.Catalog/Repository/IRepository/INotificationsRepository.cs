using Service.Catalog.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Catalog.Repository.IRepository
{
    public interface INotificationsRepository
    {
        Task<List<Notifications>> GetAll(string search, bool isNotification);
        Task<List<Notifications>> GetAllComplete(string search, bool isNotification,DateTime fecha);
        Task<Notifications> GetById(Guid id);
        Task<bool> IsDuplicate(Notifications notification);
        Task Create(Notifications notification);
        Task Update(Notifications notification);
    }
}
