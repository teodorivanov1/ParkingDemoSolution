namespace Demo.WebApi.ServiceHost.Midlewares.ErrorHandlers
{
    public abstract class BaseExceptionHandler : IExceptionHandler
    {
        private readonly IExceptionHandler nextExceptionHandler;

        public BaseExceptionHandler(IExceptionHandler nextExceptionHandler)
        {
            this.nextExceptionHandler = nextExceptionHandler;
        }

        protected abstract bool CanHandle(Exception error);

        protected virtual Task Handle(HttpContext context, Exception error)
        {
            return nextExceptionHandler.Invoke(context, error);
        }

        public async Task Invoke(HttpContext context, Exception error)
        {
            if (CanHandle(error))
            {
                var response = context.Response;
                response.ContentType = "application/json";
                await Handle(context, error);
            }
            else
            {
                await nextExceptionHandler.Invoke(context, error);
            }
        }
    }
}
