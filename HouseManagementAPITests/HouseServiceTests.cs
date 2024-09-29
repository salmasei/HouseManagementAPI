
using HouseManagementAPI.Caching;
using HouseManagementAPI.Models;
using Microsoft.Extensions.Caching.Memory;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace HouseManagementAPITests
{
    public class HouseServiceTests
    {
        [Fact]
        public void AddHouse_Should_Add_House()
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
            service.AddHouse(house);

            // Assert
            var houses = service.GetHouses();
            Assert.Contains(house, houses);
        }

        [Fact]
        public void UpdateHouse_Should_Update_House()
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
            service.AddHouse(initialHouse);

            var updatedHouse = new HouseModel
            {
                Address = "Test Address",
                NumberOfFloors = 3,
                UnitType = "townhouse",
                Features = new List<string> { "garage" }
            };

            // Act
            service.UpdateHouse("Test Address", updatedHouse);

            // Assert
            var houses = service.GetHouses();
            Assert.DoesNotContain(initialHouse, houses); // Old house should be removed
            Assert.Contains(updatedHouse, houses); // Updated house should be present
            var houseInCache = houses.FirstOrDefault(h => h.Address == "Test Address");
            Assert.NotNull(houseInCache);
            Assert.Equal(3, houseInCache.NumberOfFloors);
            Assert.Equal("townhouse", houseInCache.UnitType);
            Assert.Contains("garage", houseInCache.Features);
        }

        [Fact]
        public void UpdateHouse_With_NonExistent_Address_Should_Do_Nothing()
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
            service.AddHouse(initialHouse);

            var updatedHouse = new HouseModel
            {
                Address = "NonExistent Address",
                NumberOfFloors = 3,
                UnitType = "townhouse",
                Features = new List<string> { "garage" }
            };

            // Act
            service.UpdateHouse("NonExistent Address", updatedHouse);

            // Assert
            var houses = service.GetHouses();
            Assert.DoesNotContain(updatedHouse, houses); // Updated house should not be added
            Assert.Contains(initialHouse, houses); // The original house should still exist
        }

        [Fact]
        public void DeleteHouse_Should_Remove_House_From_Cache()
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
            service.AddHouse(house);

            // Act
            service.DeleteHouse("Test Address");

            // Assert
            var houses = service.GetHouses();
            Assert.DoesNotContain(house, houses); // The house should be removed
        }

        [Fact]
        public void DeleteHouse_With_NonExistent_Address_Should_Do_Nothing()
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
            service.AddHouse(house);

            // Act
            service.DeleteHouse("NonExistent Address");

            // Assert
            var houses = service.GetHouses();
            Assert.Contains(house, houses); // The house should still exist since the address was non-existent
        }

        [Fact]
        public void GetHouses_Should_Return_EmptyList_When_No_Houses_Added()
        {
            // Arrange
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var service = new InMemoryCacheService(memoryCache);

            // Act
            var houses = service.GetHouses();

            // Assert
            Assert.Empty(houses); // Should return an empty list if no houses are added
        }

        [Fact]
        public void GetHouses_Should_Return_All_Added_Houses()
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
            service.AddHouse(house1);
            service.AddHouse(house2);
            var houses = service.GetHouses();

            // Assert
            Assert.Equal(2, houses.Count);
            Assert.Contains(house1, houses);
            Assert.Contains(house2, houses);
        }
    }
}
