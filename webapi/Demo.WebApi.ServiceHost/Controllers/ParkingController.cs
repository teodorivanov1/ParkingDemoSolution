using Demo.WebApi.Application.Exceptions;
using Demo.WebApi.Application.Features.Parking.Commands;
using Demo.WebApi.Application.Features.Parking.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.WebApi.ServiceHost.Controllers
{
    [ApiController]
    public class ParkingController : BaseApiController
    {
        private const string ParkingConfigurationErrorMessage = "ParkingSpots is not found or configured in appsettings.json";
        private readonly int totalParkingSpots;
        public ParkingController(IConfiguration configuration)
        {
            if (!int.TryParse(configuration["ParkingSpots"], out totalParkingSpots) || totalParkingSpots == 0)
            {
                throw new ApiException(ParkingConfigurationErrorMessage);
            }
        }

        [HttpPost, Authorize()]
        public async Task<IActionResult> Entry(EntryCommand command)
        {
            var ocupied = await Mediator.Send(new GetOcupiedSpots());
            command.Availability = totalParkingSpots - ocupied.Data;

            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ListAll(int page = 1)
        {
            var data = await Mediator.Send(new GetAll(page));
            return View(data);
        }

        [HttpGet, Authorize()]
        public async Task<IActionResult> GetAvailability()
        {
            var ocupied = await Mediator.Send(new GetOcupiedSpots());
            return Ok(totalParkingSpots - ocupied.Data);
        }

        [HttpGet, Authorize()]
        public async Task<IActionResult> GetVehicleRate(string plateNumber)
        {
            var ocupied = await Mediator.Send(new GetByPlate(plateNumber));
            return Ok(ocupied);
        }

        [HttpPost, Authorize()]
        public async Task<IActionResult> Exit(string plateNumber)
        {
            var canditate = await Mediator.Send(new GetByPlate(plateNumber));
            await Mediator.Send(new ExitCommand(canditate.Data!.Id));
            return Ok(canditate);
        }
    }
}
