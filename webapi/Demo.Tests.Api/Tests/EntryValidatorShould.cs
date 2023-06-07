using Demo.Tests.Api.Data;
using Demo.Tests.Api.Fixtures;
using Demo.WebApi.Application.Abstractions;
using Demo.WebApi.Application.Features.Parking.Commands;
using Demo.WebApi.Application.Features.Parking.CommonValidators;
using Demo.WebApi.Core.Entities;
using Newtonsoft.Json;

namespace Demo.Tests.Api.Tests
{
    [Collection("SequentialTests")]
    public class EntryValidatorShould : BaseTest, IClassFixture<BaseFixture>
    {
        [Fact]
        public void Trigger_When_Plate_Is_Empty()
        {
            var body = JsonConvert.SerializeObject(EntryCommandFactory.Create(
                plate: string.Empty));

            Response<string> result = PostRequest<string>(body);

            Assert.False(result.Succeeded);
            Assert.Contains(PlateFormatValidator.RequiredPlateNumberErrorMessage, result.Errors!);
        }

        [Fact]
        public void Trigger_When_Plate_Chars_Is_Less()
        {
            var minimumChars = PlateFormatValidator.PlateRange.From;
            var body = JsonConvert.SerializeObject(EntryCommandFactory.Create(
                plate: new string('x', minimumChars - 1)));

            Response<string> result = PostRequest<string>(body);

            Assert.False(result.Succeeded);
            Assert.Contains(PlateFormatValidator.IvalidPlateNumberErrorMessage, result.Errors!);
        }

        [Fact]
        public void Trigger_When_Plate_Chars_Is_Greeter()
        {
            var minimumChars = PlateFormatValidator.PlateRange.To;
            var body = JsonConvert.SerializeObject(EntryCommandFactory.Create(
                plate: new string('x', minimumChars + 1)));

            Response<string> result = PostRequest<string>(body);

            Assert.False(result.Succeeded);
            Assert.Contains(PlateFormatValidator.IvalidPlateNumberErrorMessage, result.Errors!);
        }

        [Fact]
        public void Trigger_When_Already_In_Parking()
        {
            var body = JsonConvert.SerializeObject(EntryCommandFactory.Create());

            _ = PostRequest<ParkingSpot>(body);
            Response<string> result = PostRequest<string>(body);

            Assert.False(result.Succeeded);
            Assert.Contains(EntryCommandValidator.AlreadyInParkingErrorMessage, result.Errors!);
        }
    }
}