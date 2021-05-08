using ApiGoBarber.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Services.Interfaces
{
    public interface IFileService
    {
        Task<AvatarDTO> Save(IFormFile formFile);
    }
}
