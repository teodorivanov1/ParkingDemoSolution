﻿using Demo.WebApi.Application.Abstractions;
using MediatR;

namespace Demo.WebApi.Application.Features.Clients.Commands
{
    public class UpdateCommand : IRequest<Response<bool>>
    {
        public UpdateCommand(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
