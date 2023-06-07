using AutoMapper;
using Demo.WebApi.Application.Mapping;
using Demo.WebApi.Application.Pipelines;
using Demo.WebApi.Core.Abstraction;
using Demo.WebApi.Core.Entities;
using Demo.WebApi.Core.Services.RateCalculators;
using Demo.WebApi.Core.Services.RateCalculators.Discount;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Demo.WebApi.Application
{
    public static class RegisterService
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            // these calculators must be resolved using some abstraction.
            services.AddScoped(typeof(EmployeeDiscountCalculator), typeof(EmployeeDiscountCalculator));
            services.AddScoped(typeof(NoDiscountCalculator), typeof(NoDiscountCalculator));
            services.AddScoped(typeof(ICoreValidator<ParkingSpot>), typeof(ParkingSpotValidator));
            services.AddScoped(typeof(IParkingRateCalculator), typeof(ParkingRateCalculator));

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApplicationProfile(
                    provider.CreateScope()
                    .ServiceProvider.GetService<ICoreValidator<ParkingSpot>>()!));

            }).CreateMapper());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}