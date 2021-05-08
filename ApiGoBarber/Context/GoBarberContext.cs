using ApiGoBarber.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Context
{
    public class GoBarberContext : DbContext
    {

        public GoBarberContext(DbContextOptions<GoBarberContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Avatar> Avatars { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
