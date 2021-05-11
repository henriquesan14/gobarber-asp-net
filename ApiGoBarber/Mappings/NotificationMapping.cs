using ApiGoBarber.DTOs;
using ApiGoBarber.Entities.SchemasMongoDb;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Mappings
{
    public class NotificationMapping : Profile
    {
        public NotificationMapping()
        {
            CreateMap<Notification, NotificationDTO>().ReverseMap();
        }
    }
}
