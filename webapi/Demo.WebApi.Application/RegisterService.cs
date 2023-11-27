using AutoMapper;
using Demo.WebApi.Application.Mapping;
using Demo.WebApi.Application.Pipelines;
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
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                //    cfg.AddProfile(new ApplicationProfile(
                //        provider.CreateScope()
                //        .ServiceProvider.GetService<ICoreValidator<ParkingSpot>>()!));
                cfg.AddProfile(new ApplicationProfile());
            }).CreateMapper());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluientValidatorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DataIntegrityBehavior<,>));
        }
    }
}