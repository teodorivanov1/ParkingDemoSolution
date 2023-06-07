using Demo.Tests.Api.Data;
using Demo.WebApi.Application.Abstractions;
using Demo.WebApi.Application.Features.Parking.Commands;
using Newtonsoft.Json;

namespace Demo.Tests.Api.Tests
{
    public class EntryShould : BaseTest
    {
        [Fact]
        public void Succeed()
        {
            var rand = new Random();
            var rndPlate = string.Join("", Enumerable.Repeat(0, 5).Select(n => (char)rand.Next(127)));

            var body = JsonConvert.SerializeObject(EntryCommandFactory.Create(plate: rndPlate));

            Response<Response<EntryCommandResult>> result = PostRequest<Response<EntryCommandResult>>(body);

            Assert.True(result.Succeeded);
        }
    }
}
