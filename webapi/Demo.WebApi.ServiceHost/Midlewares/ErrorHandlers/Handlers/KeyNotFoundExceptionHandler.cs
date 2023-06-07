using System.Net;

namespace Demo.WebApi.ServiceHost.Midlewares.ErrorHandlers.Handlers
{
    public class KeyNotFoundExceptionHandler : BaseExceptionHandler
    {
        public KeyNotFoundExceptionHandler(IExceptionHandler nextExceptionHandler) : base(nextExceptionHandler)
        {
        }

        protected override bool CanHandle(Exception error)
        {
            return error is KeyNotFoundException;
        }

        protected override Task Handle(HttpContext context, Exception error)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            return Task.CompletedTask;
        }
    }

}
