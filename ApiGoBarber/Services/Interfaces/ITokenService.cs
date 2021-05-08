using ApiGoBarber.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
