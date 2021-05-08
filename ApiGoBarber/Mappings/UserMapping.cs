using ApiGoBarber.DTOs;
using ApiGoBarber.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Mappings
{
    public class UserMapping : Profile
    {

        public UserMapping()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
