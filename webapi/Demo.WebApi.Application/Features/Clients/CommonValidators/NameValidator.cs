using FluentValidation;

namespace Demo.WebApi.Application.Features.Clients.CommonValidators
{
    public static class NameValidator
    {
        public const string RequiredNameMessage = "Name is required.";
        public const string IvalidNameLengthMessage = "Invalid name format. Enter between 3 and 30 characters.";

        public static (int From, int To) NameRange = (From: 3, To: 30);

        public static IRuleBuilderOptions<T, string> ApplyNameValidator<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage(RequiredNameMessage)
                .MinimumLength(NameRange.From).WithMessage(IvalidNameLengthMessage)
                .MaximumLength(NameRange.To).WithMessage(IvalidNameLengthMessage);
        }
    }
}
