using Microsoft.Extensions.Caching.Memory;
using HouseManagementAPI.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using HouseManagementAPI.Repositories;

namespace HouseManagementAPI.Caching
{
    public class InMemoryCacheService : IHouseCacheService
    {
        private readonly IMemoryCache _cache;
        private readonly string _cacheKey = "houses";

        public InMemoryCacheService(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public async Task<List<HouseModel>> GetHousesAsync()
        {
            return await Task.FromResult(_cache.GetOrCreate(_cacheKey, entry => new List<HouseModel>()));
        }

        public async Task AddHouseAsync(HouseModel house)
        {
            var houses = await GetHousesAsync();
            houses.Add(house);
            _cache.Set(_cacheKey, houses);
        }

        public async Task<bool> UpdateHouseAsync(string address, HouseModel updatedHouse)
        {
            var houses = await GetHousesAsync();
            var house = houses.FirstOrDefault(h => h.Address == address);
            if (house != null)
            {
                houses.Remove(house);
                houses.Add(updatedHouse);
                _cache.Set(_cacheKey, houses);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteHouseAsync(string address)
        {
            var houses = await GetHousesAsync();
            var house = houses.FirstOrDefault(h => h.Address == address);
            if (house != null)
            {
                houses.Remove(house);
                _cache.Set(_cacheKey, houses);
                return true;
            }
            return false;
        }
    }
}
