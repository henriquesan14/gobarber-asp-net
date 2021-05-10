using ApiGoBarber.DTOs;
using ApiGoBarber.Entities;
using ApiGoBarber.ExceptionUtil;
using ApiGoBarber.Repositories.Interfaces;
using ApiGoBarber.Services.Interfaces;
using ApiGoBarber.Validators;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiGoBarber.Services
{
    public class AppointmentService : IAppointmentService
    {

        private readonly IAppointmentRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly AppointmentValidator _validator;

        public AppointmentService(IAppointmentRepository repository, IMapper mapper, AppointmentValidator validator,
            IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<List<AppointmentDTO>> GetAppointments(int userId)
        {
            var appointments = await _repository.GetAppointments(userId);
            return _mapper.Map<List<AppointmentDTO>>(appointments);
        }

        public async Task<CreateAppointmentDTO> SaveAppointment(CreateAppointmentDTO appointmentDto, int userId)
        {
            var result = _validator.Validate(appointmentDto);
            if(result.Errors.Count() > 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest, result.Errors);
            }
            User isProvider = await _userRepository.GetByIdAsync(appointmentDto.ProviderId);
            if(isProvider == null || !isProvider.Provider)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized,new { 
                    Message = "Você só pode criar agendamentos para provedores"
                });
            }
            Appointment appointment = _mapper.Map<Appointment>(appointmentDto);
            appointment.Provider = isProvider;
            User user = await _userRepository.GetByIdAsync(appointmentDto.UserId);
            appointment.User = user;
            Appointment appointmentSaved = await _repository.AddAsync(appointment);
            return _mapper.Map<CreateAppointmentDTO>(appointmentSaved);
        }
    }
}
