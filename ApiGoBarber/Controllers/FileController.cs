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
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<ActionResult<AvatarDTO>> Save([FromForm] IFormFile file)
        {
            return await _fileService.Save(file);
        }
    }
}
