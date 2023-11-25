using AutoMapper;
using Demo.WebApi.Application.Features.Clients.Commands;
using Demo.WebApi.Core.Entities.Clients;

namespace Demo.WebApi.Application.Mapping
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<AddClientCommand, Client>()
                .ConstructUsing(src => new Client(src.Name));
        }
    }
}
