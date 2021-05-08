using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGoBarber.DTOs;
using ApiGoBarber.Entities;
using ApiGoBarber.Extensions;
using ApiGoBarber.Repositories;
using ApiGoBarber.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGoBarber.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }
        

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Save([FromBody] UserDTO userDTO)
        {
            UserDTO user = await _service.Save(userDTO);
            return Created(new Uri($"{Request.Path}/{user.Id}", UriKind.Relative), user);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateUserDTO dto)
        {
            await _service.Update(dto);
            return NoContent();
        }
    }
}
