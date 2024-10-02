using Microsoft.AspNetCore.Mvc;
using HouseManagementAPI.Models;
using System.Threading.Tasks;
using HouseManagementAPI.CQRS.Commands;
using HouseManagementAPI.CQRS.Queries;
using Microsoft.AspNetCore.RateLimiting;

namespace HouseManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("HouseLimiter")]
    public class HouseController : ControllerBase
    {
        private readonly AddHouseCommandHandler _addHouseHandler;
        private readonly GetHousesQueryHandler _getHousesHandler;

        public HouseController(AddHouseCommandHandler addHouseHandler, GetHousesQueryHandler getHousesHandler)
        {
            _addHouseHandler = addHouseHandler;
            _getHousesHandler = getHousesHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetHouses()
        {
            try
            {
                var query = new GetHousesQuery();
                var houses = await _getHousesHandler.Handle(query);
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
                var command = new AddHouseCommand { House = house };
                await _addHouseHandler.Handle(command);
                return Ok("House added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Update and Delete methods would similarly use command handlers
    }
}
