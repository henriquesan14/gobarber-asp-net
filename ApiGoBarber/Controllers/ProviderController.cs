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
    public class ProviderController : ControllerBase
    {

        private IUserService _userService;

        public ProviderController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProviderDTO>>> GetProviders() 
        {
            return await _userService.GetProviders();
        }
    }
}
