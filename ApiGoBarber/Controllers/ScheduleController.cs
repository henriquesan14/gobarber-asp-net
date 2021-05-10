using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApiGoBarber.DTOs;
using ApiGoBarber.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGoBarber.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public ScheduleController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetSchedule([FromQuery] DateTime? date)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            var userId = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return Ok(await _appointmentService.GetSchedule(Int32.Parse(userId), date));
        }
    }
}
