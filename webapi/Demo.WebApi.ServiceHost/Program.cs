using Demo.WebApi.Application;
using Demo.WebApi.Infrastructure;
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

        builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

        builder.Services.ConfigureSwagger();

        builder.Services.ConfigureApplication();
        builder.Services.ConfigureInfrastructure(builder.Configuration);

        var app = builder.Build();

        // DO NOT use auto-migrate in production-ready app
        app.ApplyDbMigrations();

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
