using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Demo.WebApi.ServiceHost.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public abstract class BaseApiController : Controller
    {
#nullable disable
        private IMediator mediator;
        protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
    }
}
