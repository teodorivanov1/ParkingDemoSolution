﻿using Demo.WebApi.Application.Abstractions;
using Demo.WebApi.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace Demo.WebApi.ServiceHost.Midlewares.ErrorHandlers.Handlers
{
    public class ApiExceptionHandler : BaseExceptionHandler
    {
        public ApiExceptionHandler(IExceptionHandler nextExceptionHandler) : base(nextExceptionHandler)
        {
        }

        protected override bool CanHandle(Exception error)
        {
            return error is ApiException;
        }

        protected override async Task Handle(HttpContext context, Exception error)
        {
            var responseModel = new Response<string>() { Succeeded = false, Message = error?.Message };
            var result = JsonSerializer.Serialize(responseModel);

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(result);
        }
    }
}
