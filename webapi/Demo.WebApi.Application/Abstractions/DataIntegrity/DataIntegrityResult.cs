namespace Demo.WebApi.Application.Abstractions.DataIntegrity
{
    public class DataIntegrityResult
    {
        // TODO Adopt
        public DataIntegrityResult(List<string> errors)
        {
            Errors = errors;
        }

        public DataIntegrityResult()
            : this(new List<string>())
        {
        }

        public bool IsValid
        {
            get { return !Errors.Any(); }
        }

        public List<string> Errors { get; }
    }
}
