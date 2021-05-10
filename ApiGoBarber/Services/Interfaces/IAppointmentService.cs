using ApiGoBarber.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentDTO> SaveAppointment(AppointmentDTO appointmentdTO, int userId);
    }
}
