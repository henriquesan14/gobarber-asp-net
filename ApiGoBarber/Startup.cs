using ApiGoBarber.Extensions;
using ApiGoBarber.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using ApiGoBarber.ExceptionUtil;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ApiGoBarber.Settings;
using Microsoft.Extensions.Options;

namespace ApiGoBarber
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.Configure<GoBarberDatabaseSettings>(Configuration.GetSection(nameof(GoBarberDatabaseSettings)));
            services.AddSingleton<IGoBarberDatabaseSettings>(sp =>
            sp.GetRequiredService<IOptions<GoBarberDatabaseSettings>>().Value);

            services.AddTransient<IGoBarberMongoContext , GoBarberMongoContext>();

            services.AddDbContext<GoBarberContext>(c => 
                c.UseSqlServer(Configuration.GetConnectionString("DbConnection")), ServiceLifetime.Singleton);

            services.AddAutoMapper(typeof(Startup));

            services.ResolveDependencies();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GoBarber API", Version = "v1" });
            });

            services.AddControllers(options =>
                options.Filters.Add(new HttpResponseExceptionFilter()));

            var key = Encoding.ASCII.GetBytes(Configuration["TokenSettings:Secret"]);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.JsonSerializationConfig();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GoBarber API V1");
            });


        }
    }
}
