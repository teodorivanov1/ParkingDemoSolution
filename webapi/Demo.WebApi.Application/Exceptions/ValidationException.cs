using FluentValidation.Results;

namespace Demo.WebApi.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("One or more request validation failures have occurred.") =>
            Errors = new List<string>();
        public List<string> Errors { get; }
        public ValidationException(IEnumerable<ValidationFailure> error) : this() =>
            Errors.AddRange(error.Select(f => f.ErrorMessage));
    }
}
