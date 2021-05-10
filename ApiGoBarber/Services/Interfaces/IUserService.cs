using ApiGoBarber.DTOs;
using ApiGoBarber.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> Save(UserDTO dto);
        Task Update(UpdateUserDTO dto);

        Task<AuthResponseDTO> Login(UserCredentialsDTO dto);

        Task<List<ProviderDTO>> GetProviders();
    }
}
