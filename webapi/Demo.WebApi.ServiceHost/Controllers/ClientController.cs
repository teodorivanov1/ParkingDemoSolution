using Demo.WebApi.Application.Features.Clients.Commands;
using Demo.WebApi.Application.Features.Clients.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Demo.WebApi.ServiceHost.Controllers
{
    [ApiController]
    public class ClientController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> AddClient(AddClientCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ListAll(int page = 1)
        {
            var data = await Mediator.Send(new GetAll(page));
            return View(data);
        }
    }
}
