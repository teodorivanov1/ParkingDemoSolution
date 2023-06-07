using FluentValidation;

namespace Demo.WebApi.Application.Features.Parking.CommonValidators
{
    public static class PlateFormatValidator
    {
        public const string RequiredPlateNumberErrorMessage = "The plate number is required.";
        public const string IvalidPlateNumberErrorMessage = "Invalid plate number. Enter between 4 and 8 characters.";

        public static (int From, int To) PlateRange = (From: 4, To: 8);

        public static IRuleBuilderOptions<T, string> ApplyPlateFormatRules<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotEmpty()
                .WithMessage(RequiredPlateNumberErrorMessage)
                .MinimumLength(PlateRange.From)
                .WithMessage(IvalidPlateNumberErrorMessage)
                .MaximumLength(PlateRange.To)
                .WithMessage(IvalidPlateNumberErrorMessage);
        }
    }
}
