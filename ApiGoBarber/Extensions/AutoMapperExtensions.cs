using ApiGoBarber.Repositories;
using ApiGoBarber.Repositories.Base;
using ApiGoBarber.Services;
using ApiGoBarber.Services.Interfaces;
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

            //Services
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
