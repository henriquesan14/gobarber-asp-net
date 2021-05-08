using ApiGoBarber.DTOs;
using ApiGoBarber.Entities;
using ApiGoBarber.ExceptionUtil;
using ApiGoBarber.Extensions;
using ApiGoBarber.Repositories;
using ApiGoBarber.Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiGoBarber.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserDTO> Save(UserDTO dto)
        {
            User userExist = await _repository.GetByEmail(dto.Email);
            if (userExist != null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest, new { 
                    Message = "Já existe um usuário com este email"
                });
            }
            User userCreated = await _repository.AddAsync(_mapper.Map<User>(dto));
            return _mapper.Map<UserDTO>(userCreated);
        }
    }
}
