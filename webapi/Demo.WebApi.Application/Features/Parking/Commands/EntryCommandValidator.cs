using Demo.WebApi.Application.Abstractions.Repositories;
using Demo.WebApi.Application.Features.Parking.CommonValidators;
using FluentValidation;

namespace Demo.WebApi.Application.Features.Parking.Commands
{
    public class EntryCommandValidator : AbstractValidator<EntryCommand>
    {
        public const string AlreadyInParkingErrorMessage = "Vehicle with such plate number is already registered.";

        private readonly IParkingSpotsRepository parkingSpotsRepository;

        public EntryCommandValidator(IParkingSpotsRepository parkingSpotsRepository)
        {
            RuleFor(x => x.Plate!).ApplyPlateFormatRules();

            this.parkingSpotsRepository = parkingSpotsRepository;

            RuleFor(x => x.Plate!)
                .Must(IsInParking)
                .WithMessage(AlreadyInParkingErrorMessage);
        }

        // in real world solution is better to separate data integrity checks in own validators
        private bool IsInParking(string plate) =>
            parkingSpotsRepository.GetByPlateAsync(plate).Result is null;
    }
}
