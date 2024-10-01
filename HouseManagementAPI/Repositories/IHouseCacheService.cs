using HouseManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HouseManagementAPI.Repositories
{
    public interface IHouseCacheService
    {
        Task<List<HouseModel>> GetHousesAsync();
        Task AddHouseAsync(HouseModel house);
        Task<bool> UpdateHouseAsync(string address, HouseModel updatedHouse);
        Task<bool> DeleteHouseAsync(string address);
    }
}
