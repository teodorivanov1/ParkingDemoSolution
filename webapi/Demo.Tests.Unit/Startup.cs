using AutoMapper;
using Demo.WebApi.Application.Mapping;
using Demo.WebApi.Core.Abstraction;
using Demo.WebApi.Core.Entities;
using Demo.WebApi.Core.Services.RateCalculators;
using Demo.WebApi.Core.Services.RateCalculators.Discount;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Tests.Unit
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            // just use app register service ext
            services.AddScoped(typeof(ICoreValidator<ParkingSpot>), typeof(ParkingSpotValidator));
            services.AddScoped(typeof(EmployeeDiscountCalculator), typeof(EmployeeDiscountCalculator));
            services.AddScoped(typeof(NoDiscountCalculator), typeof(NoDiscountCalculator));
            services.AddScoped(typeof(IParkingRateCalculator), typeof(ParkingRateCalculator));
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApplicationProfile(provider.CreateScope().ServiceProvider.GetService<ICoreValidator<ParkingSpot>>()!));

            }).CreateMapper());
        }
    }
}
