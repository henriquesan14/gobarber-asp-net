using ApiGoBarber.Context;
using ApiGoBarber.Entities.SchemasMongoDb;
using ApiGoBarber.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IGoBarberMongoContext _context;
        public NotificationRepository(IGoBarberMongoContext context)
        {
            _context = context;
        }

        public async Task Create(Notification product)
        {
            product.CreatedAt = DateTime.Now;
            await _context.Notifications.InsertOneAsync(product);
        }

        public async Task<Notification> GetNotification(string id)
        {
            return await _context.Notifications.Find(n => n.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Notification>> GetNotifications(int userId)
        {
            return await _context.Notifications.Find(n => n.UserId == userId).SortByDescending(n => n.CreatedAt).Limit(20).ToListAsync();
        }
    }
}
