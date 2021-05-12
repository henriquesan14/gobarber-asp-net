using ApiGoBarber.Context;
using ApiGoBarber.DTOs;
using ApiGoBarber.Repositories.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Repositories
{
    public class ProviderCacheRepository : IProviderCacheRepository
    {
        private readonly IGoBarberRedisContext _context;

        public ProviderCacheRepository(IGoBarberRedisContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteProviders()
        {
            return await _context.Redis.KeyDeleteAsync("providers");
        }

        public async Task<IEnumerable<ProviderDTO>> GetProviders()
        {
            var providers = await _context.Redis.StringGetAsync("providers");

            if (providers.IsNullOrEmpty)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<IEnumerable<ProviderDTO>>(providers);
        }

        public async Task<IEnumerable<ProviderDTO>> UpdateProviders(IEnumerable<ProviderDTO> providers)
        {
            var updated = await _context
                .Redis
                .StringSetAsync("providers", JsonConvert.SerializeObject(providers));

            if (!updated)
            {
                return null;
            }
            return await GetProviders();
        }
    }
}
