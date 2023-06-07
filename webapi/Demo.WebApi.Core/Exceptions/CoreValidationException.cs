namespace Demo.WebApi.Core.Exceptions
{
    public class CoreValidationException : Exception
    {
        public CoreValidationException() : base("One or more core validation rules have not satisfied.") =>
            Errors ??= new List<string>();
        public List<string> Errors { get; }
        public CoreValidationException(List<string> errors) : this() =>
            Errors.AddRange(errors);
    }
}
