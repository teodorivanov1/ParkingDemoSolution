using System.Net;

namespace Demo.WebApi.ServiceHost.Midlewares.ErrorHandlers.Handlers
{
    public class UnauthorizedAccessExceptionHandler : BaseExceptionHandler
    {
        public UnauthorizedAccessExceptionHandler(IExceptionHandler nextExceptionHandler) : base(nextExceptionHandler)
        {
        }

        protected override bool CanHandle(Exception error)
        {
            return error is UnauthorizedAccessException;
        }

        protected override Task Handle(HttpContext context, Exception error)
        {
            context.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
            return Task.CompletedTask;
        }
    }
}
