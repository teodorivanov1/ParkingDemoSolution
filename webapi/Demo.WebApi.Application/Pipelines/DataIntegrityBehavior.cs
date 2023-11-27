using Demo.WebApi.Application.Abstractions.DataIntegrity;
using Demo.WebApi.Application.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Demo.WebApi.Application.Pipelines
{
    public class DataIntegrityBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
    {
        private readonly IDataIntegrityService<TRequest>[] _dataIntegrityServices;
        // logger

        public DataIntegrityBehavior(IDataIntegrityService<TRequest>[] dataIntegrityServices, ILogger<DataIntegrityBehavior<TRequest, TResponse>> logger)
        {
            _dataIntegrityServices = dataIntegrityServices ?? throw new ArgumentNullException(nameof(dataIntegrityServices));
            //_logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var typeName = request.TypeName();
            await Console.Out.WriteLineAsync($"StrictValidating request {typeName}");

            var failures = new List<string>();
            foreach (var services in _dataIntegrityServices)
            {
                var result = await services.ExecuteAsync(request, cancellationToken);
                failures.AddRange(result.Errors.Where(error => error != null));
            }

            if (failures.Any())
            {
                await Console.Out.WriteLineAsync($"Data Integrity Services errors - {typeName} - Request: {request} - Errors: {failures}");
                throw new InvalidDataException(string.Join(Environment.NewLine, failures));
            }
            return await next();
        }
    }
}
