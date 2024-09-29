using Microsoft.AspNetCore.Mvc;
using HouseManagementAPI.Models;
using HouseManagementAPI.Caching;

namespace HouseManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        private readonly InMemoryCacheService _cacheService;

        public HouseController(InMemoryCacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpGet]
        public IActionResult GetHouses()
        {
            var houses = _cacheService.GetHouses();
            return Ok(houses);
        }

        [HttpPost]
        public IActionResult AddHouse([FromBody] HouseModel house)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _cacheService.AddHouse(house);
            return Ok("House added successfully.");
        }

        [HttpPut("{address}")]
        public IActionResult UpdateHouse(string address, [FromBody] HouseModel updatedHouse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _cacheService.UpdateHouse(address, updatedHouse);
            return Ok("House updated successfully.");
        }

        [HttpDelete("{address}")]
        public IActionResult DeleteHouse(string address)
        {
            _cacheService.DeleteHouse(address);
            return Ok("House deleted successfully.");
        }
    }
}
