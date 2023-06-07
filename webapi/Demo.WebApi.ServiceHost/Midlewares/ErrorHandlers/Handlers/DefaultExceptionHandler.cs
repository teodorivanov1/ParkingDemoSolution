using System.Net;

namespace Demo.WebApi.ServiceHost.Midlewares.ErrorHandlers.Handlers
{
    public class DefaultExceptionHandler : IExceptionHandler
    {
        public Task Invoke(HttpContext context, Exception error)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return Task.CompletedTask;
        }
    }
}
