using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Service.Catalog.Context;
using Service.Catalog.Domain.Route;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Service.Catalog.Domain.Notifications;
using System.Linq;
using Service.Catalog.Repository.IRepository;

namespace Service.Catalog.Repository
{
    public class NotificationsRepository : INotificationsRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Notifications>> GetAll(string search,bool isNotification)
        {
            var notification = _context.Cat_notificaciones
                .AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                notification = notification.Where(x => x.Titulo.ToLower().Contains(search));
            }
           notification= notification.Where(x=> x.IsNotifi == isNotification);
            return await notification.ToListAsync();
        }
        public async Task<List<Notifications>> GetAllComplete(string search, bool isNotification, DateTime fecha)
        {
            var notification = _context.Cat_notificaciones
                .Include(x => x.Sucursales)
                .ThenInclude(x => x.Branch)
                .Include(x => x.Roles)
                .AsQueryable();

            search = search.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(search) && search != "all")
            {
                notification = notification.Where(x => x.Titulo.ToLower().Contains(search));
            }
            notification = notification.Where(x => x.IsNotifi == isNotification);
            var test = notification.First();
            
            notification = notification.Where(x => x.FechaInicial.Date <= fecha.Date && fecha.Date <= x.FechaFinal.Date);
            
            return await notification.ToListAsync();
        }
        public async Task<Notifications> GetById(Guid id)
        {
            var notification = await _context.Cat_notificaciones
                .Include(x => x.Sucursales)
                .ThenInclude(x => x.Branch)
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Id == id);

            return notification;
        }

        public async Task<bool> IsDuplicate(Notifications notification)
        {
            var isDuplicate = await _context.Cat_notificaciones.AnyAsync(x => x.Id != notification.Id && x.Titulo == notification.Titulo);

            return isDuplicate;
        }


        public async Task Create(Notifications notification)
        {
            _context.Cat_notificaciones.Add(notification);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Notifications notification)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var sucursales = notification.Sucursales.ToList();
                var  roles = notification.Roles.ToList();

                notification.Sucursales = null;
                notification.Roles = null;
                _context.Cat_notificaciones.Update(notification);

                await _context.SaveChangesAsync();

                var config = new BulkConfig();
                config.SetSynchronizeFilter<Notification_Branch>(x => x.NotificacionId == notification.Id);

                await _context.BulkInsertOrUpdateOrDeleteAsync(sucursales, config);
                config.SetSynchronizeFilter<Notification_Role>(x => x.NotificacionId == notification.Id);
                await _context.BulkInsertOrUpdateOrDeleteAsync(roles, config);
                transaction.Commit();
            }
            catch (System.Exception)
            {
                transaction.Rollback();
                throw;

            }
        }
    }
}
