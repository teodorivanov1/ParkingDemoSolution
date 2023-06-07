namespace Demo.WebApi.Core.Abstraction
{
    // this is very basic solution, better to replace this interface with abstract class
    // and exclude some basic logic from actual validators.
    public interface ICoreValidator<TCandidate> where TCandidate : class
    {
        void Validate(TCandidate candidate);
    }
}
