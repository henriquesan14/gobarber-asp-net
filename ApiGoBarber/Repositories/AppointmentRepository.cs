using ApiGoBarber.Context;
using ApiGoBarber.Entities;
using ApiGoBarber.Repositories.Base;
using ApiGoBarber.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Repositories
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(GoBarberContext dbContext): base(dbContext)
        {

        }

        public async Task<IEnumerable<Appointment>> GetAppointments(int userId)
        {
            return await _dbContext.Appointments
                .AsNoTracking()
                    .Include(a => a.Provider)
                        .ThenInclude(p => p.Avatar)
                            .Where(a => a.User.Id == userId && !a.CanceledAt.HasValue)
                                .OrderBy(a => a.Date)
                                    .ToListAsync();
        }
    }
}
