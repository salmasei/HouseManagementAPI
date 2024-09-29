using Microsoft.Extensions.Caching.Memory;
using HouseManagementAPI.Models;

namespace HouseManagementAPI.Caching
{
    public class InMemoryCacheService
    {
        private readonly IMemoryCache _cache;
        private readonly string _cacheKey = "houses";

        public InMemoryCacheService(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public List<HouseModel> GetHouses()
        {
            return _cache.GetOrCreate(_cacheKey, entry => new List<HouseModel>());
        }

        public void AddHouse(HouseModel house)
        {
            var houses = GetHouses();
            houses.Add(house);
            _cache.Set(_cacheKey, houses);
        }

        public void UpdateHouse(string address, HouseModel updatedHouse)
        {
            var houses = GetHouses();
            var house = houses.FirstOrDefault(h => h.Address == address);
            if (house != null)
            {
                houses.Remove(house);
                houses.Add(updatedHouse);
                _cache.Set(_cacheKey, houses);
            }
        }

        public void DeleteHouse(string address)
        {
            var houses = GetHouses();
            var house = houses.FirstOrDefault(h => h.Address == address);
            if (house != null)
            {
                houses.Remove(house);
                _cache.Set(_cacheKey, houses);
            }
        }
    }
}
