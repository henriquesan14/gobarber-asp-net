using ApiGoBarber.Entities.SchemasMongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetNotifications(int userId);
        Task<Notification> GetNotification(string id);
        Task Create(Notification product);
    }
}
