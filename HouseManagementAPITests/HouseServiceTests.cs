using HouseManagementAPI.Caching;
using HouseManagementAPI.Models;
using Microsoft.Extensions.Caching.Memory;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseManagementAPITests
{
    public class HouseServiceTests
    {
        [Fact]
        public async Task AddHouse_Should_Add_House()
        {
            // Arrange
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var service = new InMemoryCacheService(memoryCache);
            var house = new HouseModel
            {
                Address = "Test Address",
                NumberOfFloors = 2,
                UnitType = "house",
                Features = new List<string> { "balcony" }
            };

            // Act
            await service.AddHouseAsync(house);

            // Assert
            var houses = await service.GetHousesAsync();
            Assert.Contains(house, houses);
        }

        [Fact]
        public async Task UpdateHouse_Should_Update_House()
        {
            // Arrange
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var service = new InMemoryCacheService(memoryCache);

            var initialHouse = new HouseModel
            {
                Address = "Test Address",
                NumberOfFloors = 2,
                UnitType = "house",
                Features = new List<string> { "balcony" }
            };
            await service.AddHouseAsync(initialHouse);

            var updatedHouse = new HouseModel
            {
                Address = "Test Address",
                NumberOfFloors = 3,
                UnitType = "townhouse",
                Features = new List<string> { "garage" }
            };

            // Act
            await service.UpdateHouseAsync("Test Address", updatedHouse);

            // Assert
            var houses = await service.GetHousesAsync();
            Assert.DoesNotContain(initialHouse, houses); // Old house should be removed
            Assert.Contains(updatedHouse, houses); // Updated house should be present
            var houseInCache = houses.FirstOrDefault(h => h.Address == "Test Address");
            Assert.NotNull(houseInCache);
            Assert.Equal(3, houseInCache.NumberOfFloors);
            Assert.Equal("townhouse", houseInCache.UnitType);
            Assert.Contains("garage", houseInCache.Features);
        }

        [Fact]
        public async Task UpdateHouse_With_NonExistent_Address_Should_Do_Nothing()
        {
            // Arrange
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var service = new InMemoryCacheService(memoryCache);

            var initialHouse = new HouseModel
            {
                Address = "Test Address",
                NumberOfFloors = 2,
                UnitType = "house",
                Features = new List<string> { "balcony" }
            };
            await service.AddHouseAsync(initialHouse);

            var updatedHouse = new HouseModel
            {
                Address = "NonExistent Address",
                NumberOfFloors = 3,
                UnitType = "townhouse",
                Features = new List<string> { "garage" }
            };

            // Act
            await service.UpdateHouseAsync("NonExistent Address", updatedHouse);

            // Assert
            var houses = await service.GetHousesAsync();
            Assert.DoesNotContain(updatedHouse, houses); // Updated house should not be added
            Assert.Contains(initialHouse, houses); // The original house should still exist
        }

        [Fact]
        public async Task DeleteHouse_Should_Remove_House_From_Cache()
        {
            // Arrange
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var service = new InMemoryCacheService(memoryCache);

            var house = new HouseModel
            {
                Address = "Test Address",
                NumberOfFloors = 2,
                UnitType = "house",
                Features = new List<string> { "balcony" }
            };
            await service.AddHouseAsync(house);

            // Act
            await service.DeleteHouseAsync("Test Address");

            // Assert
            var houses = await service.GetHousesAsync();
            Assert.DoesNotContain(house, houses); // The house should be removed
        }

        [Fact]
        public async Task DeleteHouse_With_NonExistent_Address_Should_Do_Nothing()
        {
            // Arrange
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var service = new InMemoryCacheService(memoryCache);

            var house = new HouseModel
            {
                Address = "Test Address",
                NumberOfFloors = 2,
                UnitType = "house",
                Features = new List<string> { "balcony" }
            };
            await service.AddHouseAsync(house);

            // Act
            await service.DeleteHouseAsync("NonExistent Address");

            // Assert
            var houses = await service.GetHousesAsync();
            Assert.Contains(house, houses); // The house should still exist since the address was non-existent
        }

        [Fact]
        public async Task GetHouses_Should_Return_EmptyList_When_No_Houses_Added()
        {
            // Arrange
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var service = new InMemoryCacheService(memoryCache);

            // Act
            var houses = await service.GetHousesAsync();

            // Assert
            Assert.Empty(houses); // Should return an empty list if no houses are added
        }

        [Fact]
        public async Task GetHouses_Should_Return_All_Added_Houses()
        {
            // Arrange
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var service = new InMemoryCacheService(memoryCache);

            var house1 = new HouseModel
            {
                Address = "Test Address 1",
                NumberOfFloors = 2,
                UnitType = "house",
                Features = new List<string> { "balcony" }
            };
            var house2 = new HouseModel
            {
                Address = "Test Address 2",
                NumberOfFloors = 1,
                UnitType = "apartment",
                Features = new List<string> { "pool" }
            };

            // Act
            await service.AddHouseAsync(house1);
            await service.AddHouseAsync(house2);
            var houses = await service.GetHousesAsync();

            // Assert
            Assert.Equal(2, houses.Count);
            Assert.Contains(house1, houses);
            Assert.Contains(house2, houses);
        }
    }
}
