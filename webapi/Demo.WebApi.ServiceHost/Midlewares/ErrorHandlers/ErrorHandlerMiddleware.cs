using Demo.WebApi.ServiceHost.Midlewares.ErrorHandlers.Handlers;

namespace Demo.WebApi.ServiceHost.Midlewares.ErrorHandlers
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            //TODO inject all
            var defaultExceptionHandler = new DefaultExceptionHandler();
            var unauthorizedAccessExceptionHandler = new UnauthorizedAccessExceptionHandler(defaultExceptionHandler);
            var coreValidationExceptionHandler = new CoreValidationExceptionHandler(unauthorizedAccessExceptionHandler);
            var keyNotFoundExceptionHandler = new KeyNotFoundExceptionHandler(coreValidationExceptionHandler);
            var validationExceptionHandler = new ValidationExceptionHandler(keyNotFoundExceptionHandler);
            var apiExceptionHandler = new ApiExceptionHandler(validationExceptionHandler);

            try
            {
                await next(context);
            }
            catch (Exception error)
            {
                await apiExceptionHandler.Invoke(context, error);
            }
        }
    }
}
