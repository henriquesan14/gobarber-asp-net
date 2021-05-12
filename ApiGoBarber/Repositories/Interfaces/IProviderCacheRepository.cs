using ApiGoBarber.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Repositories.Interfaces
{
    public interface IProviderCacheRepository
    {
        Task<IEnumerable<ProviderDTO>> GetProviders();
        Task<IEnumerable<ProviderDTO>> UpdateProviders(IEnumerable<ProviderDTO> providers);
        Task<bool> DeleteProviders();
    }
}
