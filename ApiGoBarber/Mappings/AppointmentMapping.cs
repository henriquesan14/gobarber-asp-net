using ApiGoBarber.DTOs;
using ApiGoBarber.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Mappings
{
    public class AppointmentMapping : Profile
    {
        public AppointmentMapping()
        {
            CreateMap<Appointment, CreateAppointmentDTO>().ReverseMap();
            CreateMap<Appointment, AppointmentDTO>().ReverseMap();
        }
    }
}
