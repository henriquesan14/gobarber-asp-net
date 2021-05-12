using ApiGoBarber.Repositories;
using ApiGoBarber.Repositories.Base;
using ApiGoBarber.Repositories.Interfaces;
using ApiGoBarber.Services;
using ApiGoBarber.Services.Interfaces;
using ApiGoBarber.Validators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            //Repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IFileRepository, FileRepository >();
            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            services.AddTransient<INotificationRepository, NotificationRepository>();

            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEmailService, EmailService>();

            //validators
            services.AddScoped<UserValidator>();
            services.AddScoped<UpdateUserValidator>();
            services.AddScoped<CredentialsValidator>();
            services.AddScoped<AppointmentValidator>();

            return services;
        }
    }
}
