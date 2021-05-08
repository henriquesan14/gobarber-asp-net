using ApiGoBarber.Repositories;
using ApiGoBarber.Repositories.Base;
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

            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();

            //validators
            services.AddScoped<UserValidator>();
            services.AddScoped<UpdateUserValidator>();
            services.AddScoped<CredentialsValidator>();

            return services;
        }
    }
}
