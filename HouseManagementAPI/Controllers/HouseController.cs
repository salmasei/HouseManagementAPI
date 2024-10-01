using Microsoft.AspNetCore.Mvc;
using HouseManagementAPI.Models;
using HouseManagementAPI.Caching;
using System.Threading.Tasks;
using HouseManagementAPI.Repositories;

namespace HouseManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        private readonly IHouseCacheService _houseService;

        public HouseController(IHouseCacheService houseService)
        {
            _houseService = houseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHouses()
        {
            try
            {
                var houses = await _houseService.GetHousesAsync();
                return Ok(houses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddHouse([FromBody] HouseModel house)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _houseService.AddHouseAsync(house);
                return Ok("House added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{address}")]
        public async Task<IActionResult> UpdateHouse(string address, [FromBody] HouseModel updatedHouse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var success = await _houseService.UpdateHouseAsync(address, updatedHouse);
                if (!success)
                {
                    return NotFound("House not found.");
                }

                return Ok("House updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{address}")]
        public async Task<IActionResult> DeleteHouse(string address)
        {
            try
            {
                var success = await _houseService.DeleteHouseAsync(address);
                if (!success)
                {
                    return NotFound("House not found.");
                }

                return Ok("House deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
