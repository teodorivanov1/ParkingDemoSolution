using Demo.WebApi.Application;
using Demo.WebApi.Infrastructure;
using Demo.WebApi.ServiceHost.Authorization;
using Demo.WebApi.ServiceHost.Extensions;
using Demo.WebApi.ServiceHost.Midlewares.ErrorHandlers;

public partial class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddRazorPages();
        builder.Services.AddControllersWithViews();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

        builder.Services.ConfigureSwaggerApiSequrity();
        builder.Services.ConfigureJwtAuthentication();

        builder.Services.ConfigureApplication();
        builder.Services.ConfigureInfrastructure(builder.Configuration);
        builder.Services.AddCustomIdentity();

        var app = builder.Build();

        app.InitializeDb();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.AddBoilerplate();

        app.UseMiddleware<ErrorHandlerMiddleware>();

        app.Run();
    }
}
