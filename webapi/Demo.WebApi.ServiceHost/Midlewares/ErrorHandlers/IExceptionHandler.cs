namespace Demo.WebApi.ServiceHost.Midlewares.ErrorHandlers
{
    public interface IExceptionHandler
    {
        Task Invoke(HttpContext context, Exception error);
    }
}
