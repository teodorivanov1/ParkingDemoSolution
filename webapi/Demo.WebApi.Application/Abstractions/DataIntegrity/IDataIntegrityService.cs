namespace Demo.WebApi.Application.Abstractions.DataIntegrity
{
    public interface IDataIntegrityService<in T>
    {
        Task<DataIntegrityResult> ExecuteAsync(T request, CancellationToken cancellationToken = default);
    }
}
