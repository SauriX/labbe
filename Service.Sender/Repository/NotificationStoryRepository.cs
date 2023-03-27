
using Service.Sender.Context;
using Service.Sender.Domain.NotificationHistory;
using Service.Sender.Dtos;
using Service.Sender.Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace Service.Sender.Repository
{
    public class NotificationStoryRepository: INotificationStoryRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationStoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<NotificationHistory>> getByFilter(NotificationFilterDto filter) { 
        
            var notifications = _context.CAT_Notifications.AsQueryable();
            if (filter.SucursalId != null && filter.RolId !=null) {
                notifications = notifications.Where(x => (x.Para == "all" || x.Para == filter.SucursalId || x.Para == filter.RolId)&& DateTime.Now.Date == x.FechaCreacion.Date);
            }

            return await notifications.ToListAsync();
        }

        public async Task createNotification(NotificationHistory notification) { 
            _context.CAT_Notifications.Add(notification);
             await _context.SaveChangesAsync();
        } 

    }
}
