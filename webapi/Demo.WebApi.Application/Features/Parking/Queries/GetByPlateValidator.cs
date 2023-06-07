using Demo.WebApi.Application.Features.Parking.CommonValidators;
using FluentValidation;

namespace Demo.WebApi.Application.Features.Parking.Queries
{
    public class GetByPlateValidator : AbstractValidator<GetByPlate>
    {
        public GetByPlateValidator()
        {
            RuleFor(x => x.PlateNumber!).ApplyPlateFormatRules();
        }

    }
}
