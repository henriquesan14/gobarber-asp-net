using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApiGoBarber.DTOs;
using ApiGoBarber.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGoBarber.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost]
        public async Task<ActionResult<CreateAppointmentDTO>> SaveAppointment([FromBody] CreateAppointmentDTO appointmentDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            var userId = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return await _appointmentService.SaveAppointment(appointmentDto, Int32.Parse(userId));
        }

        [HttpGet]
        public async Task<ActionResult<List<AppointmentDTO>>> GetAppointments()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            var userId = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return await _appointmentService.GetAppointments(Int32.Parse(userId));
        }
    }
}
