using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ProviderController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IAppointmentService _appointmentService;

        public ProviderController(IUserService userService, IAppointmentService appointmentService)
        {
            _userService = userService;
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProviderDTO>>> GetProviders() 
        {
            return Ok(await _userService.GetProviders());
        }

        [HttpGet("{id}/Available")]
        public async Task<ActionResult<IEnumerable<AvailableDTO>>> GetAvailable(int id, [FromQuery] long date)
        {
            var available = await _appointmentService.GetAvailable(id, date);
            return Ok(available);
        }
    }
}
