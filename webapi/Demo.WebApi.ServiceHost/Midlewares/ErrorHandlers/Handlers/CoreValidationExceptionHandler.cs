using Demo.WebApi.Application.Abstractions;
using Demo.WebApi.Core.Exceptions;
using System.Net;
using System.Text.Json;

namespace Demo.WebApi.ServiceHost.Midlewares.ErrorHandlers.Handlers
{
    public class CoreValidationExceptionHandler : BaseExceptionHandler
    {
        public CoreValidationExceptionHandler(IExceptionHandler nextExceptionHandler) : base(nextExceptionHandler)
        {
        }

        protected override bool CanHandle(Exception error)
        {
            return error is CoreValidationException;
        }

        protected override async Task Handle(HttpContext context, Exception error)
        {
            var responseModel = new Response<string>
            {
                Succeeded = false,
                Message = error?.Message,
                Errors = (error as CoreValidationException)?.Errors
            };

            var result = JsonSerializer.Serialize(responseModel);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(result);
        }
    }
}
