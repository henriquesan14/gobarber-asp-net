using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGoBarber.DTOs;
using ApiGoBarber.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGoBarber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;
        public AuthController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<AuthResponseDTO>> Login([FromBody] UserCredentialsDTO credentialsDto)
        {
            return await _service.Login(credentialsDto);
        }
    }
}
