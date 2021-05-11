using ApiGoBarber.DTOs;
using ApiGoBarber.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<CreateAppointmentDTO> SaveAppointment(CreateAppointmentDTO appointmentdTO, int userId);
        Task<PagedList<AppointmentDTO>> GetAppointments(int userId, PageFilter pageFilter);
        Task<IEnumerable<AppointmentDTO>> GetSchedule(int userId, DateTime? date);
        Task CancelAppointment(int id, int userId);
    }
}
