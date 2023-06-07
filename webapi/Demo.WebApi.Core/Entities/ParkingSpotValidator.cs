using Demo.WebApi.Common.Enums;
using Demo.WebApi.Core.Abstraction;
using Demo.WebApi.Core.Exceptions;

namespace Demo.WebApi.Core.Entities
{
    public class ParkingSpotValidator : ICoreValidator<ParkingSpot>
    {
        private const string EmployeeDiscartErrorMessage = "Discounts are allowed only for cars.";
        private List<string> errors = new();

        public void Validate(ParkingSpot candidate)
        {
            // TODO make ICoreValidator abstract and do bellow
            // here must be only rules
            errors.Clear();
            ApplyDiscountRule(candidate);

            // Add rules ...

            if (errors.Count > 0)
            {
                throw new CoreValidationException(errors);
            }
        }

        public void ApplyDiscountRule(ParkingSpot candidate)
        {
            if (candidate.VehicleType != VehicleType.A && candidate.DiscountType == DiscountType.Employee)
            {
                errors.Add(EmployeeDiscartErrorMessage);
            }
        }
    }
}
