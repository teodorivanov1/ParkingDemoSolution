using Autofac;
using Demo.WebApi.Application.Abstractions.DataIntegrity;
using Demo.WebApi.Application.Services.Client;
using System.Reflection;

namespace Demo.WebApi.ServiceHost
{
    public class AutofacModutole : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ClientDataIntegrityService).GetTypeInfo().Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IDataIntegrityService<>)))
                .AsImplementedInterfaces();
        }
    }
}
