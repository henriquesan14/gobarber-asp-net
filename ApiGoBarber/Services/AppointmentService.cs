﻿using ApiGoBarber.DTOs;
using ApiGoBarber.Entities;
using ApiGoBarber.Entities.SchemasMongoDb;
using ApiGoBarber.ExceptionUtil;
using ApiGoBarber.Page;
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
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        private readonly AppointmentValidator _validator;

        public AppointmentService(IAppointmentRepository repository, IMapper mapper, AppointmentValidator validator,
            IUserRepository userRepository, INotificationRepository notificationRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _mapper = mapper;
            _validator = validator;
            _notificationRepository = notificationRepository;
        }

        public async Task<PagedList<AppointmentDTO>> GetAppointments(int userId, PageFilter pageFilter)
        {
            var appointments = await _repository.GetAppointments(userId, pageFilter);
            var appointmentsDto = _mapper.Map<List<AppointmentDTO>>(appointments);
            return new PagedList<AppointmentDTO>(appointmentsDto, appointmentsDto.Count(), pageFilter.PageNumber, pageFilter.PageSize);
        }

        public async Task<CreateAppointmentDTO> SaveAppointment(CreateAppointmentDTO appointmentDto, int userId)
        {
            var result = _validator.Validate(appointmentDto);
            if (result.Errors.Count() > 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest, result.Errors);
            }
            User isProvider = await _userRepository.GetByIdAsync(appointmentDto.ProviderId);
            if (isProvider == null || !isProvider.Provider)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized, new
                {
                    Message = "Você só pode criar agendamentos para provedores"
                });
            }
            var date = appointmentDto.Date.Value;
            var hourStart = new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0);

            if (hourStart < DateTime.Now)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest, new { 
                    Message = "Não é permitido horário anterior ao atual"
                });
            }

            Expression<Func<Appointment, bool>> predicate = a => a.Provider.Id == appointmentDto.ProviderId && !a.CanceledAt.HasValue
            && a.Date.Value == hourStart;
            var checkAvailability = await _repository.GetAsync(predicate);
            if(checkAvailability.Count() > 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest, new
                {
                    Message = "Horário não está disponivel"
                });
            }

            Appointment appointment = _mapper.Map<Appointment>(appointmentDto);
            appointment.Provider = isProvider;
            User user = await _userRepository.GetByIdAsync(userId);
            appointment.User = user;
            Appointment appointmentSaved = await _repository.AddAsync(appointment);

            await _notificationRepository.Create(new Notification { 
                Content = $"Novo agendamento de {user.Name} para o dia {appointmentDto.Date.Value.Day}/{appointmentDto.Date.Value.Month}/{appointmentDto.Date.Value.Year} {appointmentDto.Date.Value.Hour}:{appointmentDto.Date.Value.Minute}",
                UserId = appointmentDto.ProviderId
            });
            return _mapper.Map<CreateAppointmentDTO>(appointmentSaved);
        }

        public async Task<IEnumerable<AppointmentDTO>> GetSchedule(int userId, DateTime? date)
        {
            if (!date.HasValue)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest, new
                {
                    Message = "O campo de Data é obrigatório"
                });
            }
            User isProvider = await _userRepository.GetByIdAsync(userId);
            if (isProvider == null || !isProvider.Provider)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized, new
                {
                    Message = "Usuário não é um provider"
                });
            }
            DateTime dateMin = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, 0, 0, 0);
            DateTime dateMax = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, 23, 59, 59);

            Expression<Func<Appointment, bool>> predicate = a =>  a.Provider.Id == userId && !a.CanceledAt.HasValue && (
                a.Date >= dateMin && a.Date <= dateMax
            );
            Func<IQueryable<Appointment>, IOrderedQueryable<Appointment>> orderBy = u => u.OrderBy(a => a.Date);
            IEnumerable<Appointment> appointments = await _repository.GetAsync(predicate, orderBy);
            IEnumerable<AppointmentDTO> appointmentsDto = _mapper.Map<List<AppointmentDTO>>(appointments);
            return appointmentsDto;
        }
    }

}