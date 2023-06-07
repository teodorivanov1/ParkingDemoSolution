using Demo.Tests.Api.Fixtures.Auth;
using Demo.WebApi.Infrastructure;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Tests.Api.Fixtures
{
    public class BaseFixture : IDisposable
    {
        public HttpClient Client { get; private set; }
        public AppDbContext AppDbContext { get; private set; }

        public BaseFixture()
        {
            var appFactory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment("Test");
                });

            Client = appFactory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    // skip authorize attributes
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                });
            }).CreateClient();

            AppDbContext = appFactory.Services.GetService<AppDbContext>() ?? throw new NullReferenceException("Cannot initialize AppDbContext.");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

#nullable disable
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Client != null)
                {
                    Client.Dispose();
                    Client = null;
                }
                if (AppDbContext != null)
                {
                    AppDbContext.Dispose();
                    AppDbContext = null;
                }
            }
        }
    }
}
