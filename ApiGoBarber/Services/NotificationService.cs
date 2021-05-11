using ApiGoBarber.DTOs;
using ApiGoBarber.Entities;
using ApiGoBarber.Entities.SchemasMongoDb;
using ApiGoBarber.ExceptionUtil;
using ApiGoBarber.Repositories.Interfaces;
using ApiGoBarber.Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public NotificationService(INotificationRepository notificationRepository, IUserRepository userRepository,
            IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<NotificationDTO>> GetNotifications(int userId)
        {
            User user = await _userRepository.GetByIdAsync(userId);
            if(user != null && !user.Provider)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.Unauthorized, new
                {
                    Message = "Somente provedores podem carregar notificações"
                });
            }
            IEnumerable<Notification>  notifications = await _notificationRepository.GetNotifications(userId);
            return _mapper.Map<List<NotificationDTO>>(notifications);
        }

        public async Task UpdateNotification(string id)
        {
            var notification = await _notificationRepository.GetNotification(id);
            notification.Read = true;
            notification.UpdatedAt = DateTime.Now;
            await _notificationRepository.Update(notification);
        }
    }
}
